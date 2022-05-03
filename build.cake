#addin "Cake.Xamarin"
#addin "Cake.Plist"
#addin "Cake.AndroidAppManifest"
//#tool "nuget:?package=GitVersion.CommandLine&amp;version=5.0.1" // Reference older version because newest doesn't work on macOS.
 
var target = Argument("target", (string)null);
var environmentArg = Argument("env", (string)null);
 
//====================================================================
// Consts
 
// Environment
const string DEV_ENV = "dev";
const string STAGING_ENV = "staging";
const string PROD_ENV = "prod";
 
// General
const string APP_NAME="GA360";
const string PATH_TO_SOLUTION = "GA360.sln";
const string PATH_TO_UNIT_TESTS_PROJECT = "GA360.Tests/GA360Tests.csproj";
const string APP_PACKAGE_FOLDER_NAME = "AppPackages";
 
// Android
const string PATH_TO_ANDROID_PROJECT = "GA360.Android/GA360.Android.csproj";
const string PATH_TO_ANDROID_MANIFEST_FILE = "GA360.Android/Properties/AndroidManifest.xml";
readonly string PATH_TO_ANDROID_KEYSTORE_FILE = EnvironmentVariable("GA360_KEYSTORE_PATH");
readonly string ANDROID_KEYSTORE_ALIAS = EnvironmentVariable("GA360_KEYSTORE_ALIAS");
readonly string ANDROID_KEYSTORE_PASSWORD = EnvironmentVariable("GA360_KEYSTORE_PASSWORD");
 
// iOS
const string PATH_TO_IOS_PROJECT = "GA360.iOS/GA360.iOS.csproj";
const string PATH_TO_INFO_PLIST_FILE = "GA360.iOS/Info.plist";
 
//====================================================================
// Moves app package to app packages folder
 
public string MoveAppPackageToPackagesFolder(FilePath appPackageFilePath)
{
    var packageFileName = appPackageFilePath.GetFilename();
    var targetAppPackageFilePath = new FilePath($"{APP_PACKAGE_FOLDER_NAME}/" + packageFileName);
 
    if (FileExists(targetAppPackageFilePath))
    {
        DeleteFile(targetAppPackageFilePath);
    }
 
    EnsureDirectoryExists($"{APP_PACKAGE_FOLDER_NAME}");
    MoveFile(appPackageFilePath, targetAppPackageFilePath);
 
    return targetAppPackageFilePath.ToString();
}
 
//====================================================================
// Class that hold information for current build.
 
public class BuildInfo
{
    public string ApiUrl { get; }
    public int BuildNumber { get; }
    public string AppVersion { get; }
    public string AppName { get; }
    public string PackageName { get; }
    public string AndroidAppCenterAppName { get; }
    public string ApkFileName { get; }
    public string IosAppCenterAppName { get; }
    public string IpaFileName { get; }
    public string Environment { get; }
 
    public BuildInfo(
      string apiUrl, 
      int buildNumber, 
      string appVersion,
      string appName,
      string packageName,
      string androidAppCenterAppName,
      string apkFileName,
      string iosAppCenterAppName,
      string ipaFileName,
      string environment)
    {
        ApiUrl = apiUrl;
        BuildNumber = buildNumber;
        AppVersion = appVersion;
        AppName = appName;
        PackageName = packageName;
        AndroidAppCenterAppName = androidAppCenterAppName;
        ApkFileName = apkFileName;
        IosAppCenterAppName = iosAppCenterAppName;
        IpaFileName = ipaFileName;
        Environment = environment;
    }
}
 
//====================================================================
// Setups script.
 
Setup<BuildInfo>(setupContext => 
{
    var gitVersion = GitVersion();
    var apiUrl = "https://dev.GuardianAngel360.com";
    var appName = $"{APP_NAME}.dev";
    var packageName = "com.guauardianangel360.GA360.dev";
    var androidAppCenterAppName = "GA360/GA360-Android-DEV";
    var iosAppCenterAppName = "GA360/GA360-iOS-DEV";
    var ipaFileName = $"{APP_NAME}.iOS.ipa";
    var currentEnvironment = GetEnvironment();
 
    if (currentEnvironment == STAGING_ENV)
    {
        apiUrl = "https://staging.guardianangel360.com";
        appName = $"{APP_NAME}.staging";
        packageName = "com.guardianangel306.GA360.staging";
        androidAppCenterAppName = "GA360/GA360-Android-staging";
        iosAppCenterAppName = "GA360/GA360-iOS-staging";
    }
    else if (currentEnvironment == PROD_ENV)
    {
        apiUrl = "https://prod.guardianangel360.com";
        appName = APP_NAME;
        packageName = "com.guardianangel360.GA360";
    }
 
    var apkFileName = $"{packageName}-Signed.apk";
 
    return new BuildInfo(
      apiUrl,
      buildNumber: (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
      appVersion: gitVersion.MajorMinorPatch,
      appName,
      packageName,
      androidAppCenterAppName,
      apkFileName,
      iosAppCenterAppName,
      ipaFileName,
      currentEnvironment);
});
 
public string GetEnvironment()
{
    if (String.IsNullOrEmpty(environmentArg))
    {
        var gitVersion = GitVersion();
        var branchName = gitVersion.BranchName;
 
        if (branchName.StartsWith("release/"))
        {
            return STAGING_ENV;
        }
        else if (branchName.Equals("master"))
        {
            return PROD_ENV;
        }
        else
        {
            return DEV_ENV;
        }
    }
    else
    {
        return environmentArg;
    }
}
 
//====================================================================
// Cleans all bin and obj folders.
 
Task("Clean")
  .Does(() =>
{
  CleanDirectories("**/bin");
  CleanDirectories("**/obj");
});
 
//====================================================================
// Restores NuGet packages for solution.
 
Task("Restore")
  .Does(() =>
{
  NuGetRestore(PATH_TO_SOLUTION);
});
 
//====================================================================
// Run unit tests
 
Task("RunUnitTests")
  .IsDependentOn("Clean")
  .IsDependentOn("Restore")
  .Does(() =>
  {
     var settings = new DotNetCoreTestSettings
     {
         Configuration = "Release",
         ArgumentCustomization = args=>args.Append("--logger trx")
     };
 
      DotNetCoreTest(PATH_TO_UNIT_TESTS_PROJECT, settings);
  });
 
//====================================================================
// Updates config files with proper values
 
Task("UpdateConfigFiles")
  .Does<BuildInfo>(buildInfo =>
  {
      var appSettingsFile = File("GA360/AppSettings.cs");
      TransformTextFile(appSettingsFile)
        .WithToken("API_URL", buildInfo.ApiUrl)
        .Save(appSettingsFile);
  });
 
//==================================================================== Android ====================================================================
 
//====================================================================
// Update Android Manifest
 
Task("UpdateAndroidManifest")
  .Does<BuildInfo>(buildInfo =>
{
    var androidManifestFilePath = new FilePath(PATH_TO_ANDROID_MANIFEST_FILE);
    var manifest = DeserializeAppManifest(androidManifestFilePath);
 
    manifest.VersionName = buildInfo.AppVersion;
    manifest.VersionCode = buildInfo.BuildNumber;
    manifest.ApplicationLabel = buildInfo.AppName;
    manifest.PackageName = buildInfo.PackageName;
    manifest.Debuggable = false;
 
    SerializeAppManifest(androidManifestFilePath, manifest);
});
 
//====================================================================
// Publish Android APK
 
Task("PublishAPK")
  .IsDependentOn("RunUnitTests")
  .IsDependentOn("UpdateConfigFiles")
  .IsDependentOn("UpdateAndroidManifest")
  .Does<BuildInfo>(buildInfo => 
{
    FilePath apkFilePath = null;
 
    if (buildInfo.Environment == PROD_ENV)
    {
        apkFilePath = BuildAndroidApk(PATH_TO_ANDROID_PROJECT, sign: true, configurator: msBuildSettings => 
        {
            msBuildSettings.WithProperty("AndroidKeyStore", "True")
                           .WithProperty("AndroidSigningKeyAlias", ANDROID_KEYSTORE_ALIAS)
                           .WithProperty("AndroidSigningKeyPass", ANDROID_KEYSTORE_PASSWORD)
                           .WithProperty("AndroidSigningKeyStore", PATH_TO_ANDROID_KEYSTORE_FILE)
                           .WithProperty("AndroidSigningStorePass", ANDROID_KEYSTORE_PASSWORD);
        });
    }
    else
    {
        apkFilePath = BuildAndroidApk(PATH_TO_ANDROID_PROJECT, sign: true);
    }
 
    MoveAppPackageToPackagesFolder(apkFilePath);
});
 
//==================================================================== iOS ====================================================================
 
//====================================================================
// Update iOS Info.plist
 
Task("UpdateIosInfoPlist")
  .Does<BuildInfo>(buildInfo =>
  {
    var plist = File(PATH_TO_INFO_PLIST_FILE);
    dynamic data = DeserializePlist(plist);
 
    data["CFBundleShortVersionString"] = buildInfo.AppVersion;
    data["CFBundleVersion"] = buildInfo.BuildNumber.ToString();
    data["CFBundleName"] = buildInfo.AppName;
    data["CFBundleDisplayName"] = buildInfo.AppName;
    data["CFBundleIdentifier"] = buildInfo.PackageName;
 
    SerializePlist(plist, data);
  });
 
//====================================================================
// Publish iOS IPA
 
Task("PublishIPA")
  .IsDependentOn("RunUnitTests")
  .IsDependentOn("UpdateConfigFiles")
  .IsDependentOn("UpdateIosInfoPlist")
  .Does<BuildInfo>(buildInfo =>
  {
    var buildConfiguration = "Release";
 
    if (buildInfo.Environment == PROD_ENV)
    {
        buildConfiguration = "AppStore";
    }
 
    var ipaFilePath = BuildiOSIpa(PATH_TO_IOS_PROJECT, buildConfiguration);
    MoveAppPackageToPackagesFolder(ipaFilePath);
  });
 
//==============================================
bundletool build-apks \
--bundle=com.guardianangel360.ga360.aab \
--output=com.guardianangel360.ga360.apks \
--ks=GuardianAngel360.keystore \
--ks-pass=pass:AcatoIm2022! \
--ks-key-alias=guardianangel360 

bundletool install-apks --apks=com.guardianangel360.ga360.apks



#echo export ANDROID_HOME=/Users/markwardell/Library/Developer/Xamarin/android-sdk-macosx
#echo export PATH=$PATH:$ANDROID_HOME/platform-tools
#echo export PATH=$PATH:$ANDROID_HOME/tools
#echo export PATH=$PATH:$ANDROID_HOME/tools/bin
#/Users/markwardell/Library/Developer/Xamarin/android-sdk-macosx
#echo export PATH=$PATH:$ANDROID_HOME/emulator

keytool -genkey \
        -v \
        -keystore AcatoIm.keystore \
        -alias AcatoIm \
        -keyalg RSA \
        -keysize 2048 \
        -validity 20000
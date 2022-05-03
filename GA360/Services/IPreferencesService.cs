namespace GA360.Services
{
    public interface IPreferencesService
    {
        int Radius { get; set; }
        int Theme { get; set; }
        int GPSPermissionStatus { get; set; }

        string LanguageCode { get; set; }
    }
}
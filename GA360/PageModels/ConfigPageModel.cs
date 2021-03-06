using System.Text;
using System.Threading.Tasks;
using GA360.Models;
using GA360.Services;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Essentials.Interfaces;
using Nito.AsyncEx;


namespace GA360.PageModels
{
    public class ConfigPageModel : BasePageModel
    {
        private readonly IPreferencesService _preferencesService;
        private readonly IClipboard _clipBoard;
        private readonly IPermissions _permissions;

        private string _languageCode;

        private int _radius;

        private int _theme;

        private bool _allowLocation;

        private bool _isVisibleToClipBoard;
        
        public MvvmHelpers.ObservableRangeCollection<Config> Configurations { get; set; }
        
        public AsyncCommand ToClipBoardCommand { get; set; }
        
        public bool IsVisibleToClipBoard { get => _isVisibleToClipBoard; set => SetProperty(ref _isVisibleToClipBoard, value); }

        public ConfigPageModel(IPreferencesService preferencesService, IClipboard clipBoard, IPermissions permissions)
        {
            _preferencesService = preferencesService;
            _clipBoard = clipBoard;
            _permissions = permissions;

            ToClipBoardCommand = new AsyncCommand(ToClipBoard);
        }
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Radius = {Radius}");
            sb.AppendLine($"LanguageCode = {LanguageCode}");
            sb.AppendLine($"Theme = {Theme}");
            return sb.ToString();
        }

        public bool AllowLocation
        {
            get
            {
                return true;
            }
        }

        public int Theme
        {
            get => _theme;
            set => base.SetProperty(ref _theme, value);
        }

        public int Radius
        {
            get
            {
                _radius = _preferencesService.Radius;
                return _radius;
            }
            set
            {
                base.SetProperty(ref _radius, value);
                _preferencesService.Radius = value;
            }
        }

        public string LanguageCode
        {
            get
            {
                _languageCode = _preferencesService.LanguageCode;
                return _languageCode;
            }
            set
            {
                base.SetProperty(ref _languageCode, value);
                _preferencesService.LanguageCode = value;
            }
        }
        
        private async Task ToClipBoard()
        {
            await _clipBoard.SetTextAsync(this.ToString());
            IsVisibleToClipBoard = true;
        }
    }
}
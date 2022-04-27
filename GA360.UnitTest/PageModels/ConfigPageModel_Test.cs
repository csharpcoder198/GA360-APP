using GA360.PageModels;
using GA360.Services;
using NSubstitute;
using Xamarin.Essentials.Interfaces;
using Xunit;

namespace GA360.UnitTest.PageModels
{
    public class ConfigPageModel_Test
    {
        [Fact]
        public void TestDefaults()
        {
            var tHelper = new TestHelper();
            
            tHelper.MockPrefService.Radius.Returns(1);
            Assert.Equal(tHelper.TestInstance.Radius,1);
            
            tHelper.MockPrefService.LanguageCode.Returns("EN");
            Assert.Equal(tHelper.TestInstance.LanguageCode,"EN");
            
            tHelper.MockPrefService.Theme.Returns(0);
            Assert.Equal(tHelper.TestInstance.Theme,0);
            
        }
        
        private class TestHelper
        {
            public ConfigPageModel TestInstance { get; set; }
            public IPreferencesService MockPrefService { get; set; }
            public TestHelper()
            {
                Startup.Init();
                var xesPrefService = Substitute.For<IPreferencesService>();
                var clipBoard = Substitute.For<IClipboard>();
                
                MockPrefService = xesPrefService;
               
                var ps = new ConfigPageModel(xesPrefService, clipBoard);
                
                TestInstance = ps;
            }
        }
    }
}
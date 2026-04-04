using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace PingboardWhosWhoAutoPlay
{
    internal static class SeleniumDriver
    {
        public static IWebDriver? Driver { get; private set; }
        public static WebDriverWait? Wait { get; private set; }

        public static void Initialize()
        {
            var options = new ChromeOptions();
            options.AddArgument("--window-size=785,750");
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--log-level=3");

            var driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;

            Driver = new ChromeDriver(driverService, options);
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(60));
        }

        public static void Dispose()
        {
            Driver?.Quit();
            Driver?.Dispose();
            Driver = null;
            Wait = null;
        }
    }
}

using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace PingboardWhosWhoAutoPlay
{
    internal static class WebActions
    {
        private const string SignInUrl = "https://pingboard.workleap.com/home";
        private const int ScrollDelayMs = 2000;
        private const int ButtonPollDelayMs = 100;

        public static void Run(CancellationToken ct)
        {
            MainForm.Log("Login to Pingboard with your credentials");
            WaitForLogin(ct);

            MainForm.Log("Navigating to Directory");
            NavigateToDirectory(ct);

            MainForm.Log("Retrieving Employee List");
            SetListView(ct);
            ScrollToBottom(ct);

            var employees = ExtractEmployees();
            MainForm.Log($"Employees Extracted: {employees.Count}");

            ReturnToHomePage(ct);

            MainForm.Log("Beginning Who's Who Game");
            StartWhosWhoGame(ct);
            PlayGame(employees, ct);
        }

        private static void WaitForLogin(CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            SeleniumDriver.Driver!.Navigate().GoToUrl(SignInUrl);
            SeleniumDriver.Wait!.Until(d =>
            {
                ct.ThrowIfCancellationRequested();
                return d.Title.Contains("Home");
            });
        }

        private static void NavigateToDirectory(CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            var directoryLink = SeleniumDriver.Wait!.Until(
                ExpectedConditions.ElementToBeClickable(By.Id("directory-guide_tip-link")));
            directoryLink.Click();
            SeleniumDriver.Wait.Until(ExpectedConditions.TitleContains("Directory"));
        }

        private static void SetListView(CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            var viewAsListButton = SeleniumDriver.Wait!.Until(
                ExpectedConditions.ElementToBeClickable(By.CssSelector("button[aria-label='View as list']")));
            viewAsListButton.Click();
        }

        private static void ScrollToBottom(CancellationToken ct)
        {
            var js = (IJavaScriptExecutor)SeleniumDriver.Driver!;
            long lastHeight = (long)(js.ExecuteScript("return document.body.scrollHeight") ?? 0L);

            while (true)
            {
                ct.ThrowIfCancellationRequested();
                js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
                Thread.Sleep(ScrollDelayMs);

                long newHeight = (long)(js.ExecuteScript("return document.body.scrollHeight") ?? 0L);
                if (newHeight == lastHeight)
                    break;

                lastHeight = newHeight;
            }
        }

        private static List<Employee> ExtractEmployees()
        {
            var employees = new List<Employee>();

            var userList = SeleniumDriver.Wait!.Until(
                ExpectedConditions.ElementIsVisible(By.CssSelector("ul.divide-y")));
            var userItems = userList.FindElements(By.CssSelector("li[id^='user-cell-']"));

            foreach (var item in userItems)
            {
                try
                {
                    var img = item.FindElement(By.CssSelector("img"));
                    string? name = img.GetAttribute("alt")?.Trim();
                    string? imageUrl = img.GetAttribute("src");

                    if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(imageUrl))
                        employees.Add(new Employee(name, imageUrl));
                }
                catch (NoSuchElementException)
                {
                    // Skip items without an image element
                }
            }

            return employees;
        }

        private static void ReturnToHomePage(CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            ((IJavaScriptExecutor)SeleniumDriver.Driver!).ExecuteScript("window.scrollTo(0, 0);");

            var homeLink = SeleniumDriver.Wait!.Until(
                ExpectedConditions.ElementToBeClickable(By.Id("home-link")));
            homeLink.Click();
            SeleniumDriver.Wait.Until(ExpectedConditions.TitleContains("Home"));
        }

        private static void StartWhosWhoGame(CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            var playButton = SeleniumDriver.Wait!.Until(
                ExpectedConditions.ElementToBeClickable(
                    By.XPath("//button[.//span[text()=\"Play Who's Who\"]]")));
            playButton.Click();

            var everyoneButton = SeleniumDriver.Wait.Until(
                ExpectedConditions.ElementToBeClickable(
                    By.XPath("//button[.//div[text()='Everyone']]")));
            everyoneButton.Click();
        }

        private static string? ExtractMemberId(string? url)
        {
            if (url is null) return null;
            const string marker = "/members/";
            int start = url.IndexOf(marker);
            if (start < 0) return null;
            start += marker.Length;
            int end = url.IndexOf('/', start);
            return end > start ? url[start..end] : url[start..];
        }

        private static void PlayGame(List<Employee> employees, CancellationToken ct)
        {
            SeleniumDriver.Wait!.Until(
                ExpectedConditions.ElementExists(By.XPath("//span[text()=\"Who's this?\"]")));

            int answered = 0;

            while (!ct.IsCancellationRequested)
            {
                var image = SeleniumDriver.Wait.Until(
                    ExpectedConditions.ElementIsVisible(
                        By.XPath("//img[contains(@class, 'styles__WhosWhoFace')]")));
                string? imageUrl = image.GetAttribute("src");
                string? gameMemberId = ExtractMemberId(imageUrl);

                var employee = gameMemberId is not null
                    ? employees.FirstOrDefault(e => ExtractMemberId(e.ImageUrl) == gameMemberId)
                    : null;
                if (employee != null)
                {
                    answered++;
                    MainForm.Log($"Employee Found ({answered}): {employee.Name}");

                    var button = SeleniumDriver.Wait.Until(
                        ExpectedConditions.ElementToBeClickable(
                            By.XPath($"//button[@data-testid='whos-who-choice'][.//span[text()='{employee.Name}']]")));
                    button.Click();
                }

                ClickNextButton(ct);
            }
        }

        private static void ClickNextButton(CancellationToken ct)
        {
            string[] labels = ["Next", "Next Round", "Continue"];

            while (!ct.IsCancellationRequested)
            {
                foreach (var label in labels)
                {
                    var buttons = SeleniumDriver.Driver!.FindElements(
                        By.XPath($"//button[.//span[text()='{label}']]"));

                    if (buttons.Count > 0)
                    {
                        buttons[0].Click();
                        return;
                    }
                }

                Thread.Sleep(ButtonPollDelayMs);
            }
        }
    }
}

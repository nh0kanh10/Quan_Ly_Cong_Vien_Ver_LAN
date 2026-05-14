using DaiNam.UITests.Core;
using NUnit.Framework;

namespace DaiNam.UITests.Tests
{
    [SetUpFixture]
    public class GlobalTestSetup
    {
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            AppManager.LaunchApp();
            
            // Bypass login (if needed, map a LoginPage and log in here)
            // Example:
            // var loginPage = new LoginPage(AppManager.MainWindow);
            // loginPage.Login("admin", "admin");
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            AppManager.CloseApp();
        }
    }

    public abstract class TestBase
    {
        [SetUp]
        public void BeforeEachTest()
        {
            // Any specific setup before each test, e.g. navigating to home
        }

        [TearDown]
        public void AfterEachTest()
        {
            // Take screenshot on failure
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                // Capture screenshot logic here
            }

            // Navigate back to safe state
        }
    }
}

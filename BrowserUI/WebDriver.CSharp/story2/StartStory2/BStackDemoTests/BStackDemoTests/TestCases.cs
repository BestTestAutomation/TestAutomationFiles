using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;


namespace BStackDemoTests
{
    [TestFixture]
    public class TestCases
    {
        IWebDriver driver;
        IWebElement element;
        
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [TearDown]
        public void Teardown() 
        {
            driver.Quit();
        }

        #region LocatorStrings
        private readonly string bStackUrl = "https://bstackdemo.com/";
        private By homepageSignInLink = By.Id("signin");
        private By userNameTextBox = By.Id("react-select-2-input");
        private By passwordTextbox = By.Id("react-select-3-input");
        private By loginButton = By.Id("login-btn");
        private By headerUsernameText = By.ClassName("username");
        private By errorHeader = By.XPath("//*[@id=\"__next\"]/div[2]/div/form/div[2]/h3");
        private By logoutLink = By.Id("logout");
        #endregion


        [TestCase("demouser", "testingisfun99", "demouser", true, TestName = "Sign in using valid credentials")]
        [TestCase("demouserNOT", "testingisfun99", "Invalid Username", false, TestName = "Sign in using invalid username")]
        [TestCase("demouser", "testingisstillfun99", "Invalid Password", false, TestName = "Sign in using invalid password")]
        [TestCase("", "", "Invalid Username", false, TestName = "Sign in using blank credentials")]
        [TestCase("", "testingisfun99", "Invalid Username", false, TestName = "Sign in using blank username")]
        [TestCase("demouser", "", "Invalid Password", false, TestName = "Sign in using blank password")]
        public void SignInTestCases(string username, string password, string expectedResult, bool isValid)
        {
            // navigate to the page
            driver.Navigate().GoToUrl(bStackUrl);
            SignIn(username, password);
            if (isValid)
            {
                // verify that we have signed in
                element = driver.FindElement(headerUsernameText);
            }
            else
            {
                // verify that we have NOT signed in
                element = driver.FindElement(errorHeader);
            }
            string textToVerify = element.Text;
            Assert.AreEqual(expectedResult, textToVerify);
        }


        [TestCase("demouser", "testingisfun99", TestName = "Logout")]
        public void Logout(string username, string password)
        {
            // navigate to the page
            driver.Navigate().GoToUrl(bStackUrl);
            SignIn(username, password);
            element = driver.FindElement(logoutLink);
            Assert.AreEqual("Logout", element.Text);
        }

        private void SignIn(string username, string password) 
        {
            // click sign in
            element = driver.FindElement(homepageSignInLink);
            element.Click();
            // enter our credentials
            element = driver.FindElement(userNameTextBox);
            element.SendKeys(username + Keys.Enter);
            element = driver.FindElement(passwordTextbox);
            element.SendKeys(password + Keys.Enter);
            // click the login button
            element = driver.FindElement(loginButton);
            element.Click();
        }
    }
}

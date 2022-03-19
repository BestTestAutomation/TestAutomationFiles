using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


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
        private readonly string homepageSignInLink = "signin";
        private readonly string userNameTextBox = "react-select-2-input";
        private readonly string passwordTextbox = "react-select-3-input";
        private readonly string loginButton = "login-btn";
        private readonly string headerUsernameText = "username";
        private readonly string errorHeader = "//*[@id=\"__next\"]/div[2]/div/form/div[2]/h3";
        private readonly string logoutLink = "logout";
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
                element = driver.FindElement(By.ClassName(headerUsernameText));
            }
            else
            {
                // verify that we have NOT signed in
                element = driver.FindElement(By.XPath(errorHeader));
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
            element = driver.FindElement(By.Id(logoutLink));
            element.Click();
            element = driver.FindElement(By.Id(homepageSignInLink));
            Assert.AreEqual("Sign In", element.Text);
        }

        private void SignIn(string username, string password) 
        {
            // click sign in
            element = driver.FindElement(By.Id(homepageSignInLink));
            element.Click();
            // enter our credentials
            element = driver.FindElement(By.Id(userNameTextBox));
            element.SendKeys(username + Keys.Enter);
            element = driver.FindElement(By.Id(passwordTextbox));
            element.SendKeys(password + Keys.Enter);
            // click the login button
            element = driver.FindElement(By.Id(loginButton));
            element.Click();
        }
    }
}

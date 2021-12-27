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

        [Test]
        public void SignInToBStackDemoValid() 
        {
            // navigate to the page
            driver.Navigate().GoToUrl("https://bstackdemo.com/");
            // click sign in
            element = driver.FindElement(By.Id("signin"));
            element.Click();
            // enter our credentials
            // element = driver.FindElement(By.Id("username"));
            element = driver.FindElement(By.Id("react-select-2-input"));
            element.SendKeys("demouser" + Keys.Enter);
            //element = driver.FindElement(By.Id("password"));
            element = driver.FindElement(By.Id("react-select-3-input"));
            element.SendKeys("testingisfun99" + Keys.Enter);
            // click the login button
            element = driver.FindElement(By.Id("login-btn"));
            element.Click();
            // verify that we have signed in
            element = driver.FindElement(By.ClassName("username"));
            string elementText = element.Text;
            Assert.AreEqual("demouser", elementText);
        }

        [Test]
        public void SignInToBStackDemoInvalidUsername()
        {
            // navigate to the page
            driver.Navigate().GoToUrl("https://bstackdemo.com/");
            // click sign in
            element = driver.FindElement(By.Id("signin"));
            element.Click();
            // enter our credentials
            // element = driver.FindElement(By.Id("username"));
            element = driver.FindElement(By.Id("react-select-2-input"));
            element.SendKeys("demouserNOT" + Keys.Enter);
            //element = driver.FindElement(By.Id("password"));
            element = driver.FindElement(By.Id("react-select-3-input"));
            element.SendKeys("testingisfun99" + Keys.Enter);
            // click the login button
            element = driver.FindElement(By.Id("login-btn"));
            element.Click();
            // verify that we have NOT signed in
            element = driver.FindElement(By.XPath("//*[@id=\"__next\"]/div[2]/div/form/div[2]/h3"));
            Assert.AreEqual("Invalid Username", element.Text);
        }

        [Test]
        public void SignInToBStackDemoInvalidPassword()
        {
            // navigate to the page
            driver.Navigate().GoToUrl("https://bstackdemo.com/");
            // click sign in
            element = driver.FindElement(By.Id("signin"));
            element.Click();
            // enter our credentials
            // element = driver.FindElement(By.Id("username"));
            element = driver.FindElement(By.Id("react-select-2-input"));
            element.SendKeys("demouser" + Keys.Enter);
            //element = driver.FindElement(By.Id("password"));
            element = driver.FindElement(By.Id("react-select-3-input"));
            element.SendKeys("testingisstillfun99" + Keys.Enter);
            // click the login button
            element = driver.FindElement(By.Id("login-btn"));
            element.Click();
            // verify that we have NOT signed in
            element = driver.FindElement(By.XPath("//*[@id=\"__next\"]/div[2]/div/form/div[2]/h3"));
            Assert.AreEqual("Invalid Password", element.Text);
        }
    }
}

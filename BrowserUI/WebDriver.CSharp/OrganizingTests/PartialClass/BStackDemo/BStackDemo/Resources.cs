using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Text;

namespace BStackDemo
{
    [TestFixture]
    public partial class TestCases
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

        private By homepageSignInLink = By.Id("signin");
        private By userNameTextBox = By.Id("react-select-2-input");
        private By passwordTextbox = By.Id("react-select-3-input");
        private By loginButton = By.Id("login-btn");
        private By headerUsernameText = By.ClassName("username");
        private By errorHeader = By.XPath("//*[@id=\"__next\"]/div[2]/div/form/div[2]/h3");
        private By logoutLink = By.Id("logout");
        private readonly string bStackUrl = "https://bstackdemo.com";

        private readonly string validUserName = "demouser";
        private readonly string validPassword = "testingisfun99";

        private By vendorCheckboxes = By.ClassName("checkmark");
        private List<string> validVendors = new List<string>() { "Apple", "Google", "OnePlus", "Samsung" };
        private By productsFound = By.ClassName("products-found");

        private By vendorItemsShown = By.ClassName("shelf-item__title");
        private static string googleItems = "Pixel 2, Pixel 3, Pixel 4";
        private static string onePlusItems = "One Plus 8, One Plus 8T, One Plus 8 Pro, One Plus 7T, One Plus 7, One Plus 6T";
        private static string appleItems = "iPhone 12, iPhone 12 Mini, iPhone 12 Pro, iPhone 12 Pro Max, iPhone 11, iPhone 11 Pro, iPhone XS, iPhone XR, iPhone XS Max";
        private static string samsungItems = "Galaxy S20, Galaxy S20+, Galaxy S20 Ultra, Galaxy S10, Galaxy S9, Galaxy Note 20, Galaxy Note 20 Ultra";
        
    }
}

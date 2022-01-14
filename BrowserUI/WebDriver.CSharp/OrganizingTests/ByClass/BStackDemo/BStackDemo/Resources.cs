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
    public class Resources
    {
        public static IWebDriver driver;
        public static IWebElement element;

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

        public static By homepageSignInLink = By.Id("signin");
        public static By userNameTextBox = By.Id("react-select-2-input");
        public static By passwordTextbox = By.Id("react-select-3-input");
        public static By loginButton = By.Id("login-btn");
        public static By headerUsernameText = By.ClassName("username");
        public static By errorHeader = By.XPath("//*[@id=\"__next\"]/div[2]/div/form/div[2]/h3");
        public static By logoutLink = By.Id("logout");
        public static string bStackUrl = "https://bstackdemo.com";

        public static string validUserName = "demouser";
        public static string validPassword = "testingisfun99";

        public static By vendorCheckboxes = By.ClassName("checkmark");
        public static List<string> validVendors = new List<string>() { "Apple", "Google", "OnePlus", "Samsung" };
        public static By productsFound = By.ClassName("products-found");

        public static By vendorItemsShown = By.ClassName("shelf-item__title");
        public static string googleItems = "Pixel 2, Pixel 3, Pixel 4";
        public static string onePlusItems = "One Plus 8, One Plus 8T, One Plus 8 Pro, One Plus 7T, One Plus 7, One Plus 6T";
        public static string appleItems = "iPhone 12, iPhone 12 Mini, iPhone 12 Pro, iPhone 12 Pro Max, iPhone 11, iPhone 11 Pro, iPhone XS, iPhone XR, iPhone XS Max";
        public static string samsungItems = "Galaxy S20, Galaxy S20+, Galaxy S20 Ultra, Galaxy S10, Galaxy S9, Galaxy Note 20, Galaxy Note 20 Ultra";
        
    }
}

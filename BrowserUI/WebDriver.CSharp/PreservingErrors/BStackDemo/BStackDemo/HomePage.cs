using System;
using System.Collections.Generic;
using System.Text;
using CoreDriver;
using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace BStackDemo
{
    class HomePage
    {
        private Core driver;
        private Header header;

        private By vendorCheckboxes = By.ClassName("checkmark");
        public static List<string> validVendors = new List<string>() { "Apple", "Google", "OnePlus", "Samsung" };
        private By productsFound = By.ClassName("products-found");

        private By vendorItemsShown = By.ClassName("shelf-item__title");
        public static string googleItems = "Pixel 2, Pixel 3, Pixel 4";
        public static string onePlusItems = "One Plus 8, One Plus 8T, One Plus 8 Pro, One Plus 7T, One Plus 7, One Plus 6T";
        public static string appleItems = "iPhone 12, iPhone 12 Mini, iPhone 12 Pro, iPhone 12 Pro Max, iPhone 11, iPhone 11 Pro, iPhone XS, iPhone XR, iPhone XS Max";
        public static string samsungItems = "Galaxy S20, Galaxy S20+, Galaxy S20 Ultra, Galaxy S10, Galaxy S9, Galaxy Note 20, Galaxy Note 20 Ultra";

        public HomePage(Core coreDriver)
        {
            driver = coreDriver;
        }
        public void SignIn(string userName, string password)
        {
            header = new Header(driver);
            SignInPage signInPage = header.StartSignIn();
            signInPage.SignIn(userName, password);
        }
        public string UsernameHeaderText
        {
            get
            {
                header = new Header(driver);
                string textValue = header.NameHeaderText;
                return textValue;
            }
            private set { }
        }
        public bool SignOut()
        {
            header = new Header(driver);
            header.SignOut();
            return header.VerifyLoggedOut();
        }
        public ReadOnlyCollection<IWebElement> VendorCheckboxes
        {
            get
            {
                driver.FindElements(vendorCheckboxes);
                return driver.Elements;
            }
            private set { }
        }
        public ReadOnlyCollection<IWebElement> VendorItemsShown
        {
            get
            {
                driver.FindElements(vendorItemsShown);
                return driver.Elements;
            }
            private set { }
        }
        public bool ProductsFoundText(string expectedResult)
        {
            return driver.FindElementTextMatchWithRetry(productsFound, expectedResult);
        }
    }
}

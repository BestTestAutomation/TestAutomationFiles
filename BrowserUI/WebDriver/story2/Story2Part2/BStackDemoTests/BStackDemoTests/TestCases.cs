using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;


namespace BStackDemoTests
{
    [TestFixture]
    public class TestCases
    {
        IWebDriver driver;
        IWebElement element;
        
        [OneTimeSetUp]
        public void Initialize() 
        {
            vendorProducts = new Dictionary<string, List<string>>();
            vendorProducts.Add("Apple", new List<string>(appleItems.Split(", ")));
            vendorProducts.Add("Google", new List<string>(googleItems.Split(", ")));
            vendorProducts.Add("OnePlus", new List<string>(onePlusItems.Split(", ")));
            vendorProducts.Add("Samsung", new List<string>(samsungItems.Split(", ")));
        }

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

        private By vendorCheckboxes = By.ClassName("checkmark");
        private List<string> validVendors = new List<string>() {"Apple", "Google", "OnePlus", "Samsung"};
        private By productsFound = By.ClassName("products-found");

        private By vendorItemsShown = By.ClassName("shelf-item__title");
        private string googleItems = "Pixel 2, Pixel 3, Pixel 4 ";
        private string onePlusItems = "One Plus 8, One Plus 8T, One Plus 8 Pro, One Plus 7T, One Plus 7, One Plus 6T";
        private string appleItems = "iPhone 12, iPhone 12 Mini, iPhone 12 Pro, iPhone 12 Pro Max, iPhone 11, iPhone 11 Pro, iPhone XS, iPhone XR, iPhone XS Max";
        private string samsungItems = "Galaxy S20, Galaxy S20+, Galaxy S20 Ultra, Galaxy S10, Galaxy S9, Galaxy Note 20, Galaxy Note 20 Ultra";
        private static Dictionary<string, List<string>> vendorProducts;
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

        private void SignIn(string username = "demouser", string password = "testingisfun99") 
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

        
        [TestCase(true, TestName = "Verify vendors signed in")]
        [TestCase(false, TestName = "Verify vendors not signed in")]
        public void VerifyVendors(bool signIn)
        {
            // navigate to the page
            driver.Navigate().GoToUrl(bStackUrl);
            if (signIn) 
            {
                SignIn();
            }
            ReadOnlyCollection<IWebElement> vendors = driver.FindElements(vendorCheckboxes);
            Assert.AreEqual(validVendors.Count, vendors.Count);
            foreach (IWebElement vendor in vendors) 
            {
                Assert.IsTrue(validVendors.Contains(vendor.Text));
            }
        }

        [Test, Retry(3)]
        [TestCase(false, "Apple", "9", TestName = "Verify number of Apple products")]
        [TestCase(false, "Google", "3", TestName = "Verify number of Google products")]
        [TestCase(false, "OnePlus", "6", TestName = "Verify number of OnePlus products")]
        [TestCase(false, "Samsung", "7", TestName = "Verify number of Samsung products")]
        [TestCase(true, "Apple", "9", TestName = "Verify number of Apple products signed in")]
        [TestCase(true, "Google", "3", TestName = "Verify number of Google products signed in")]
        [TestCase(true, "OnePlus", "6", TestName = "Verify number of OnePlus products signed in")]
        [TestCase(true, "Samsung", "7", TestName = "Verify number of Samsung products signed in")]
        public void VerifyVendorItemCount(bool signIn, string vendorName, string expectedNumberOfProducts)
        {
            string expectedResult = expectedNumberOfProducts + " Product(s) found.";
            // navigate to the page
            driver.Navigate().GoToUrl(bStackUrl);
            if (signIn)
            {
                SignIn();
            }
            ReadOnlyCollection<IWebElement> vendors = driver.FindElements(vendorCheckboxes);
            foreach (IWebElement el in vendors) 
            {
                if (el.Text == vendorName) 
                {
                    element = el;
                    break;
                }
            }
            element.Click();
            element = driver.FindElement(productsFound);
            int retriesAllowed = 10;
            while (element.Text != expectedResult && retriesAllowed > 0) 
            {
                Thread.Sleep(100);
                element = driver.FindElement(productsFound);
                --retriesAllowed;
            }
            Assert.AreEqual(expectedResult, element.Text);
        }

        [TestCase(false, "Apple", "iPhone 12, iPhone 12 Mini, iPhone 12 Pro, iPhone 12 Pro Max, iPhone 11, iPhone 11 Pro, iPhone XS, iPhone XR, iPhone XS Max")]
        [TestCase(true, "Apple", "iPhone 12, iPhone 12 Mini, iPhone 12 Pro, iPhone 12 Pro Max, iPhone 11, iPhone 11 Pro, iPhone XS, iPhone XR, iPhone XS Max")]
        [TestCase(false, "Google", "Pixel 2, Pixel 3, Pixel 4")]
        [TestCase(true, "Google", "Pixel 2, Pixel 3, Pixel 4")]
        [TestCase(false, "OnePlus", "One Plus 8, One Plus 8T, One Plus 8 Pro, One Plus 7T, One Plus 7, One Plus 6T")]
        [TestCase(true, "OnePlus", "One Plus 8, One Plus 8T, One Plus 8 Pro, One Plus 7T, One Plus 7, One Plus 6T")]
        [TestCase(false, "Samsung", "Galaxy S20, Galaxy S20+, Galaxy S20 Ultra, Galaxy S10, Galaxy S9, Galaxy Note 20, Galaxy Note 20 Ultra")]
        [TestCase(true, "Samsung", "Galaxy S20, Galaxy S20+, Galaxy S20 Ultra, Galaxy S10, Galaxy S9, Galaxy Note 20, Galaxy Note 20 Ultra")]
        public void VerifyVendorItemsShow(bool signIn, string vendor, string products)
        {
            // navigate to the page
            driver.Navigate().GoToUrl(bStackUrl);
            if (signIn)
            {
                SignIn();
            }
            ReadOnlyCollection<IWebElement> vendors = driver.FindElements(vendorCheckboxes);
            foreach (IWebElement el in vendors)
            {
                if (el.Text == vendor)
                {
                    element = el;
                    break;
                }
            }
            element.Click();
            Thread.Sleep(3000);
            ReadOnlyCollection<IWebElement> shelfItems = driver.FindElements(vendorItemsShown);
            List<string> items = new List<string>();
            string[] productsExpected = products.Split(", ");
            foreach (IWebElement shelfItem in shelfItems)
            {
                items.Add(shelfItem.Text);
            }
            Assert.AreEqual(productsExpected.Length, items.Count);
            foreach (string productExpected in productsExpected)
            {
                Assert.IsTrue(items.Contains(productExpected));
            }
        }

        private static IEnumerable<TestCaseData> VendorProductsTestData()
        {
            foreach (KeyValuePair<string, List<string>> pairs in vendorProducts) 
            {
                yield return new TestCaseData(true);
            }
        }

        #region CulledCode
        /*SignInToBStackDemo() 
         //element = driver.FindElement(By.XPath("//*[@id=\"react-select-2-input\"]"));
            //element = driver.FindElement(By.XPath("/html/body/div/div[2]/div/form/div[2]/div[1]/div/div[1]/div[2]/div/input"));
            //element = driver.FindElement(By.CssSelector("input[type='text']"));
            // element = driver.FindElement(By.XPath("/html/body/div/div[2]/div/form/div[2]/div[2]/div/div[1]/div[2]/div/input"));
         */

        /*
         [TestCase(false, "3 Product(s) found.")]
        public void VerifyGoogleItems(bool signIn, string expectedResult)
        {
            // navigate to the page
            driver.Navigate().GoToUrl(bStackUrl);
            if (signIn)
            {
                SignIn();
            }
            ReadOnlyCollection<IWebElement> vendors = driver.FindElements(vendorCheckboxes);
            foreach (IWebElement el in vendors) 
            {
                if (el.Text == "Google") 
                {
                    element = el;
                    break;
                }
            }
            element.Click();
            element = driver.FindElement(productsFound);
            int retriesAllowed = 3;
            while (element.Text != expectedResult && retriesAllowed > 0) 
            {
                Thread.Sleep(100);
                element = driver.FindElement(productsFound);
                --retriesAllowed;
            }
            Assert.AreEqual(expectedResult, element.Text);
        }*/
        #endregion
    }
}

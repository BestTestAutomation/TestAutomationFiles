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

        #region Locators
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

        #endregion

        #region SignInTests
        [TestCase("demouser", "testingisfun99", true, "demouser", TestName = "Valid sign in")]
        [TestCase("demouserNOT", "testingisfun99", false, "Invalid Username", TestName = "Invalid user name")]
        [TestCase("demouser", "badpassword", false, "Invalid Password", TestName = "Invalid user password")]
        [TestCase("", "", false, "Invalid Username", TestName = "Sign in using blank credentials")]
        [TestCase("", "testingisfun99", false, "Invalid Username", TestName = "Sign in using blank username")]
        [TestCase("demouser", "", false, "Invalid Password", TestName = "Sign in using blank password")]
        public void SignInTestCases(string userName, string password, bool isValid, string expectedResult)
        {
            driver.Navigate().GoToUrl(bStackUrl);
            SignIn(userName, password);
            if (isValid)
            {
                element = driver.FindElement(headerUsernameText);
            }
            else
            {
                element = driver.FindElement(errorHeader);
            }
            string elementText = element.Text;
            Assert.AreEqual(expectedResult, elementText);
        }


        [TestCase("demouser", "testingisfun99", TestName = "Logout")]
        public void Logout(string userName, string password)
        {
            // navigate to the page
            driver.Navigate().GoToUrl(bStackUrl);
            SignIn(userName, password);
            element = driver.FindElement(logoutLink);
            element.Click();
            element = driver.FindElement(homepageSignInLink);
            Assert.AreEqual("Sign In", element.Text);
        }
        #endregion

        #region HelperMethods
        private void SignIn(string userName, string password) 
        {
            // click sign in
            element = driver.FindElement(homepageSignInLink);
            element.Click();
            // enter our credentials
            element = driver.FindElement(userNameTextBox);
            element.SendKeys(userName + Keys.Enter);
            element = driver.FindElement(passwordTextbox);
            element.SendKeys(password + Keys.Enter);
            // click the login button
            element = driver.FindElement(loginButton);
            element.Click();
        }
        #endregion

        #region HomePageTests
        [TestCase(true, TestName = "Verify vendors signed in")]
        [TestCase(false, TestName = "Verify vendors not signed in")]
        public void VerifyVendors(bool signIn)
        {
            // navigate to the page
            driver.Navigate().GoToUrl(bStackUrl);
            if (signIn)
            {
                SignIn(validUserName, validPassword);
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
                SignIn(validUserName, validPassword);
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

        private static IEnumerable<TestCaseData> VendorProductsTestData()
        {
            yield return new TestCaseData(true, "Apple", appleItems).SetName("Verify Apple items shown, signed in " + true);
            yield return new TestCaseData(false, "Apple", appleItems).SetName("Verify Apple items shown, signed in " + false);
            yield return new TestCaseData(true, "Google", googleItems).SetName("Verify Google items shown, signed in " + true);
            yield return new TestCaseData(false, "Google", googleItems).SetName("Verify Google items shown, signed in " + false);
            yield return new TestCaseData(true, "OnePlus", onePlusItems).SetName("Verify OnePlus items shown, signed in " + true);
            yield return new TestCaseData(false, "OnePlus", onePlusItems).SetName("Verify OnePlus items shown, signed in " + false);
            yield return new TestCaseData(true, "Samsung", samsungItems).SetName("Verify Samsung items shown, signed in " + true);
            yield return new TestCaseData(false, "Samsung", samsungItems).SetName("Verify Samsung items shown, signed in " + false);
        }

        private static IEnumerable<TestCaseData> VendorProductsCombinedTestData()
        {
            Dictionary<string, string> combinations = new Dictionary<string, string>();
            combinations.Add("Apple, Google", new StringBuilder().AppendJoin(", ", appleItems, googleItems).ToString());
            combinations.Add("Apple, OnePlus", new StringBuilder().AppendJoin(", ", appleItems, onePlusItems).ToString());
            combinations.Add("Apple, Samsung", new StringBuilder().AppendJoin(", ", appleItems, samsungItems).ToString());            // Apple, Google, OnePlus
            combinations.Add("Apple, Google, OnePlus", new StringBuilder().AppendJoin(", ", appleItems, googleItems, onePlusItems).ToString());
            combinations.Add("Apple, Google, Samsung", new StringBuilder().AppendJoin(", ", appleItems, googleItems, samsungItems).ToString());
            combinations.Add("Apple, OnePlus, Samsung", new StringBuilder().AppendJoin(", ", appleItems, onePlusItems, samsungItems).ToString());
            combinations.Add("Apple, Google, OnePlus, Samsung", new StringBuilder().AppendJoin(", ", appleItems, googleItems, onePlusItems, samsungItems).ToString());
            combinations.Add("Google, OnePlus", new StringBuilder().AppendJoin(", ", googleItems, onePlusItems).ToString());
            combinations.Add("Google, Samsung", new StringBuilder().AppendJoin(", ", googleItems, samsungItems).ToString());
            combinations.Add("Google, OnePlus, Samsung", new StringBuilder().AppendJoin(", ", googleItems, onePlusItems, samsungItems).ToString());
            combinations.Add("OnePlus, Samsung", new StringBuilder().AppendJoin(", ", onePlusItems, samsungItems).ToString());
            combinations.Add("default", new StringBuilder().AppendJoin(", ", appleItems, googleItems, onePlusItems, samsungItems).ToString());
            foreach (KeyValuePair<string, string> pair in combinations)
            {
                yield return new TestCaseData(true, pair.Key, pair.Value).SetName("Verify " + pair.Key + " items shown, signed in " + true);
                yield return new TestCaseData(false, pair.Key, pair.Value).SetName("Verify " + pair.Key + " items shown, signed in " + false);
            }
        }

        [Test, Retry(3)]
        [TestCaseSource("VendorProductsCombinedTestData")]
        [TestCaseSource("VendorProductsTestData")]
        public void VerifyCombinedVendorItemsShow(bool signIn, string vendors, string products)
        {
            // navigate to the page
            driver.Navigate().GoToUrl(bStackUrl);
            if (signIn)
            {
                SignIn(validUserName, validPassword);
            }
            ReadOnlyCollection<IWebElement> vendorsToCheck = driver.FindElements(vendorCheckboxes);
            foreach (IWebElement el in vendorsToCheck)
            {
                if (vendors.Contains(el.Text))
                {
                    el.Click();
                }
            }
            Thread.Sleep(3000);
            ReadOnlyCollection<IWebElement> shelfItems = driver.FindElements(vendorItemsShown);
            List<string> items = new List<string>();
            foreach (IWebElement shelfItem in shelfItems)
            {
                items.Add(shelfItem.Text);
            }
            int productCount = 0;
            foreach (string product in products.Split(", "))
            {
                Assert.IsTrue(items.Contains(product));
                productCount++;
            }
            Assert.AreEqual(productCount, items.Count);
        }

        [Test]
        public void VerifyProductsCanBeAddedToCart() { }

        [Test]
        public void VerifyCheckoutProcess() { }

        #endregion

        #region FavoritesTests
        [Test]
        public void VerifySelectedFavoritesShow() { }

        [Test]
        public void VerifyFavoritesCanBeRemoved() { }

        #endregion

        #region ShoppingBagTests
        [Test]
        public void VerifyProductsCanBeRemovedFromCart() { }
        #endregion

        #region CheckoutTest
        [Test]
        public void VerifyCheckout() { }

        #endregion

        #region OffersTests
        #endregion
    }
}

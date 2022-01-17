using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Text;
using CoreDriver;

namespace BStackDemo
{
    [TestFixture]
    public class TestCases
    {
        Core driver;

        [SetUp]
        public void Setup() 
        {
            driver = new Core();
            driver.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [TearDown]
        public void Teardown() 
        {
            driver.Quit();
        }

        #region Locators
        private readonly string bStackUrl = "https://bstackdemo.com";


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
            string actualResult = string.Empty;
            driver.Navigate(bStackUrl);
            HomePage homePage = new HomePage(driver);
            homePage.SignIn(userName, password);
            if (isValid)
            {
                actualResult = homePage.UsernameHeaderText;

            }
            else
            {
                SignInPage signInPage = new SignInPage(driver);
                actualResult = signInPage.ErrorHeaderText;
            }
            string elementText = driver.Element.Text;
            Assert.AreEqual(expectedResult, actualResult);
        }


        [TestCase("demouser", "testingisfun99", TestName = "Logout")]
        public void Logout(string userName, string password)
        {
            // navigate to the page
            driver.Navigate(bStackUrl);
            HomePage homePage = new HomePage(driver);
            homePage.SignIn(userName, password);
            Assert.IsTrue(homePage.SignOut());
        }
        #endregion

        #region HelperMethods
        #endregion

        #region HomePageTests
        [TestCase(true, TestName = "Verify vendors signed in")]
        [TestCase(false, TestName = "Verify vendors not signed in")]
        public void VerifyVendors(bool signIn)
        {
            // navigate to the page
            driver.Navigate(bStackUrl);
            HomePage homePage = new HomePage(driver);
            if (signIn)
            {
                homePage.SignIn(SignInPage.ValidUserName, SignInPage.ValidPassword);
            }
            ReadOnlyCollection<IWebElement> vendorsFound = homePage.VendorCheckboxes;
            Assert.AreEqual(HomePage.validVendors.Count, vendorsFound.Count);
            foreach (IWebElement vendor in driver.Elements)
            {
                Assert.IsTrue(HomePage.validVendors.Contains(vendor.Text));
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
            driver.Navigate(bStackUrl);
            HomePage homePage = new HomePage(driver);
            if (signIn)
            {
                homePage.SignIn(SignInPage.ValidUserName, SignInPage.ValidPassword);
            }
            ReadOnlyCollection<IWebElement> vendorsFound = homePage.VendorCheckboxes;
            foreach (IWebElement el in vendorsFound)
            {
                if (el.Text == vendorName)
                {
                    el.Click();
                    break;
                }
            }
            Assert.IsTrue(homePage.ProductsFoundText(expectedResult), driver.GetAllErrors());
        }

        private static IEnumerable<TestCaseData> VendorProductsTestData()
        {
            yield return new TestCaseData(true, "Apple", HomePage.appleItems).SetName("Verify Apple items shown, signed in " + true);
            yield return new TestCaseData(false, "Apple", HomePage.appleItems).SetName("Verify Apple items shown, signed in " + false);
            yield return new TestCaseData(true, "Google", HomePage.googleItems).SetName("Verify Google items shown, signed in " + true);
            yield return new TestCaseData(false, "Google", HomePage.googleItems).SetName("Verify Google items shown, signed in " + false);
            yield return new TestCaseData(true, "OnePlus", HomePage.onePlusItems).SetName("Verify OnePlus items shown, signed in " + true);
            yield return new TestCaseData(false, "OnePlus", HomePage.onePlusItems).SetName("Verify OnePlus items shown, signed in " + false);
            yield return new TestCaseData(true, "Samsung", HomePage.samsungItems).SetName("Verify Samsung items shown, signed in " + true);
            yield return new TestCaseData(false, "Samsung", HomePage.samsungItems).SetName("Verify Samsung items shown, signed in " + false);
        }

        private static IEnumerable<TestCaseData> VendorProductsCombinedTestData()
        {
            Dictionary<string, string> combinations = new Dictionary<string, string>();
            combinations.Add("Apple, Google", new StringBuilder().AppendJoin(", ", HomePage.appleItems, HomePage.googleItems).ToString());
            combinations.Add("Apple, OnePlus", new StringBuilder().AppendJoin(", ", HomePage.appleItems, HomePage.onePlusItems).ToString());
            combinations.Add("Apple, Samsung", new StringBuilder().AppendJoin(", ", HomePage.appleItems, HomePage.samsungItems).ToString());
            combinations.Add("Apple, Google, OnePlus", new StringBuilder().AppendJoin(", ", HomePage.appleItems, HomePage.googleItems, HomePage.onePlusItems).ToString());
            combinations.Add("Apple, Google, Samsung", new StringBuilder().AppendJoin(", ", HomePage.appleItems, HomePage.googleItems, HomePage.samsungItems).ToString());
            combinations.Add("Apple, OnePlus, Samsung", new StringBuilder().AppendJoin(", ", HomePage.appleItems, HomePage.onePlusItems, HomePage.samsungItems).ToString());
            combinations.Add("Apple, Google, OnePlus, Samsung", new StringBuilder().AppendJoin(", ", HomePage.appleItems, HomePage.googleItems, HomePage.onePlusItems, HomePage.samsungItems).ToString());
            combinations.Add("Google, OnePlus", new StringBuilder().AppendJoin(", ", HomePage.googleItems, HomePage.onePlusItems).ToString());
            combinations.Add("Google, Samsung", new StringBuilder().AppendJoin(", ", HomePage.googleItems, HomePage.samsungItems).ToString());
            combinations.Add("Google, OnePlus, Samsung", new StringBuilder().AppendJoin(", ", HomePage.googleItems, HomePage.onePlusItems, HomePage.samsungItems).ToString());
            combinations.Add("OnePlus, Samsung", new StringBuilder().AppendJoin(", ", HomePage.onePlusItems, HomePage.samsungItems).ToString());
            combinations.Add("default", new StringBuilder().AppendJoin(", ", HomePage.appleItems, HomePage.googleItems, HomePage.onePlusItems, HomePage.samsungItems).ToString());
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
            driver.Navigate(bStackUrl);
            HomePage homePage = new HomePage(driver);
            if (signIn)
            {
                homePage.SignIn(SignInPage.ValidUserName, SignInPage.ValidPassword);
            }
            ReadOnlyCollection<IWebElement> vendorsFound = homePage.VendorCheckboxes;
            foreach (IWebElement el in vendorsFound)
            {
                if (vendors.Contains(el.Text))
                {
                    el.Click();
                }
            }
            Thread.Sleep(3000);
            
            List<string> items = new List<string>();
            foreach (IWebElement shelfItem in homePage.VendorItemsShown)
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

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
    class HomePageTests
    {
        IWebDriver driver = Resources.driver;
        IWebElement element = Resources.element;

        [TestCase(true, TestName = "Verify vendors signed in")]
        [TestCase(false, TestName = "Verify vendors not signed in")]
        public void VerifyVendors(bool signIn)
        {
            // navigate to the page
            driver.Navigate().GoToUrl(Resources.bStackUrl);
            if (signIn)
            {
                SignInTests.SignIn(Resources.validUserName, Resources.validPassword);
            }
            ReadOnlyCollection<IWebElement> vendors = driver.FindElements(Resources.vendorCheckboxes);
            Assert.AreEqual(Resources.validVendors.Count, vendors.Count);
            foreach (IWebElement vendor in vendors)
            {
                Assert.IsTrue(Resources.validVendors.Contains(vendor.Text));
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
            driver.Navigate().GoToUrl(Resources.bStackUrl);
            if (signIn)
            {
                SignInTests.SignIn(Resources.validUserName, Resources.validPassword);
            }
            ReadOnlyCollection<IWebElement> vendors = driver.FindElements(Resources.vendorCheckboxes);
            foreach (IWebElement el in vendors)
            {
                if (el.Text == vendorName)
                {
                    element = el;
                    break;
                }
            }
            element.Click();
            element = driver.FindElement(Resources.productsFound);
            int retriesAllowed = 10;
            while (element.Text != expectedResult && retriesAllowed > 0)
            {
                Thread.Sleep(100);
                element = driver.FindElement(Resources.productsFound);
                --retriesAllowed;
            }
            Assert.AreEqual(expectedResult, element.Text);
        }

        private static IEnumerable<TestCaseData> VendorProductsTestData()
        {
            yield return new TestCaseData(true, "Apple", Resources.appleItems).SetName("Verify Apple items shown, signed in " + true);
            yield return new TestCaseData(false, "Apple", Resources.appleItems).SetName("Verify Apple items shown, signed in " + false);
            yield return new TestCaseData(true, "Google", Resources.googleItems).SetName("Verify Google items shown, signed in " + true);
            yield return new TestCaseData(false, "Google", Resources.googleItems).SetName("Verify Google items shown, signed in " + false);
            yield return new TestCaseData(true, "OnePlus", Resources.onePlusItems).SetName("Verify OnePlus items shown, signed in " + true);
            yield return new TestCaseData(false, "OnePlus", Resources.onePlusItems).SetName("Verify OnePlus items shown, signed in " + false);
            yield return new TestCaseData(true, "Samsung", Resources.samsungItems).SetName("Verify Samsung items shown, signed in " + true);
            yield return new TestCaseData(false, "Samsung", Resources.samsungItems).SetName("Verify Samsung items shown, signed in " + false);
        }

        private static IEnumerable<TestCaseData> VendorProductsCombinedTestData()
        {
            Dictionary<string, string> combinations = new Dictionary<string, string>();
            combinations.Add("Apple, Google", new StringBuilder().AppendJoin(", ", Resources.appleItems, Resources.googleItems).ToString());
            combinations.Add("Apple, OnePlus", new StringBuilder().AppendJoin(", ", Resources.appleItems, Resources.onePlusItems).ToString());
            combinations.Add("Apple, Samsung", new StringBuilder().AppendJoin(", ", Resources.appleItems, Resources.samsungItems).ToString());            // Apple, Google, OnePlus
            combinations.Add("Apple, Google, OnePlus", new StringBuilder().AppendJoin(", ", Resources.appleItems, Resources.googleItems, Resources.onePlusItems).ToString());
            combinations.Add("Apple, Google, Samsung", new StringBuilder().AppendJoin(", ", Resources.appleItems, Resources.googleItems, Resources.samsungItems).ToString());
            combinations.Add("Apple, OnePlus, Samsung", new StringBuilder().AppendJoin(", ", Resources.appleItems, Resources.onePlusItems, Resources.samsungItems).ToString());
            combinations.Add("Apple, Google, OnePlus, Samsung", new StringBuilder().AppendJoin(", ", Resources.appleItems, Resources.googleItems, Resources.onePlusItems, Resources.samsungItems).ToString());
            combinations.Add("Google, OnePlus", new StringBuilder().AppendJoin(", ", Resources.googleItems, Resources.onePlusItems).ToString());
            combinations.Add("Google, Samsung", new StringBuilder().AppendJoin(", ", Resources.googleItems, Resources.samsungItems).ToString());
            combinations.Add("Google, OnePlus, Samsung", new StringBuilder().AppendJoin(", ", Resources.googleItems, Resources.onePlusItems, Resources.samsungItems).ToString());
            combinations.Add("OnePlus, Samsung", new StringBuilder().AppendJoin(", ", Resources.onePlusItems, Resources.samsungItems).ToString());
            combinations.Add("default", new StringBuilder().AppendJoin(", ", Resources.appleItems, Resources.googleItems, Resources.onePlusItems, Resources.samsungItems).ToString());
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
            driver.Navigate().GoToUrl(Resources.bStackUrl);
            if (signIn)
            {
                SignInTests.SignIn(Resources.validUserName, Resources.validPassword);
            }
            ReadOnlyCollection<IWebElement> vendorsToCheck = driver.FindElements(Resources.vendorCheckboxes);
            foreach (IWebElement el in vendorsToCheck)
            {
                if (vendors.Contains(el.Text))
                {
                    el.Click();
                }
            }
            Thread.Sleep(3000);
            ReadOnlyCollection<IWebElement> shelfItems = driver.FindElements(Resources.vendorItemsShown);
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
    }
}

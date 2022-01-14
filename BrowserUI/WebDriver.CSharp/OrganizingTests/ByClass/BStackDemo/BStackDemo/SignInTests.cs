using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
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
    class SignInTests
    {
        IWebDriver driver = Resources.driver;
        IWebElement element = Resources.element;

        [TestCase("demouser", "testingisfun99", true, "demouser", TestName = "Valid sign in")]
        [TestCase("demouserNOT", "testingisfun99", false, "Invalid Username", TestName = "Invalid user name")]
        [TestCase("demouser", "badpassword", false, "Invalid Password", TestName = "Invalid user password")]
        [TestCase("", "", false, "Invalid Username", TestName = "Sign in using blank credentials")]
        [TestCase("", "testingisfun99", false, "Invalid Username", TestName = "Sign in using blank username")]
        [TestCase("demouser", "", false, "Invalid Password", TestName = "Sign in using blank password")]
        public void SignInTestCases(string userName, string password, bool isValid, string expectedResult)
        {
            Resources.driver.Navigate().GoToUrl(Resources.bStackUrl);
            SignIn(userName, password);
            if (isValid)
            {
                element = driver.FindElement(Resources.headerUsernameText);
            }
            else
            {
                element = driver.FindElement(Resources.errorHeader);
            }
            string elementText = Resources.element.Text;
            Assert.AreEqual(expectedResult, elementText);
        }


        [TestCase("demouser", "testingisfun99", TestName = "Logout")]
        public void Logout(string userName, string password)
        {
            // navigate to the page
            driver.Navigate().GoToUrl(Resources.bStackUrl);
            SignIn(userName, password);
            element = driver.FindElement(Resources.logoutLink);
            element.Click();
            element = driver.FindElement(Resources.homepageSignInLink);
            Assert.AreEqual("Sign In", Resources.element.Text);
        }

        public static void SignIn(string userName, string password)
        {
            // click sign in
            Resources.element = Resources.driver.FindElement(Resources.homepageSignInLink);
            Resources.element.Click();
            // enter our credentials
            Resources.element = Resources.driver.FindElement(Resources.userNameTextBox);
            Resources.element.SendKeys(userName + Keys.Enter);
            Resources.element = Resources.driver.FindElement(Resources.passwordTextbox);
            Resources.element.SendKeys(password + Keys.Enter);
            // click the login button
            Resources.element = Resources.driver.FindElement(Resources.loginButton);
            Resources.element.Click();
        }
    }
}

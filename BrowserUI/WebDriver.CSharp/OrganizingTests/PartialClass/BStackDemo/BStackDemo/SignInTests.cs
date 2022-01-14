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
    public partial class TestCases
    {
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
    }
}

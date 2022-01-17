using System;
using System.Collections.Generic;
using System.Text;
using CoreDriver;
using OpenQA.Selenium;

namespace BStackDemo
{
    class Header
    {
        private Core driver;
        private By homepageSignInLink = By.Id("signin");
        private By headerUsernameText = By.ClassName("username");
        private By logoutLink = By.Id("logout");
        public Header(Core coreDriver)
        {
            driver = coreDriver;
        }
        public SignInPage StartSignIn()
        {
            driver.FindElement(homepageSignInLink);
            driver.Element.Click();
            return new SignInPage(driver);
        }
        public string NameHeaderText
        {
            get
            {
                driver.FindElement(headerUsernameText);
                string textValue = driver.Element.Text;
                return textValue;
            }
            private set { }
        }
        public void SignOut()
        {
            driver.FindElement(logoutLink);
            driver.Element.Click();
        }
        public bool VerifyLoggedOut()
        {
            bool success = false;
            try
            {
                driver.FindElement(homepageSignInLink);
                success = true;
            }
            catch (Exception e)
            {
                driver.Errors.Add(e.Message);
                driver.Errors.Add(e.StackTrace);
            }
            return success;
        }
    }
}

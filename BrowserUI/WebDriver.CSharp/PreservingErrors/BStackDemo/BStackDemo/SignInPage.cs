using System;
using System.Collections.Generic;
using System.Text;
using CoreDriver;
using OpenQA.Selenium;

namespace BStackDemo
{
    class SignInPage
    {
        private Core driver;

        private By userNameTextBox = By.Id("react-select-2-input");
        private By passwordTextbox = By.Id("react-select-3-input");
        private By loginButton = By.Id("login-btn");
        private By errorHeader = By.XPath("//*[@id=\"__next\"]/div[2]/div/form/div[2]/h3");
        private static string validUserName = "demouser";
        private static string validPassword = "testingisfun99";

        public SignInPage(Core coreDriver)
        {
            driver = coreDriver;
        }
        public void SignIn(string userName, string password)
        {
            // enter our credentials
            driver.FindElement(userNameTextBox);
            driver.Element.SendKeys(userName + Keys.Enter);
            driver.FindElement(passwordTextbox);
            driver.Element.SendKeys(password + Keys.Enter);
            // click the login button
            driver.FindElement(loginButton);
            driver.Element.Click();
        }
        public string ErrorHeaderText
        {
            get
            {
                driver.FindElement(errorHeader);
                string textValue = driver.Element.Text;
                return textValue;
            }
            private set { }
        }
        public static string ValidUserName
        {
            get
            {
                return validUserName;
            }
            private set { }
        }

        public static string ValidPassword
        {
            get
            {
                return validPassword;
            }
            private set { }
        }
    }
}

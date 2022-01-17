using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;
using System.Threading;

namespace CoreDriver
{
    public class Core
    {
        private IWebDriver coreDriver;
        private IWebElement element;
        private ReadOnlyCollection<IWebElement> elements;
        public IWebDriver Driver
        {
            get { return coreDriver; }
            private set { }
        }
        public IWebElement Element
        {
            get { return element; }
            private set { }
        }
        public ReadOnlyCollection<IWebElement> Elements
        {
            get { return elements; }
            private set { }
        }

        public Core()
        {
            coreDriver = new ChromeDriver();
        }
        public void Quit()
        {
            coreDriver.Quit();
        }
        public void Navigate(string url)
        {
            coreDriver.Navigate().GoToUrl(url);
        }
        public void FindElement(By locator)
        {
            element = coreDriver.FindElement(locator);
        }

        public void FindElements(By locator)
        {
            elements = coreDriver.FindElements(locator);
        }
        public bool FindElementTextMatchWithRetry(By locator, string textToMatch, int numberOfRetries = 10)
        {
            bool found = false;
            FindElement(locator);
            while (!found && numberOfRetries > 0)
            {
                if (Element.Text == textToMatch)
                {
                    found = true;
                }
                else
                {
                    Thread.Sleep(100);
                    FindElement(locator);
                    --numberOfRetries;
                }
            }
            return found;
        }
    }
}

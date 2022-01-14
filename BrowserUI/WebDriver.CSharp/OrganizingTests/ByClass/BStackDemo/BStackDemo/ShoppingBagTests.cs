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
    class ShoppingBagTests
    {
        IWebDriver driver = Resources.driver;
        IWebElement element = Resources.element;
        
        [Test]
        public void VerifyProductsCanBeRemovedFromCart() { }
    }
}

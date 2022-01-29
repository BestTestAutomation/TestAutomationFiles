﻿using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;


namespace BStackDemoTests
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
        }

        [TearDown]
        public void Teardown() 
        {
            driver.Quit();
        }

        [Test]
        public void FirstTest() { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Testing
{


    
    public class Browsertest
    {
        IWebDriver driver;

        [SetUp]
        public void init()
        {
            driver = new FirefoxDriver();
        }
        [TearDown]
        public void destroy()
        {
            driver.Dispose();
        }

        [Test]
        public void simplesearch()
        {
            //Given
            driver.Navigate().GoToUrl("https://www.google.com");
            Assert.IsTrue(driver.FindElement(By.Id("hplogo")).Displayed, "logo not found");

            //When
            driver.FindElement(By.Name("q")).Clear();
            driver.FindElement(By.Name("q")).SendKeys("oiuyyg976f867f865fdy6t7fd65d5674sd56rdytuf");
            driver.FindElement(By.Name("btnG")).Click();
            Console.WriteLine("Hello World!");
            //Then
            //System.Threading.Thread.Sleep(10000);
            Assert.IsTrue(ClassElementDisplayed("g", driver), "results not found");
        }

        public bool ClassElementDisplayed(string classname, IWebDriver driver)
        {
            bool foundelement = false;
            try
            {
                foundelement = driver.FindElement(By.ClassName(classname)).Displayed;

            }
            catch (NoSuchElementException)
            {
                foundelement = false;
            }

            return foundelement;

        }


    }
}

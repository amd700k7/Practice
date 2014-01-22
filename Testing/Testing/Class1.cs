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
    public class Browsertest    //class to test searching in a firefox browser
    {
        IWebDriver driver;      //create web driver

        [SetUp]
        public void init()      //method to initialize driver
        {
            driver = new FirefoxDriver();   //define driver and open a new firefox browser
        }
        [TearDown]
        public void destroy()   //method to close the browser
        {
            driver.Dispose();   //kill the firefox browser
        }

        [Test]                  //mark following method for testing
        public void simplesearch()  //create method to perform a google search
        {
            string samplestring = "sample";
            int pages = 3;                      //max number of pages to search
            int pagenumber = 1;                 //track number of pages searched
            bool nextpage = false;              //to simplify displayed results

            //Given
            driver.Navigate().GoToUrl("https://www.google.com");    //tell webpage to go to a website
            Assert.IsTrue(driver.FindElement(By.Id("hplogo")).Displayed, "logo not found"); //Test to verify that website was loaded

            //When
            driver.FindElement(By.Name("q")).Clear();   //locate search box element and clear it of any text
//            driver.FindElement(By.Name("q")).SendKeys("abcasdfewawef");    //locate search box element and insert a string (1 page sample)
            driver.FindElement(By.Name("q")).SendKeys("dominion");    //locate search box element and insert a string (multi page sample)

            driver.FindElement(By.Name("btnG")).Click();    //locate search button element and click it

            //Then
            System.Threading.Thread.Sleep(5000);         //delay for 5000ms to visually verify page results; also to allow for page to load
            Assert.IsTrue(ClassElementDisplayed("g", driver), "CLASS results not found"); //Test to verify results were found

            nextpage = driver.FindElement(By.Id("pnnext")).Displayed;

            if (nextpage == true)       //are more results found?
            {
                do//if more results are found
                {

                    Console.Write("Page ");              //print to console
                    Console.Write(pagenumber);              //print current page count to console
                    Console.WriteLine(" has been displayed");              //print to console


                    GoNext(driver);     //go to the next page
                    pages--;            //decrease number of pages to search through
                    pagenumber++;       //increases searched page count

                } while (driver.FindElement(By.ClassName("pn")).Displayed & pages != 0);   //search at most 3 pages in google and print to console the page numbers
            }
            else if (nextpage == false) //if no results found
            {
                Console.Write("Page ");              //print to console
                Console.Write(pagenumber);              //print to console
                Console.WriteLine(" has been displayed");              //print to console
            }
            Assert.IsTrue(ClassElementDisplayed("pn", driver), "ID results not found");   //test to verify 'next' button found
                
        }

        public bool ClassElementDisplayed(string classname, IWebDriver driver)  //method to check the class element we searched for
        {
            bool foundelement = false;      //create and initialize 'foundelement'
            try
            {
                foundelement = driver.FindElement(By.ClassName(classname)).Displayed;   //set foundelement 

            }
            catch (NoSuchElementException)  //look for an error exception
            {
                foundelement = false;       //if an exception was found, set foundelement to false
            }

            return foundelement;

        }

        public bool IdElementDisplayed(string idname, IWebDriver driver) //method to check the id element we searched for
        {
            bool foundid = false;   //create and initialize 'foundid'
            try
            {
                foundid = driver.FindElement(By.Id(idname)).Displayed;  //set foundid
            }
            catch (NoSuchElementException)  //look for an error exception
            {
                foundid = false;            //if an exception was found, set foundid to false
            }
            return foundid;
        }

        public void GoNext(IWebDriver driver)           //method to tell Google to go to the next page of search elements
        {
            driver.FindElement(By.Id("pnnext")).Click();    //click the next button
            System.Threading.Thread.Sleep(5000);         //delay for 5000ms to visually verify page results; also to allow for page to load
        }
        
    }
}

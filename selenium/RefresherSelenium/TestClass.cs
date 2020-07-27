
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Office.Interop.Excel;





namespace RefresherSelenium
{
    [TestFixture]
    class TestClass : Utilities
    {
       

        [Test]
        [Category("Sample")]
       // [Author("shrawan")]
        public void SampleLoginandLogoff()
        {
            //test yet to Implement 
            IWebElement element = driver.FindElement(By.ClassName("menusubnav")).FindElement(By.XPath("//a[@href = 'addAccount.php']"));
            Javascriptclick(element);
            var ddlItem = driver.FindElement(By.XPath("//td/select"));
            var SelectItem = new SelectElement(ddlItem);
            SelectItem.SelectByText("current");

            // element = driver.FindElement(By.XPath("/html/body/div[3]/div/ul/li[10]/a"));
            
            //Javascriptclick(element);

           driver.FindElement(By.XPath("//div[3]/div/ul/li[10]")).Click();
            Assert.Fail("this test passed intensionally ");
            IAlert alert = driver.SwitchTo().Alert();
            var text = alert.Text;
            Console.WriteLine(text);
            alert.Accept();

        }

        [Test]
        [Category("Waits")]
        public void ExplicitWaitImplementation()
        {
            //implementation
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement element = driver.FindElement(By.ClassName("menusubnav")).FindElement(By.XPath("//a[@href = 'addAccount.php']"));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.ClassName("menusubnav")));
            Javascriptclick(element);
           // WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var ddlItem = driver.FindElement(By.XPath("//td/select"));
            var SelectItem = new SelectElement(ddlItem);
            SelectItem.SelectByText("current");
            string Logoutbutton = "//div[3]/div/ul/li[10]";
            IWebElement logout = 
                wait.Until(
                    SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
                        By.XPath(Logoutbutton)));

           // logout.Click();
            driver.FindElement(By.XPath("//div[3]/div/ul/li[10]")).Click();
            IAlert alert = driver.SwitchTo().Alert();
            var text = alert.Text;
            Console.WriteLine(text);
            alert.Accept();

        }

        #region end of tests 
        #endregion
    }
}

using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;





namespace RefresherSelenium
{
    public class LoginPage
    {
      IWebDriver driver = new ChromeDriver();
     //   Utilities utils = new Utilities();



        //IWebDriver driver;

        /// <summary>
        /// url 
        ///   IWebDriver driver;

        /// 
        /// </summary>
        


        public void Login()
        {
            driver.FindElement(By.XPath("//*[@id='bySelection']/div[2]")).Click();
            driver.FindElement(By.XPath("//*[@id='userNameInput']")).SendKeys("usqaex\\yesravankumar");
            driver.FindElement(By.XPath("//*[@id='passwordInput']")).SendKeys("FQ#63zr83r");
            ////*[@id="passwordInput"]
          //  driver.Manage().Window.Maximize();
        }

        

        public void Ddltest()
        {

            IWebElement element = driver.FindElement(By.ClassName("menusubnav")).FindElement(By.XPath("//a[@href = 'addAccount.php']"));
            //Javascriptclick(element);


            //Actions actions = new Actions(driver);
            //actions.MoveToElement(element).Click().Build().Perform();



            var ddlItem = driver.FindElement(By.XPath("//td/select"));
            var SelectItem = new SelectElement(ddlItem);
            SelectItem.SelectByText("current");

            //var ParentWind = driver.CurrentWindowHandle;
            ////Click
            //var newWinddow = driver.CurrentWindowHandle;

            //driver.SwitchTo().Window(newWinddow);
            //driver.Close();
            //driver.SwitchTo().Window(ParentWind);


        }

        



       

    }


}

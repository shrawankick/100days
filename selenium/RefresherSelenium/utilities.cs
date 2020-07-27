using Microsoft.Office.Interop.Excel;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using excel = Microsoft.Office.Interop.Excel;

namespace RefresherSelenium
{
    public class Utilities
    {

        public static IWebDriver driver;

        public static IWebDriver Driver
        {
            get
            {
                if (driver == null)
                    throw new NullReferenceException("The WebDriver browser instance was not initialized. You should first call the method InitBrowser.");
                return driver;
            }
            private set
            {
                driver = value;
            }
        }

        /// <summary>
        /// This is the starting point of the test 
        /// </summary>
        [SetUp]
        public void LoginToAPP()
        {
            Navigateurl();
            Waitforsomesec(30);
            loginToApplacation();
            Waitforsomesec(30);
            Console.WriteLine("setup completed");
        }
        [TearDown]
        public void Close()
        {
            TakesScreenshotOnFailedTest();
            driver.Close();
            driver.Quit();
        }


        /// <summary>
        /// this method will open the driver 
        /// and maximize the window 
        /// get the URL form the App.config file 
        /// 
        /// </summary>
        public void Navigateurl()
        {
            string url;
            driver = new ChromeDriver();
            //driver = new EdgeDriver();
            driver.Manage().Window.Maximize();
            // we can 
            string Env = ConfigurationManager.AppSettings["Env"];
            url = EnvromentUrl(Env);
            //url = ConfigurationManager.AppSettings["QAURL"];
            driver.Navigate().GoToUrl(url);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        public string EnvromentUrl(string Env)
        {
            string URL = Env == "QA" ? ConfigurationManager.AppSettings["QAURL"] : ConfigurationManager.AppSettings["stageURL"];
            return URL;
        }

        /// <summary>
        /// we will login to with user name and password which are loaded from app.config
        ///
        /// </summary>
        public void Login()
        {


            driver.FindElement(
               By.XPath("//*[@name='uid']")).SendKeys(
                   ConfigurationManager.AppSettings["UserName"]);
            driver.FindElement(
                By.XPath("//*[@name='password']")).SendKeys(
                    ConfigurationManager.AppSettings["Password"]);


        }
        /// <summary>
        /// we will login to the application based on the Credentials
        /// and Click on submit button 
        /// </summary>
        public void loginToApplacation()
        {

            Login();
            driver.FindElement(By.XPath("//*[@name='btnLogin']")).Click();
            //Waitforsomesec(70);
            // driver.FindElement(By.XPath("//*[@id='submitButton']")).Click();
            Waitforsomesec(30);
        }



        /// <summary>
        /// we wait for some duration to make sure page is loading 
        /// </summary>
        /// <param name="sec">duration</param>
        public void Waitforsomesec(int sec)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(sec));
        }
        /// <summary>
        /// this is short form of the driver.FindElement(By.XPath(...)
        /// just add the xpath of element
        /// </summary>
        /// <param name="elementTofind"></param>
        public void FindElementBy(string elementTofind)
        {
            driver.FindElement(By.XPath(elementTofind));

        }

        public void Javascriptclick(IWebElement element)
        {
            IJavaScriptExecutor javaScriptExecutor = (IJavaScriptExecutor)driver;
            javaScriptExecutor.ExecuteScript("arguments[0].click();", element);
        }

        /// <summary>
        /// log off from the application 
        /// </summary>
        public void Logoff()
        {
            driver.FindElement(By.XPath("//*[@id='lnkLogOut']")).Click();
            Console.WriteLine("test completed");
            //  driver.Close();
        }

        public void PageLoad()
        {
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10); //pageLoadTimeout(10, TimeUnit.SECONDS);
        }
        /// <summary>
        /// TAKES SCREEN SHOT ONLY ON FAILED TESTCASES 
        /// </summary>
        public void TakesScreenshotOnFailedTest()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                var filename = TestContext.CurrentContext.Test.MethodName + "_screenshot_" + DateTime.Now.Ticks + ".jpg";

                //need to add relative path from the solution
                try
                {

                    var RootPath = System.IO.Path.Combine(
                       AppDomain.CurrentDomain.BaseDirectory,
                       "..\\..\\SeleniumSS\\");
                    if (!Directory.Exists(RootPath))
                    {
                        Directory.CreateDirectory(RootPath);
                    }
                    var path = RootPath + filename;
                    screenshot.SaveAsFile(path, ScreenshotImageFormat.Jpeg);
                    TestContext.AddTestAttachment(path);
                }
                catch (DirectoryNotFoundException dirEx)
                {
                    // Let the user know that the directory did not exist.
                    Console.WriteLine("Directory not found: " + dirEx.Message);
                }


            }
        }
        /// <summary>
        /// this is used to find whats inside the web element IE text,css 
        /// </summary>
        /// <param name="element">web element</param>
        /// <param name="findwhat">attribute like text, color,we and add more </param>
        /// <returns>text</returns>
        public string FindTheTextInsidetheElement(string element, string findwhat)
        {
            string text;
            IWebElement webElement = driver.FindElement(By.XPath(element));
            switch (findwhat)
            {
                case "text":
                    text = webElement.Text;
                    break;
                case "color":
                    text = webElement.GetCssValue("color");
                    break;
                default:
                    text = "Item not found please check";
                    break;
            }

            //String text = webElement.Text;
            return text;
        }
        /// <summary>
        /// checks footer in every screen
        /// </summary>
        public void FooterCheck()
        {
            string colors = FindTheTextInsidetheElement("//*[@id='layoutBody']/div[5]/footer", "color");

            // Assert.AreEqual(colors, "rgba(33, 37, 41, 1)");
            AssertExpectedandActual(colors, "rgba(33, 37, 41, 1)");

            Console.WriteLine("Footer displayed as Expected");
        }

        /// <summary>
        /// to find element for clicking 
        /// </summary>
        /// <param name="elementToClick">Xpath of the element</param>
        public void findElementandClick(string elementToClick)
        {
            driver.FindElement(By.XPath(elementToClick)).Click();
        }
        /// <summary>
        /// we are waiting for loader to disappear
        /// if loader didn't disappear we are failing test 
        /// </summary>
        public void PageisLoadingWait()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            try
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(By.Id("loading-image")));

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex + "load failed");
                Assert.Fail("page is loading longer");
            }
            // wait.Until(ExpectedConditions.visibilityOf(loader)); // wait for loader to appear
            // wait.Until(ExpectedConditions.invisibilityOf(loader)); // wait for loader to disappear
            Console.WriteLine("loader disappeared" + wait.Message);
            //C:\Users\yesravankumar\source\repos\CAS\CAS-CRS\Automation\Selftest\Selftest\Pages\Baseclass.cs
        }

        //public static string Base64Encode(string plainText)
        //{
        //    var plainTextBytes = System.Text.Encoding.Unicode.GetBytes(plainText);
        //    return System.Convert.ToBase64String(plainTextBytes);
        //}

        //public static string BaseDecode(string base64EncodedData)
        //{
        //    var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        //    return System.Text.Encoding.Unicode.GetString(base64EncodedBytes);
        //}

        /// <summary>
        /// this will decrypt the text 
        /// </summary>
        /// <param name="input">string to decrypt </param>
        /// <returns></returns>
        public string Decrypt(string input)
        {
            string key = "sblw-3hn8-sqoy19";
            byte[] inputArray = Convert.FromBase64String(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        /// <summary>
        /// Click on search button and wait for the page to load
        /// </summary>
        public void clickonSearchButton()
        {
            findElementandClick("//*[@id='btnSearch']");
            //  Waitforsomesec(60);
            PageisLoadingWait();
        }
        /// <summary>
        /// find the main tab from the Home screen 
        /// </summary>
        /// <param name="id"></param>
        public static void FindMainTabFromHomeScreen(string id)
        {
            driver.FindElement(By.XPath($"//*[@id='{id}']")).Click();
        }
        /// <summary>
        /// this is short signature of Assert Are equal  Statement
        /// </summary>
        /// <param name="Expected">Expected outPut</param>
        /// <param name="Actual">Actual Output </param>
        public static void AssertExpectedandActual(string Expected, string actual)
        {
            Assert.AreEqual(Expected, actual, $"assert failed because {Expected} is expected but {actual} is displayed");
        }
        /// <summary>
        /// Verify Error Message Page Is Displayed ??
        /// we are verifying  the page  similar to "https://qcas.deloitte.com/Error/Index?requestId=cfcccaa1-8d06-49e0-853d-8211fb335d50"
        /// </summary>
        public void VerifyErrorMessagePageIsDisplayed()
        {
            //VerifyErrorMessagePageIsDisplayed 
            IWebElement webElementerror;
            try
            {
                webElementerror = driver.FindElement(By.XPath("//*[@id='partialBody']/h2"));
            }
            catch (Exception)
            {

                webElementerror = null;
            }

            if (webElementerror != null
                && webElementerror.Text == "An error occurred while processing your request.")
            {
                string Errormsg = FindTheTextInsidetheElement("//*[@id='partialBody']/h4", "text");
                Assert.Fail(Errormsg);
            }


        }
        /// <summary>
        /// things yet to include 
        /// other waits 
        /// ddl 
        /// Data loading from Excel 
        /// </summary>
        public void GeneralCode()
        {
            

            SelectElement DDLSelect = new SelectElement(driver.FindElement(By.XPath("//*[@id='ddlUserName']")));

            DDLSelect.SelectByText("Sravan Kumar");


            //for finding elements  and tab management

            var mainwindow = driver.WindowHandles[0];
            var newWindowHandle = driver.WindowHandles[1];
            driver.SwitchTo().Window(driver.WindowHandles[1]);
            //using find all sub Elements
            List<IWebElement> AllsubTabs = driver.FindElements(By.XPath("//*[@id='uiItemlist']/li")).ToList();

            driver.FindElement(By.XPath("//*[@id='bySelection']/div[2]")).Click();
            driver.FindElement(
                By.XPath("//*[@id='userNameInput']")).SendKeys(
                    ConfigurationManager.AppSettings["QaAdminUserName"]);
            driver.FindElement(
                By.XPath("//*[@id='passwordInput']")).SendKeys(Decrypt(
                    ConfigurationManager.AppSettings["QaAdminPassword"]));

        }


        public void Waits()
        {
            ImplicitWait();
            WaitExplicit("enter string");
        }
        /// <summary>
        /// ExpectedConditions
        ///AlertIsPresent()
        ///ElementIsVisible()
        ///ElementExists()
        ///ElementToBeClickable(By)
        ///ElementToBeClickable(IWebElement)
        ///ElementToBeSelected(By)
        ///ElementToBeSelected(IWebElement)
        ///ElementToBeSelected(IWebElement, Boolean)
        ///TitleContains()
        ///UrlContains()
        ///UrlMatches()
        ///VisibilityOfAllElementsLocatedBy(By)
        ///VisibilityOfAllElementsLocatedBy(ReadOnlyCollection<IWebElement>)
        ///StalenessOf(IWebElement)
        ///TextToBePresentInElement()
        ///TextToBePresentInElementValue(IWebElement, String)
        /// </summary>
        /// <param name="Xpath"></param>
        private static void WaitExplicit(string Xpath)
        {
            String target_xpath = Xpath;

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            IWebElement SearchResult =
                wait.Until(
                    SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(target_xpath)));
        }

        private static void ImplicitWait()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }






        #region CodeEND
        #endregion
    }
}

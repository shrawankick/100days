using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Selftest
{
    public class Baseclass
    {
        readonly IWebDriver driver = new ChromeDriver();
        
    }
}
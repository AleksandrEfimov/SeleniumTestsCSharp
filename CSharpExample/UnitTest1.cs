using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    [TestFixture]
    public class MyFirstTest
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void Start()
        {
            driver = new ChromeDriver();
            
            //driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(20));
            
            
           wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void FirstTest()
        {
            driver.Url = "http://localhost:8080/litecart/admin/";
            IWebElement element = wait.Until(d => d.FindElement(By.Name("username")));
            

            driver.FindElement(By.Name("username")).SendKeys("admin");
            // в поисках ошибки применён xPath - не помог, но работает.
            driver.FindElement(By.XPath("//*[@id=\"box-login\"]/form/div[1]/table/tbody/tr[2]/td[2]/span/input")).SendKeys("admin");
            // данному логину необходим был Клик, в то время как Гугл требовал сабмит
            driver.FindElement(By.Name("login")).Click();
            // как критерий входа в админ-панель - виджет статистики интернет-магазина
            wait.Until(ExpectedConditions.ElementExists(By.Id("widget-stats")));
        }

        [TearDown]
        public void Stop()
        {
            //driver.FindElement(By.ClassName("fa fa-sign-out fa-lg")).Click();
            driver.Quit();
            driver = null;
        }
    }
}

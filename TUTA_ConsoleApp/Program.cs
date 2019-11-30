using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUTA_ConsoleApp
{
    public class Program
    {
        IWebDriver driver;

        static void Main(string[] args)
        {
        }

        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver("C:\\Users\\Agna\\Desktop\\");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
        }

        string username = "agnasa90@gmail.com";
        string password = "AgnaTest01";
        string password2 = "labas";
        string item = "dress";

        public void Login(string user, string pass) {

            driver.FindElement(By.XPath("//*[@id='email']")).SendKeys(user);
            driver.FindElement(By.XPath("//*[@id='passwd']")).SendKeys(pass);
        }

        public void Search(string item) {
         driver.FindElement(By.XPath("//*[@id='search_query_top']")).SendKeys(item);
        }

        /* //Darbas paskaitoje
         [Test]
           public void SuccessLogin()
          {
              driver.Url = "http://automationpractice.com/index.php";
              driver.FindElement(By.XPath("//*[@id='header']/div[2]/div/div/nav/div[1]/a")).Click();
              Login(username, password);
              driver.FindElement(By.XPath("//*[@id='SubmitLogin']/span")).Click();
              Assert.True(driver.FindElement(By.XPath("//*[@id='center_column']/h1")).Text.ToLower().Contains("my account"));
          }

            //Darbas paskaitoje
          [Test]
          public void UnsuccessLogin()
          {
              driver.Url = "http://automationpractice.com/index.php";
              driver.FindElement(By.XPath("//*[@id='header']/div[2]/div/div/nav/div[1]/a")).Click();
              Login(username, password2);
              driver.FindElement(By.XPath("//*[@id='SubmitLogin']/span")).Click();
              Assert.True(driver.FindElement(By.XPath("//*[@id='center_column']/div[1]/ol/li")).Text.ToLower().Contains("authentication failed"));

          } */

        // Namų darbas
        [Test]
        public void SuccessSignIn()
        {
            driver.Url = "http://automationpractice.com/index.php";
            driver.FindElement(By.XPath("//*[@id='header']/div[2]/div/div/nav/div[1]/a")).Click();
            Login(username, password);
            driver.FindElement(By.XPath("//*[@id='SubmitLogin']/span")).Click();
        }

        [Test]
        public void ValidateCorrectSignIn()
        {
            SuccessSignIn();
            Assert.True(driver.FindElement(By.XPath("//*[@id='header']/div[2]/div/div/nav/div[1]/a/span")).Text.ToLower().Contains("agna test"));
        }

        [Test]
        public void SearchItemInShop()
        {
            SuccessSignIn();
            Search(item);
            driver.FindElement(By.XPath("//*[@id='searchbox']/button")).Click();
        }


        [Test]
        public void ValidateFoundItem()
        {
            SearchItemInShop();
            driver.FindElement(By.XPath("//*[@id='center_column']/ul/li[3]/div/div[1]/div/a[1]/img")).Click(); 
            Assert.True(driver.FindElement(By.XPath("//*[@id='center_column']/div/div/div[3]/h1")).Text.ToLower().Contains("printed summer dress"));
        }

        [Test]
        public void FinishBuyingItem()
        {
            SearchItemInShop();
            driver.FindElement(By.XPath("//*[@id='center_column']/ul/li[3]/div/div[1]/div/a[1]/img")).Click();
            driver.FindElement(By.XPath("//*[@id='group_1']/option[2]")).Click(); 
            driver.FindElement(By.XPath("//*[@id='color_8']")).Click(); 
            driver.FindElement(By.XPath("//*[@id='add_to_cart']/button/span")).Click();
            driver.FindElement(By.XPath("//*[@id='layer_cart']/div[1]/div[2]/div[4]/a")).Click();
        }

        [Test]
        public void ValidateIfOrderIsComplete()
        {
            FinishBuyingItem();
            driver.FindElement(By.XPath("//*[@id='center_column']/p[2]/a[1]")).Click();
            driver.FindElement(By.XPath("//*[@id='center_column']/form/p/button")).Click(); 
            driver.FindElement(By.XPath("//*[@id='cgv']")).Click();
            driver.FindElement(By.XPath("//*[@id='form']/p/button")).Click();
            driver.FindElement(By.XPath("//*[@id='HOOK_PAYMENT']/div[1]/div/p/a")).Click();
            driver.FindElement(By.XPath("//*[@id='cart_navigation']/button")).Click();
            Assert.True(driver.FindElement(By.XPath("//*[@id='center_column']/div/p/strong")).Text.ToLower().Contains("is complete"));
        }

        [TearDown]
        public void CloseBrowser()
        {

            driver.Close();
        }
    }
}

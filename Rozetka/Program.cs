using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace Rozetka
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "https://rozetka.com.ua";
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            IWebElement languageUA = driver.FindElement(By.XPath("//*[@href='/ua/']"));

            while (languageUA.Displayed == true)
            {
                languageUA.Click();
                break;
            }

            driver.FindElement(By.XPath("//button[@aria-label='Каталог товарів']")).Click();
            IWebElement category = driver.FindElement(By.LinkText("Інструменти та автотовари"));
            Actions action = new Actions(driver);
            action.MoveToElement(category);
            action.Perform();

            driver.FindElement(By.LinkText("Перфоратори")).Click();

            //IWebElement banner = driver.FindElement(By.Id("rz-banner-img"));
            //IWebElement closeBannerButton = driver.FindElement(By.ClassName("exponea-close-cross"));
            //if (banner.Displayed)
            //{
            //    closeBannerButton.Click();
            //}

            IWebElement minPrice = driver.FindElement(By.XPath("//*[@data-filter-name='price']//*[@formcontrolname='min']"));
            minPrice.Clear();
            minPrice.SendKeys("1000");

            IWebElement maxPrice = driver.FindElement(By.XPath("//*[@data-filter-name='price']//*[@formcontrolname='max']"));
            maxPrice.Clear();
            maxPrice.SendKeys("5000");

            driver.FindElement(By.XPath("//button[@class='button button_color_gray button_size_small slider-filter__button']")).Click();

            IList<IWebElement> prices = driver.FindElements(By.XPath("//*[@class='goods-tile__inner']//*[@class='goods-tile__price-value']"));

            Console.WriteLine(prices.Count);

            for (int i = 0; i < prices.Count; i++)
            {
                var goodsPrice = prices[i].Text;
                var clean = goodsPrice.Replace(" ", "");
                var cleanInt = int.Parse(clean);
                if (cleanInt < 1000 && cleanInt > 5000)
                {
                    Console.WriteLine($"{goodsPrice} do not meets filter parameter");
                    break;
                }
            };
            Console.WriteLine("All goods meet filter parameter");
            Console.ReadKey();
        }
    }
}

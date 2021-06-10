using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace WernerDemo.Pages
{
    public class ShowTimesPage : BasePage
    {
        public  ShowTimesPage(IWebDriver driver) 
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//a[starts-with(@id, 'showtimes') and @aria-disabled='false']")]
        private IList<IWebElement> showTimes;
        [FindsBy(How = How.XPath, Using = "//a[text() = 'Accept']")]
        private IWebElement accept;
        [FindsBy(How = How.XPath, Using = "//button[text() = 'Continue' and @data-ctainfo='confirm/accept modal']")]
        private IWebElement continueBtn;

        public void clickARandomShowTime()
        {
            Console.WriteLine($"showTimes.Count {showTimes.Count}");
            VerifyElementsAreDisplayed(showTimes);
            var random = new Random();
            var availableShowTimes = random.Next(0, showTimes.Count - 1);
            showTimes[availableShowTimes].Click();
        }

        public void clickAccept()
        {
            try
            {
                accept.Click();
            }
            catch (Exception)
            {

                
            }
        }

        public SeatNumberPage clickContinue()
        {
            continueBtn.Click();
            return new SeatNumberPage(_driver);
        }

    }
}

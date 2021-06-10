using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace WernerDemo.Pages
{
    public class SeatNumberPage : BasePage
    {
        public SeatNumberPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }
        
        [FindsBy(How = How.XPath, Using = "//li[@class='layout__row']/ul/li/div[starts-with(@aria-label,'Row') and contains(@aria-label,'Love seat')]")]
        private IList<IWebElement> seatNumbers;
        [FindsBy(How = How.XPath, Using = "//li[@class='layout__row']/ul/li/div[starts-with(@aria-label,'Row') and contains(@aria-label,'Can reserve')]")]
        private IList<IWebElement> seatNumberMorning;

        [FindsBy(How = How.CssSelector, Using = "button#checkout-continue")]
        private IWebElement continueBtn;

        public void clickARandomSeat()
        {
            try
            {
                Console.WriteLine($"seatNumbers.Count {seatNumberMorning.Count}");
                VerifyElementsAreDisplayed(seatNumberMorning);
                var random = new Random();
                var availableSeatNumbers = random.Next(0, seatNumberMorning.Count - 1);
                seatNumberMorning[availableSeatNumbers].Click();
            }
            catch (Exception)
            {
                Console.WriteLine($"seatNumbers.Count {seatNumbers.Count}");
                VerifyElementsAreDisplayed(seatNumbers);
                var random = new Random();
                var availableSeatNumbers = random.Next(0, seatNumbers.Count - 1);
                seatNumbers[availableSeatNumbers].Click();
            }
        }

        public TicketTypePage clickContinue()
        {
            continueBtn.Click();
            return new TicketTypePage(_driver);
        }
    }
}

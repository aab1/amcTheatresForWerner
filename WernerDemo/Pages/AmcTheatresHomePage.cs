using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace WernerDemo.Pages
{
    public class AmcTheatresHomePage : BasePage
    {
        public AmcTheatresHomePage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }
        [FindsBy(How = How.XPath, Using = "//button[text()='No Thanks']")]
        private IWebElement noThanksButton;
        [FindsBy(How = How.CssSelector, Using = "[data-tour='headerShowtimes']")]
        private IWebElement showTimes;
        public void clickNoThanksOnWelComeModal()
        {
            WaitForElementToBeClickable(noThanksButton);
            noThanksButton.Click();
        }

        public ShowTimesPage clickShowTimes()
        {
            WaitForElementToBeClickable(showTimes);
            showTimes.Click();
            return new ShowTimesPage(_driver);
        }

       
    }
}

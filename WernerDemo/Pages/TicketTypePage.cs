using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace WernerDemo.Pages
{
    public class TicketTypePage : BasePage
    {
        public TicketTypePage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }
        [FindsBy(How = How.CssSelector, Using = "[id='add-adult'] svg")]
        private IWebElement addAdult;
        [FindsBy(How = How.XPath, Using = "//h2[text()='1 Adult']")]
        private IWebElement adultTicket;
        [FindsBy(How = How.CssSelector, Using = "[id='checkout-continue']")]
        private IWebElement continueBtn;
        public void clickToAddAdult()
        {
            WaitForElementToBeClickable(addAdult);
            addAdult.Click();
        }

        public void verifyAdultIsUpdated (string expectedText)
        {
            Console.WriteLine($"{adultTicket.Text} is the ouput for adult text");
            Assert.AreEqual(expectedText, adultTicket.Text);
        }

        public void verifyContinueButtonIsClickAble()
        {
            Assert.IsTrue(continueBtn.Enabled);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using WernerDemo.Drivers;



namespace WernerDemo.Pages
{
    public class BasePage : Commons
    {
        public string BASE_URL = "https://www.amctheatres.com/";
        public SelectElement select;

        public void LaunchUrl()
        {
            _driver.Navigate().GoToUrl(BASE_URL);
        }

        public void SelectByText(IWebElement element, string text)
        {
            select = new SelectElement(element);
            select.SelectByText(text);
        }

        public void SelectByValue(IWebElement element, string value)
        {
            select = new SelectElement(element);
            select.SelectByValue(value);
        }

        public void SelectByIndex(IWebElement element, int index)
        {
            select = new SelectElement(element);
            select.SelectByIndex(index);
        }

        //================================================================




        public static void SwitchToWindow(int index)
        {
            ReadOnlyCollection<string> windows = _driver.WindowHandles;

            if (windows.Count < index)
            {
                throw new NoSuchWindowException("Invalid Browser Window index: " + index);
            }

            _driver.SwitchTo().Window(windows[index]);
            Thread.Sleep(1000);
        }

        public static void SwitchToParentWindow()
        {
            var window_ids = _driver.WindowHandles;
            //count starts from index 1(i.e the first browser window tab) NOT like arrays but the swtich starts from 0
            for (int i = window_ids.Count; i > 0; i--)
            {
                if (i >= 2)
                {
                    _driver.Close();
                    _driver.SwitchTo().Window(window_ids[i - 2]);
                }

            }
            _driver.SwitchTo().Window(window_ids[0]);
        }
        /*#################################################################################################
        Uses - This method helps to type given value into a field
        It takes in any WebElement of interest and the value to type in
        ###################################################################################################
        */
        public static void TypeGivenValueInto(IWebElement element, String text)
        {
            element.Clear();
            element.SendKeys(text);
        }
        /*
         * solve element not clickable
         */
        public void ClickUsingActionClass(string cssSelector)
        {
            OpenQA.Selenium.Interactions.Actions action = new Actions(_driver);
            action.MoveToElement(GetElementByCssSelector(cssSelector)).Click().Build().Perform();
        }
        /*
         *@param list of elements to be clicked from
         */
        public void clickARandomItemInAList(IList<IWebElement> elements)
        {
            Console.WriteLine($"elements count is {elements.Count}");
            VerifyElementsAreDisplayed(elements);
            var random = new Random();
            var availableElements = random.Next(0, elements.Count - 1);
            elements[availableElements].Click();
        }

        public void WaitUntilElementIsNoLongerDisplayed(string cssSelector)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(40));
            wait.Until(driver => !GetElementByCssSelector(cssSelector).Displayed);
        }
        /*########################################################################################################
         * Wait until url contains a specified word
         */
        public void WaitUntilUrlContainsASpecifiedWord(string word)
        {
            WebDriverWait w = new WebDriverWait(_driver, TimeSpan.FromSeconds(60));
            w.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(InvalidElementStateException));
            w.Until(ExpectedConditions.UrlContains(word));
        }
        /*########################################################################################################
         * This methods waits 10 second for an element to be clickable
         * @param input any IWebElement
         */
        public void WaitForElementToBeClickable(IWebElement element)
        {
            WebDriverWait w = new WebDriverWait(_driver, TimeSpan.FromSeconds(40));
            w.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(InvalidElementStateException));
            w.Until(ExpectedConditions.ElementToBeClickable(element));
        }

        /*
         * @param this method takes string of css selector
         * it waits until element causing another element not to be clickable or accessible disappears
         */

        public void WaitForElementToDisAppear(string element_cssSelecttor)
        {
            WebDriverWait w = new WebDriverWait(_driver, TimeSpan.FromSeconds(60));
            w.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(InvalidElementStateException), typeof(InvalidOperationException));
            w.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(element_cssSelecttor)));
        }
        /*
         * ########################################################################################################
         *  @param input the string value of the css selector from webElement
         */
        public void WaitForElementToBeDisplayed(string element_cssSelecttor)
        {
            WebDriverWait w = new WebDriverWait(_driver, TimeSpan.FromSeconds(40));
            w.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(InvalidElementStateException));
            w.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(element_cssSelecttor)));
        }
        public void WaitForElementIsNoLongerAttachedToTheDOM(IWebElement element)
        {
            WebDriverWait w = new WebDriverWait(_driver, TimeSpan.FromSeconds(40));
            w.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(InvalidElementStateException));
            w.Until(ExpectedConditions.StalenessOf(element));
        }

        public void WaitForElementExistence(string element_cssSelecttor)
        {
            WebDriverWait w = new WebDriverWait(_driver, TimeSpan.FromSeconds(40));
            w.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(InvalidElementStateException));
            w.Until(ExpectedConditions.ElementExists(By.CssSelector(element_cssSelecttor)));
        }
        /*
         * ########################################################################################################
         * scroll to an element that is located by IWebElement
         */
        public static void ScrollToElement(IWebElement element)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)_driver;
            //IWebElement elem = GetElementByCssSelector(element);
            executor.ExecuteScript("window.scrollTo(0," + element.Location.Y + ")");
        }

        /*########################################################################################################
         * This method helps to scroll to the bottom of a page*/
        public static void ScrollToTheButtomOfAPage()
        {
            ((IJavaScriptExecutor)_driver)
                     .ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
        }
        /*
           Uses - This method helps to assert/validate that an element is displayed
           It takes in any IWebElement of interest
        */
        public static void VerifyAnElementIsDisplayed(IWebElement element)
        {
            Assert.IsTrue(element.Displayed, element + " is not displayed");
        }

        public static void VerifyPageTitle(string expectedPageTitle, string actualTitle, string expectedErrorMessage)
        {
            //string title = driver.Title;
            Assert.AreEqual(expectedPageTitle, actualTitle, expectedErrorMessage);
        }

        public static void VerifyElementsAreDisplayed(IList<IWebElement> elements)
        {
            Assert.IsTrue(elements.Count > 0 , elements + " are not displayed");
        }


        //####################### Find WebElelement ################################################################
        public static IWebElement GetElementById(string id)
        {
            By locator = By.Id(id);
            return GetElement(locator);
        }
        public static IWebElement GetElementByClassName(string className)
        {
            By locator = By.ClassName(className);
            return GetElement(locator);
        }
        public static IWebElement GetElementByCssSelector(string cssSelector)
        {
            By locator = By.CssSelector(cssSelector);
            return GetElement(locator);
        }
        public static IWebElement GetElementByName(string name)
        {
            By locator = By.Name(name);
            return GetElement(locator);
        }
        public static IWebElement GetElementByXpath(string xpath)
        {
            By locator = By.XPath(xpath);
            return GetElement(locator);
        }
        public static IWebElement GetElementByTagName(string tagname)
        {
            By locator = By.TagName(tagname);
            return GetElement(locator);
        }
        public static IWebElement GetElementByLinkText(string linkText)
        {
            By locator = By.LinkText(linkText);
            return GetElement(locator);
        }
        public static IList<IWebElement> GetElementsById(string id)
        {
            By locator = By.Id(id);
            return GetElements(locator);
        }
        public static IList<IWebElement> GetElementsByClassName(string className)
        {
            By locator = By.ClassName(className);
            return GetElements(locator);
        }
        public static IList<IWebElement> GetElementsByCssSelector(string cssSelector)
        {
            By locator = By.CssSelector(cssSelector);
            return GetElements(locator);
        }
        public static IList<IWebElement> GetElementsByName(string name)
        {
            By locator = By.Name(name);
            return GetElements(locator);
        }
        public static IList<IWebElement> GetElementsByXpath(string xpath)
        {
            By locator = By.XPath(xpath);
            return GetElements(locator);
        }
        public static IList<IWebElement> GetElementsByTagName(string tagname)
        {
            By locator = By.TagName(tagname);
            return GetElements(locator);
        }
        private static IWebElement GetElement(By locator)
        {
            IWebElement element = null;
            int tryCount = 0;
            while (element == null)
            {
                try
                {
                    element = _driver.FindElement(locator);
                    return new WebDriverWait(_driver, TimeSpan.FromSeconds(2))
                       .Until(ExpectedConditions.ElementExists(locator));
                }
                catch (Exception e)
                {
                    if (tryCount == 3)
                    {
                        SaveScreenshot();
                        throw e;
                    }
                }
                //var waitTime = new TimeSpan(0, 0, 0);
                //Thread.Sleep(waitTime);
                tryCount++;



            }
            Console.WriteLine("{0} is now retrieved", element.ToString());
            return element;
        }

        private static IList<IWebElement> GetElements(By locator)
        {
            IList<IWebElement> element = null;
            int tryCount = 0;
            while (element == null)
            {
                try
                {
                    element = _driver.FindElements(locator);
                    return new WebDriverWait(_driver, TimeSpan.FromSeconds(2))
                       .Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
                }
                catch (Exception e)
                {
                    if (tryCount == 3)
                    {
                        SaveScreenshot();
                        throw e;
                    }
                }
                //var waitTime = new TimeSpan(0, 0, 2);
                //Thread.Sleep(waitTime);

                tryCount++;
            }
            Console.WriteLine("{0} is now retrieved", element.ToString());
            return element;
        }
        //####################################################################################################################
        private static Screenshot TakeScreenshot()
        {
            return ((ITakesScreenshot)_driver).GetScreenshot();
        }
        public static string ScreenShotLocation()
        {
            var dateNow = DateTime.Now.Date.ToString().Replace(@"/", "").Replace(@":", "");
            dateNow = dateNow.Substring(0, 8);

            var timeNow = DateTime.Now.TimeOfDay.ToString().Replace(@"/", "").Replace(@" ", "").Replace(@":", "").Replace(@".", "");
            timeNow = timeNow.Substring(0, 6);

            //Change the location(i.e C:\\Screenshots) to any drive as required provided others need to see the screenshot e.g f drive
            return String.Format("C:\\Screenshots\\{0}_{1}.png", dateNow, timeNow);
        }


        public static void SaveScreenshot()
        {
            try
            {
                var location = ScreenShotLocation();
                var screenshot = TakeScreenshot();
                screenshot.SaveAsFile(location, ScreenshotImageFormat.Png);
                //TestController.ExtentLogScreenshotLocation(location);
            }
            catch (Exception e)
            {
                Console.Write(String.Format("Screenshot cannot be saved because {0}", e));
            }
        }


    }
}


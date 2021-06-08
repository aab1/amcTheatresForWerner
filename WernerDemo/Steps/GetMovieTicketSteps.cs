using TechTalk.SpecFlow;
using WernerDemo.Pages;

namespace WernerDemo.Steps
{
    [Binding]
    public sealed class GetMovieTicketSteps : BasePage
    {
        AmcTheatresHomePage homePage = new AmcTheatresHomePage(_driver);
        ShowTimesPage showTimePage = new ShowTimesPage(_driver);
        SeatNumberPage seatNumberPage = new SeatNumberPage(_driver);

        [Given(@"I Navigate to amctheatres hompage")]
        public void GivenINavigateToAmctheatresHompage()
        {
            homePage.LaunchUrl();
            homePage.clickNoThanksOnWelComeModal();
        }

        [When(@"I select a random movie time")]
        public void WhenISelectARandomMovieTime()
        {
            showTimePage = homePage.clickShowTimes();
            showTimePage.clickARandomShowTime();
            showTimePage.clickAccept();
            seatNumberPage = showTimePage.clickContinue();
        }

        [When(@"I select a random seat")]
        public void WhenISelectARandomSeat()
        {
            seatNumberPage.clickARandomSeat();
        }

        [When(@"I add adult")]
        public void WhenIAddAdult()
        {
            
        }

        [Then(@"The Adult updates to appear as ""(.*)""")]
        public void ThenTheAdultUpdatesToAppearAs(string p0)
        {
            
        }

        [Then(@"Continue Button is Enabled")]
        public void ThenContinueButtonIsEnabled()
        {
            
        }





    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using YahtzeeGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahtzeeGame.Tests
{
    [TestClass()]
    public class ScoreCardTests
    {
        [TestMethod()]
        public void ScoreCardTest()
        {
            Dictionary<int,int>scores= new Dictionary<int, int>() { {1,1 },{2,0 },{ 3, 1 },{ 4, 2 },{ 5, 0 },{ 6, 1 } };
            ScoreCard scoreCard = new ScoreCard(scores);
            int ExpectedSumDices = 18;
            int ActualSumDices = scoreCard.getSumDice(scores);
            Assert.AreEqual(ExpectedSumDices, ActualSumDices);

            bool expectedOutcomeFulLHouse = false;
            bool actualOutcomeFullHouse=scoreCard.isFullHouse(scores);

            Assert.AreEqual(actualOutcomeFullHouse, expectedOutcomeFulLHouse);

            bool expectedOutcomeSmallStraight = false;
            bool actualOutcomeSmallStraight=scoreCard.isSmallStraight(scores);

            Assert.AreEqual(expectedOutcomeSmallStraight, actualOutcomeSmallStraight);


            bool expectedOutcomeLargeStraight = false;
            bool actualOutcomeLargeStraight=scoreCard.isLargeStraight(scores);

            Assert.AreEqual(actualOutcomeLargeStraight, expectedOutcomeLargeStraight);

            bool expectedOutcomeThreeKind = false;
            bool actualOutcomeThreeKind=scoreCard.isThreeKind(scores);

            Assert.AreEqual(actualOutcomeThreeKind, expectedOutcomeThreeKind);

            bool expectedOutcomeFourKind = false;
            bool actualOutcomeFourKind=scoreCard.isFourKind(scores);

            Assert.AreEqual(actualOutcomeFourKind, expectedOutcomeFourKind);


            bool expectedOutcomeIsYahtzee = false;
            bool actualOutcomeIsYahtzee=scoreCard.isYahtzee(scores);

            Assert.AreEqual(actualOutcomeIsYahtzee, expectedOutcomeIsYahtzee);


            
        }
    }
}
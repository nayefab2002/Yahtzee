using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace YahtzeeGame
{
    public class Player
    {
        
        public int numRollLeft { get; set; }
        public int OnesScore { get; set; }
        public int TwosScore { get; set; }
        public int ThreesScore { get; set; }
        public int FoursScore { get; set; }
        public int FivesScore { get; set; }
        public int SixesScore { get; set; }

        public int ThreeOfAKindScore { get; set; }
        public int FourOfAKindScore { get; set; }
        public int SmallStrightScore { get; set; }
        public int LargeStraightScore { get; set; }
        public int FullHouseScore { get; set; }
        public int YahtzeeScore { get; set; }
        public int ChanceScore { get; set; }

        public bool isPlaying { get; set; }


        public Player()
        {
            OnesScore = 0; TwosScore=0; ThreesScore=0; FoursScore=0; SixesScore=0;FivesScore=0;
            ThreeOfAKindScore=0;FourOfAKindScore=0;SmallStrightScore=0;
            LargeStraightScore=0;FullHouseScore=0;YahtzeeScore=0;ChanceScore=0;
            numRollLeft = 3;
        }

        public int CalculateUpperTotal() { return OnesScore + TwosScore + ThreesScore + FoursScore + FivesScore + SixesScore; }
        public int UpperBonus()
        {
            if (CalculateUpperTotal() >= 63)
            {
                return 35;
            }
            else { return 0; }
        }

        public int CalculateLowerTotal() => ThreeOfAKindScore + FourOfAKindScore + SmallStrightScore + LargeStraightScore+ FullHouseScore + YahtzeeScore+ ChanceScore;

        public int CalculateTotal() => CalculateUpperTotal() + UpperBonus() + CalculateLowerTotal();



    }
}

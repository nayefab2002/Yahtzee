using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahtzeeGame
{
    public class ScoreCard
    {
       
        public ScoreCard() {
            
        }
        public int getSumDice(Dictionary<int, int> map)
        {
            int sum = 0;
            foreach (var item in map)
            {
                sum += (item.Key * item.Value);
            }
            return sum;
        }
        public bool isFullHouse(Dictionary<int, int> map)
        {
            for (int i = 0; i < map.Count; i++)
            {
                KeyValuePair<int, int> item = map.ElementAt(i);
                if (item.Value == 2 || item.Value == 3)
                {
                    if (item.Value == 2)
                    {
                        for (int j = i + 1; j < map.Count; j++)
                        {
                            var nextItem = map.ElementAt(j);
                            if (nextItem.Value == 3) { return true; }
                        }
                    }
                    else if (item.Value == 3)
                    {
                        for (int j = i + 1; j < map.Count; j++)
                        {
                            var nextItem = map.ElementAt(j);
                            if (nextItem.Value == 2) { return true; }
                        }
                    }
                }
            }
            return false;
        }

        public bool isSmallStraight(Dictionary<int, int> map)
        {
            int soum4count = 0;
            for (int i = 0; i < map.Count; i++)
            {

                var item = map.ElementAt(i);
                if (item.Value >= 1)
                {
                    for (int j = i + 1; j < map.Count; j++)
                    {
                        var newItem = map.ElementAt(j);
                        if (newItem.Value == 0) { break; }
                        else if (newItem.Value >= 1)
                        {
                            if (soum4count++ == 4)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public bool isLargeStraight(Dictionary<int, int> map)
        {
            int sum5Count = 0;
            for (int i = 0; i < map.Count; i++)
            {

                var item = map.ElementAt(i);
                if (item.Value == 1)
                {
                    for (int j = i + 1; j < map.Count; j++)
                    {
                        var newItem = map.ElementAt(j);

                        if (newItem.Value == 1)
                        {
                            sum5Count++;
                            if (sum5Count == 5)
                            {
                                return true;
                            }
                        }
                        if (newItem.Value == 0) { return false; }
                    }
                }


            }
            return false;

        }
        public bool isThreeKind(Dictionary<int, int> Values)
        {
            foreach (var item in Values)
            {
                if (item.Value == 3) { return true; }
            }
            return false;
        }
        public bool isFourKind(Dictionary<int, int> Values)
        {
            foreach (var item in Values)
            {
                if (item.Value == 4) { return true; }
            }
            return false;
        }
        public bool isYahtzee(Dictionary<int, int> Values)
        {
            foreach (var item in Values)
            {
                if (item.Value == 5) { return true; }
            }
            return false;
        }

        

    }
}

using System.Data;
using System.Numerics;

namespace YahtzeeGame
{
    public partial class Form1 : Form
    {
        List<int> upperSectionSumPlayer1 = new List<int>();
        List<int> LowerSectionSumPlayer1 = new List<int>();

        List<int> upperSectionSumPlayer2 = new List<int>();
        List<int> LowerSectionSumPlayer2 = new List<int>();

        List<Button> Player1Buttons = new List<Button>();
        List<Button> Player2Buttons = new List<Button>();

        bool isDice1onHold = false;
        bool isDice2onHold = false;
        bool isDice3onHold = false;
        bool isDice4onHold = false;
        bool isDice5onHold = false;

        int countDicePlayer1 = 0;
        int countDicePlayer2 = 0;

        bool isPlayer1Playing = true;
        bool isPlayer2Playing = false;

        bool isDiceRolled;

        Player PlayerOne;
        Player PlayerTwo;
        Player CurrentPlayer;

        ScoreCard PlayerScoreCard;

        public Form1()
        {
            InitializeComponent();
            PlayerOne = new Player();
            PlayerTwo = new Player();
            CurrentPlayer = PlayerOne;
            PlayerOne.isPlaying = true;
            PlayerTwo.isPlaying = false;
            PlayerScoreCard = new ScoreCard();
            isDiceRolled = false;
        }

        private void dice1_Click(object sender, EventArgs e)
        {
            dice1.Location = new Point(236, 300);
            isDice1onHold = true;
        }

        private void dice2_Click(object sender, EventArgs e)
        {
            dice2.Location = new Point(387, 300);
            isDice2onHold = true;
        }
        private void dice2DoubleClick(object sender, EventArgs e)
        {
            dice2.Location = new Point(387, 348);
            isDice2onHold = false;
        }
        private void dice3_click(object sender, EventArgs e)
        {
            dice3.Location = new Point(520, 300);
            isDice3onHold = true;
        }
        private void dice3DoubleClick(object sender, EventArgs e)
        {
            dice3.Location = new Point(520, 348);
            isDice3onHold = false;
        }
        private void dice4_Click(object sender, EventArgs e)
        {
            dice4.Location = new Point(639, 300);
            isDice4onHold = true;
        }
        private void dice4DoubleClick(object sender, EventArgs e)
        {
            dice4.Location = new Point(639, 348);
            isDice4onHold = false;
        }


        //Helper functions
        private int getSumDice(Dictionary<int, int> map)
        {
            int sum = 0;
            foreach (var item in map)
            {
                sum += (item.Key * item.Value);
            }
            return sum;
        }
        private bool isFullHouse(Dictionary<int, int> map)
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
        private bool isSmallStraight(Dictionary<int, int> map)
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
        private bool isLargeStraight(Dictionary<int, int> map)
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

        private void showUpperSectionScore(Dictionary<int, int> Values, Button dice1, Button dice2, Button dice3, Button dice4, Button dice5, Button dice6)
        {
            foreach (var item in Values)
            {
                if (item.Key == 3 && dice3.Enabled == true) { dice3.Text = $"{item.Key * item.Value}"; }
                if (item.Key == 2 && dice2.Enabled == true) { dice2.Text = $"{item.Key * item.Value}"; }
                if (item.Key == 1 && dice1.Enabled == true) { dice1.Text = $"{item.Key * item.Value}"; }
                if (item.Key == 4 && dice4.Enabled == true) { dice4.Text = $"{item.Key * item.Value}"; }
                if (item.Key == 5 && dice5.Enabled == true) { dice5.Text = $"{item.Key * item.Value}"; }
                if (item.Key == 6 && dice6.Enabled == true) { dice6.Text = $"{item.Key * item.Value}"; }
            }

        }
        private void calcUpperScoreForPlayer()
        {
            if (CurrentPlayer == PlayerOne)
            {
                if (text1player1.Enabled == false && text2player1.Enabled == false && text3player1.Enabled == false && text4player1.Enabled == false && text5player1.Enabled == false && text6player1.Enabled == false)
                {
                    sumplayer1.Text = PlayerOne.CalculateUpperTotal().ToString();
                    bonusplayer1.Text = PlayerOne.UpperBonus().ToString();
                }
            }
            else if (CurrentPlayer == PlayerTwo)
            {
                if (text1player2.Enabled == false && text2player2.Enabled == false && text3player2.Enabled == false && text4player2.Enabled == false && text5player2.Enabled == false && text6player2.Enabled == false)
                {
                    sumplayer2.Text = PlayerTwo.CalculateUpperTotal().ToString();
                    bonusplayer2.Text = PlayerTwo.UpperBonus().ToString();
                }
            }
        }
        private bool isThreeKind(Dictionary<int, int> Values)
        {
            foreach (var item in Values)
            {
                if (item.Value == 3) { return true; }
            }
            return false;
        }
        private bool isFourKind(Dictionary<int, int> Values)
        {
            foreach (var item in Values)
            {
                if (item.Value == 4) { return true; }
            }
            return false;
        }
        private bool isYahtzee(Dictionary<int, int> Values)
        {
            foreach (var item in Values)
            {
                if (item.Value == 5) { return true; }
            }
            return false;
        }
        private void showLowerSectionScore(Dictionary<int, int> Values, Button full, Button ss, Button ls, Button tok, Button fok, Button yah, Button chance)
        {
            if (isFullHouse(Values) && full.Enabled == true) { full.Text = "25"; }
            if (!isFullHouse(Values) && full.Enabled == true) { full.Text = "0"; }


            if (isSmallStraight(Values) && ss.Enabled == true) { ss.Text = "30"; }
            if (!isSmallStraight(Values) && ss.Enabled == true) { ss.Text = "0"; }

            if (isLargeStraight(Values) && ls.Enabled == true) { ls.Text = "40"; }
            if (!isLargeStraight(Values) && ls.Enabled == true) { ls.Text = "0"; }


            if (isThreeKind(Values) && tok.Enabled == true) { tok.Text = $"{getSumDice(Values)}"; }
            if (!isThreeKind(Values) && tok.Enabled == true) { tok.Text = "0"; }

            if (isFourKind(Values) && fok.Enabled == true) { fok.Text = $"{getSumDice(Values)}"; }
            if (!isFourKind(Values) && fok.Enabled == true) { fok.Text = "0"; }

            if (isYahtzee(Values) && yahplayer1.Enabled == true)
            { yah.Text = "50"; }
            else { yah.Text = "0"; }


            if (chance.Enabled == true) { chance.Text = $"{getSumDice(Values)}"; }

        }

        private void StorePlayerButtons()
        {
            Button[] listofButton = { fokplayer1, tokplayer1, text1player1, text2player1, text3player1, text4player1, text5player1, text6player1, fullplayer1, yahplayer1, chanceplayer1, ssplayer1, lsplayer1 };

            Player1Buttons.AddRange(listofButton);
            Button[] listofButton2 = { fokplayer2, tokplayer2, text2player2, text1player2, text3player2, text4player2, text5player2, text6player2, ssplayer2, lsplayer2, fullplayer2, yahplayer2, chanceplayer2 };
            Player2Buttons.AddRange(listofButton2);

        }


        private void ShowtotalScoreForPlayer()
        {
            StorePlayerButtons();
            foreach (var button in Player1Buttons) { if (button.Enabled == true) { return; } }
            foreach (var button in Player2Buttons) { if (button.Enabled == true) { return; } }
            totalplayer1.Text = PlayerOne.CalculateTotal().ToString();
            totalplayer2.Text = PlayerTwo.CalculateTotal().ToString();
            showResultToUser();
            
        }

        public void resetDiceLocation()
        {
            dice1.Location = new Point(236, 348);
            isDice1onHold = false;
            dice2.Location = new Point(387, 348);
            isDice2onHold = false;
            dice3.Location = new Point(520, 348);
            isDice3onHold = false;
            dice4.Location = new Point(639, 348);
            isDice4onHold = false;
            dice5.Location = new Point(756, 348);
            isDice5onHold = false;

        }
      
        private void refreshScreen(object sender, EventArgs e)
        {
            StorePlayerButtons();
            foreach (var button in Player1Buttons.ToList()) { button.Enabled = true; button.Text = string.Empty; }
            foreach (var button in Player2Buttons.ToList()) { button.Enabled = true; button.Text = string.Empty; }
            sumplayer1.Text = string.Empty; bonusplayer1.Text = string.Empty;
            sumplayer2.Text = string.Empty; bonusplayer2.Text = string.Empty;
            rolldiceButton1.Enabled = true;
            roll2.Enabled = true;
            totalplayer1.Text = string.Empty;
            totalplayer2.Text = string.Empty;
            PlayerOne = new Player();
            PlayerTwo = new Player();
        }

        public void clearEnabledbuttons()
        {
            StorePlayerButtons();

            foreach (var button in Player1Buttons)
            {
                if (button.Enabled == true) { button.Text = string.Empty; }
            }

            foreach (var button in Player2Buttons)
            {
                if (button.Enabled == true) { button.Text = string.Empty; }
            }

        }

        private void showResultToUser()
        {
            //ShowtotalScoreForPlayer();
            if (totalplayer1.Text == string.Empty || totalplayer2.Text == string.Empty) { return; }
           
            if (int.Parse(totalplayer1.Text) > int.Parse(totalplayer2.Text))
            {
                MessageBox.Show("Player 1 Wins!");
            }
            else if (int.Parse(totalplayer1.Text) < int.Parse(totalplayer2.Text))
            {
                MessageBox.Show("Player 2 wins!");
            }
            else
            {
                MessageBox.Show("Its Draw!");
            }

        }
      
        private void rollDice()
        {
            Random newRandom = new Random();
            if (!isDice1onHold)
            {
                dice1.Text = $"{newRandom.Next(1, 7)}";
            }
            if (!isDice2onHold) { dice2.Text = $"{newRandom.Next(1, 7)}"; }
            if (!isDice3onHold) { dice3.Text = $"{newRandom.Next(1, 7)}"; }
            if (!isDice4onHold) { dice4.Text = $"{newRandom.Next(1, 7)}"; }
            if (!isDice5onHold) { dice5.Text = $"{newRandom.Next(1, 7)}"; }

        }
        private void rolldiceButton_Click(object sender, EventArgs e)
        {
            rollDice();

            if (++countDicePlayer1 == 3)
            {
                countDicePlayer1 = 0;
                label3.Text = "Player 2 Turn!";
                rolldiceButton1.Enabled = false;
                roll2.Enabled = true;
                isPlayer2Playing = true;
                isPlayer1Playing = false;
                CurrentPlayer = PlayerTwo;
            }
            Dictionary<int, int> ScoreNum = new Dictionary<int, int>() { { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 } };
            List<int> ScoreList = new List<int>();
            ScoreList.Add(int.Parse(dice1.Text));
            ScoreList.Add(int.Parse(dice2.Text));
            ScoreList.Add(int.Parse(dice3.Text));
            ScoreList.Add(int.Parse(dice4.Text));
            ScoreList.Add(int.Parse(dice5.Text));
            for (int i = 0; i < 5; i++)
            {
                ScoreNum[ScoreList[i]]++;
            }
            showUpperSectionScore(ScoreNum, text1player1, text2player1, text3player1, text4player1, text5player1, text6player1);
            showLowerSectionScore(ScoreNum, fullplayer1, ssplayer1, lsplayer1, tokplayer1, fokplayer1, yahplayer1, chanceplayer1);
            calcUpperScoreForPlayer();
            //Add a new function to start the game for player 1 and 2
            //ShowtotalScore(upperSectionSumPlayer1, LowerSectionSumPlayer1, bonusplayer1, totalplayer1);
            //showResultToUser();

        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            rollDice();
            if (++countDicePlayer2 == 3)
            {
                countDicePlayer2 = 0;
                label3.Text = "Player 1 Turn!";
                roll2.Enabled = false;
                rolldiceButton1.Enabled = true;
                isPlayer2Playing = false;
                isPlayer1Playing = true;
                CurrentPlayer = PlayerOne;
            }
            Dictionary<int, int> ScoreNum = new Dictionary<int, int>() { { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 } };
            List<int> ScoreList = new List<int>();
            ScoreList.Add(int.Parse(dice1.Text));
            ScoreList.Add(int.Parse(dice2.Text));
            ScoreList.Add(int.Parse(dice3.Text));
            ScoreList.Add(int.Parse(dice4.Text));
            ScoreList.Add(int.Parse(dice5.Text));
            for (int i = 0; i < 5; i++)
            {
                ScoreNum[ScoreList[i]]++;
            }
            showUpperSectionScore(ScoreNum, text1player2, text2player2, text3player2, text4player2, text5player2, text6player2);
            showLowerSectionScore(ScoreNum, fullplayer2, ssplayer2, lsplayer2, tokplayer2, fokplayer2, yahplayer2, chanceplayer2);
            calcUpperScoreForPlayer();
            //ShowtotalScore(upperSectionSumPlayer2, LowerSectionSumPlayer2, bonusplayer2, totalplayer2);
            //showResultToUser();
        }


        private void dice5_Click(object sender, EventArgs e)
        {
            dice5.Location = new Point(756, 300);
            isDice5onHold = true;
        }
        private void dice5DoubleClick(object sender, EventArgs e)
        {
            dice5.Location = new Point(756, 348);
            isDice5onHold = false;
        }

        private void dice1_DoubleClick(object sender, EventArgs e)
        {
            dice1.Location = new Point(236, 348);
            isDice1onHold = false;
        }

        private void ClickHandlerForPlayer1(object sender, EventArgs e)
        {


            switch ((sender as Button).Name)
            {


                case "text1player1":
                    //changeButton(text1player1, upperSectionSumPlayer1, LowerSectionSumPlayer1, true, false,true,false);
                    PlayerOne.OnesScore = int.Parse(text1player1.Text);
                    text1player1.Enabled = false;
                    rolldiceButton1.Enabled = false;
                    roll2.Enabled = true;
                    label3.Text = "Player 2 Turn!";

                    clearEnabledbuttons();
                    resetDiceLocation();
                    CurrentPlayer = PlayerTwo;
                    break;
                case "text2player1":
                    //changeButton(text2player1, upperSectionSumPlayer1, LowerSectionSumPlayer1, true, false,true,false);
                    PlayerOne.TwosScore = int.Parse(text2player1.Text);
                    text2player1.Enabled = false;
                    rolldiceButton1.Enabled = false;
                    roll2.Enabled = true;
                    label3.Text = "Player 2 Turn!";

                    clearEnabledbuttons();
                    resetDiceLocation();
                    CurrentPlayer = PlayerTwo;

                    break;
                case "text3player1":
                    // changeButton(text3player1, upperSectionSumPlayer1, LowerSectionSumPlayer1, true, false,true,false);
                    PlayerOne.ThreesScore = int.Parse(text3player1.Text);
                    text3player1.Enabled = false;
                    rolldiceButton1.Enabled = false;
                    roll2.Enabled = true;
                    label3.Text = "Player 2 Turn!";

                    clearEnabledbuttons();
                    resetDiceLocation();
                    CurrentPlayer = PlayerTwo;
                    break;
                case "text4player1":
                    //changeButton(text4player1, upperSectionSumPlayer1, LowerSectionSumPlayer1, true, false,true,false);
                    PlayerOne.FoursScore = int.Parse(text4player1.Text);
                    text4player1.Enabled = false;
                    rolldiceButton1.Enabled = false;
                    roll2.Enabled = true;
                    label3.Text = "Player 2 Turn!";

                    clearEnabledbuttons();
                    resetDiceLocation();
                    CurrentPlayer = PlayerTwo;
                    break;
                case "text5player1":
                    //changeButton(text5player1, upperSectionSumPlayer1, LowerSectionSumPlayer1, true, false,true,false);
                    PlayerOne.FivesScore = int.Parse(text5player1.Text);
                    text5player1.Enabled = false;
                    rolldiceButton1.Enabled = false;
                    roll2.Enabled = true;
                    label3.Text = "Player 2 Turn!";

                    clearEnabledbuttons();
                    resetDiceLocation();
                    CurrentPlayer = PlayerTwo;
                    break;
                case "text6player1":
                    //changeButton(text6player1, upperSectionSumPlayer1, LowerSectionSumPlayer1, true, false,true,false);
                    PlayerOne.SixesScore = int.Parse(text6player1.Text);
                    text6player1.Enabled = false;
                    rolldiceButton1.Enabled = false;
                    roll2.Enabled = true;
                    label3.Text = "Player 2 Turn!";

                    clearEnabledbuttons();
                    resetDiceLocation();
                    CurrentPlayer = PlayerTwo;
                    break;

                //Lower Part
                case "fullplayer1":
                    //changeButton(fullplayer1, upperSectionSumPlayer1, LowerSectionSumPlayer1, false, true,true,false);
                    PlayerOne.FullHouseScore = int.Parse(fullplayer1.Text);
                    fullplayer1.Enabled = false;
                    rolldiceButton1.Enabled = false;
                    roll2.Enabled = true;
                    label3.Text = "Player 2 Turn!";

                    clearEnabledbuttons();
                    resetDiceLocation();
                    CurrentPlayer = PlayerTwo;
                    break;
                case "tokplayer1":
                    //changeButton(tokplayer1, upperSectionSumPlayer1, LowerSectionSumPlayer1, false, true,true,false);
                    PlayerOne.ThreeOfAKindScore = int.Parse(tokplayer1.Text);
                    tokplayer1.Enabled = false;
                    rolldiceButton1.Enabled = false;
                    roll2.Enabled = true;
                    label3.Text = "Player 2 Turn!";

                    clearEnabledbuttons();
                    resetDiceLocation();
                    CurrentPlayer = PlayerTwo;
                    break;
                case "fokplayer1":
                    //changeButton(fokplayer1, upperSectionSumPlayer1, LowerSectionSumPlayer1, false, true,true,false);
                    PlayerOne.FourOfAKindScore = int.Parse(fokplayer1.Text);
                    fokplayer1.Enabled = false;
                    rolldiceButton1.Enabled = false;
                    roll2.Enabled = true;
                    label3.Text = "Player 2 Turn!";

                    clearEnabledbuttons();
                    resetDiceLocation();
                    CurrentPlayer = PlayerTwo;
                    break;
                case "ssplayer1":
                    //changeButton(ssplayer1, upperSectionSumPlayer1, LowerSectionSumPlayer1, false, true,true,false);
                    PlayerOne.SmallStrightScore = int.Parse(ssplayer1.Text);
                    ssplayer1.Enabled = false;
                    rolldiceButton1.Enabled = false;
                    roll2.Enabled = true;
                    label3.Text = "Player 2 Turn!";

                    clearEnabledbuttons();
                    resetDiceLocation();
                    CurrentPlayer = PlayerTwo;
                    break;
                case "lsplayer1":
                    //changeButton(lsplayer1, upperSectionSumPlayer1, LowerSectionSumPlayer1, false, true,true,false);
                    PlayerOne.LargeStraightScore = int.Parse(lsplayer1.Text);
                    lsplayer1.Enabled = false;
                    rolldiceButton1.Enabled = false;
                    roll2.Enabled = true;
                    label3.Text = "Player 2 Turn!";

                    clearEnabledbuttons();
                    resetDiceLocation();
                    CurrentPlayer = PlayerTwo;
                    break;
                case "yahplayer1":
                    //changeButton(yahplayer1, upperSectionSumPlayer1, LowerSectionSumPlayer1, false, true,true,false);
                    PlayerOne.YahtzeeScore = int.Parse(yahplayer1.Text);
                    yahplayer1.Enabled = false;
                    rolldiceButton1.Enabled = false;
                    roll2.Enabled = true;
                    label3.Text = "Player 2 Turn!";

                    clearEnabledbuttons();
                    resetDiceLocation();
                    CurrentPlayer = PlayerTwo;
                    break;
                case "chanceplayer1":
                    //changeButton(chanceplayer1, upperSectionSumPlayer1, LowerSectionSumPlayer1, false, true,true,false);
                    PlayerOne.ChanceScore = int.Parse(chanceplayer1.Text);
                    chanceplayer1.Enabled = false;
                    rolldiceButton1.Enabled = false;
                    roll2.Enabled = true;
                    label3.Text = "Player 2 Turn!";

                    clearEnabledbuttons();
                    resetDiceLocation();
                    CurrentPlayer = PlayerTwo;
                    break;
            }







        }

        private void clickHandlerforPlayer2(object sender, EventArgs e)
        {
            switch ((sender as Button).Name)
            {
                case "text1player2":
                    //changeButton(text1player2, upperSectionSumPlayer2, LowerSectionSumPlayer2, true, false, false, true);
                    PlayerTwo.OnesScore = int.Parse(text1player2.Text);
                    text1player2.Enabled = false;
                    rolldiceButton1.Enabled = true;
                    roll2.Enabled = false;
                    label3.Text = "Player 1 Turn!";

                    clearEnabledbuttons();
                    resetDiceLocation();
                    CurrentPlayer = PlayerOne;
                    break;
                case "text2player2":
                    //changeButton(text2player2, upperSectionSumPlayer2, LowerSectionSumPlayer2, true, false, false, true);
                    PlayerTwo.TwosScore = int.Parse(text2player2.Text);
                    text2player2.Enabled = false;
                    rolldiceButton1.Enabled = true;
                    roll2.Enabled = false;
                    label3.Text = "Player 1 Turn!";
                    clearEnabledbuttons();
                    resetDiceLocation();
                    CurrentPlayer = PlayerOne;

                    break;
                case "text3player2":
                    //changeButton(text3player2, upperSectionSumPlayer2, LowerSectionSumPlayer2, true, false, false, true);
                    PlayerTwo.ThreesScore = int.Parse(text3player2.Text);
                    text3player2.Enabled = false;
                    rolldiceButton1.Enabled = true;
                    roll2.Enabled = false;
                    label3.Text = "Player 1 Turn!";

                    clearEnabledbuttons();
                    resetDiceLocation();
                    CurrentPlayer = PlayerOne;
                    break;
                case "text4player2":
                    //changeButton(text4player2, upperSectionSumPlayer2, LowerSectionSumPlayer2, true, false, false, true);
                    PlayerTwo.FoursScore = int.Parse(text4player2.Text);
                    text4player2.Enabled = false;
                    rolldiceButton1.Enabled = true;
                    roll2.Enabled = false;
                    label3.Text = "Player 1 Turn!";

                    clearEnabledbuttons();
                    resetDiceLocation();
                    CurrentPlayer = PlayerOne;
                    break;
                case "text5player2":
                    //changeButton(text5player2, upperSectionSumPlayer2, LowerSectionSumPlayer2, true, false, false, true);
                    PlayerTwo.FivesScore = int.Parse(text5player2.Text);
                    text5player2.Enabled = false;
                    rolldiceButton1.Enabled = true;
                    roll2.Enabled = false;
                    label3.Text = "Player 1 Turn!";

                    clearEnabledbuttons();
                    resetDiceLocation();
                    CurrentPlayer = PlayerOne;
                    break;
                case "text6player2":
                    //changeButton(text6player2, upperSectionSumPlayer2, LowerSectionSumPlayer2, true, false, false, true);
                    PlayerTwo.SixesScore = int.Parse(text6player2.Text);
                    text6player2.Enabled = false;
                    rolldiceButton1.Enabled = true;
                    roll2.Enabled = false;
                    label3.Text = "Player 1 Turn!";

                    clearEnabledbuttons();
                    resetDiceLocation();
                    CurrentPlayer = PlayerOne;
                    break;

                //Lower Part
                case "fullplayer2":
                    //changeButton(fullplayer2, upperSectionSumPlayer2, LowerSectionSumPlayer2, false, true, false, true);
                    PlayerTwo.FullHouseScore = int.Parse(fullplayer2.Text);
                    fullplayer2.Enabled = false;
                    rolldiceButton1.Enabled = true;
                    roll2.Enabled = false;
                    label3.Text = "Player 1 Turn!";

                    clearEnabledbuttons();
                    resetDiceLocation();
                    CurrentPlayer = PlayerOne;
                    break;
                case "tokplayer2":
                    //changeButton(tokplayer2, upperSectionSumPlayer2, LowerSectionSumPlayer2, false, true, false, true);
                    PlayerTwo.ThreeOfAKindScore = int.Parse(tokplayer2.Text);
                    tokplayer2.Enabled = false;
                    rolldiceButton1.Enabled = true;
                    roll2.Enabled = false;
                    label3.Text = "Player 1 Turn!";

                    clearEnabledbuttons();
                    resetDiceLocation();
                    CurrentPlayer = PlayerOne;
                    break;
                case "fokplayer2":
                    //changeButton(fokplayer2, upperSectionSumPlayer2, LowerSectionSumPlayer2, false, true, false, true);
                    PlayerTwo.FourOfAKindScore = int.Parse(fokplayer2.Text);
                    fokplayer2.Enabled = false;
                    rolldiceButton1.Enabled = true;
                    roll2.Enabled = false;
                    label3.Text = "Player 1 Turn!";

                    clearEnabledbuttons();
                    resetDiceLocation();
                    CurrentPlayer = PlayerOne;
                    break;
                case "ssplayer2":
                    //changeButton(ssplayer2, upperSectionSumPlayer2, LowerSectionSumPlayer2, false, true, false, true);
                    PlayerTwo.SmallStrightScore = int.Parse(ssplayer2.Text);
                    ssplayer2.Enabled = false;
                    rolldiceButton1.Enabled = true;
                    roll2.Enabled = false;
                    label3.Text = "Player 1 Turn!";

                    clearEnabledbuttons();
                    resetDiceLocation();
                    CurrentPlayer = PlayerOne;
                    break;
                case "lsplayer2":
                    //changeButton(lsplayer2, upperSectionSumPlayer2, LowerSectionSumPlayer2, false, true, false, true);
                    PlayerTwo.LargeStraightScore = int.Parse(lsplayer2.Text);
                    lsplayer2.Enabled = false;
                    rolldiceButton1.Enabled = true;
                    roll2.Enabled = false;
                    label3.Text = "Player 1 Turn!";

                    clearEnabledbuttons();
                    resetDiceLocation();
                    CurrentPlayer = PlayerOne;
                    break;
                case "yahplayer2":
                    //changeButton(yahplayer2, upperSectionSumPlayer2, LowerSectionSumPlayer2, false, true, false, true);
                    PlayerTwo.YahtzeeScore = int.Parse(yahplayer2.Text);
                    yahplayer2.Enabled = false;
                    rolldiceButton1.Enabled = true;
                    roll2.Enabled = false;
                    label3.Text = "Player 1 Turn!";

                    clearEnabledbuttons();
                    resetDiceLocation();
                    CurrentPlayer = PlayerOne;
                    break;
                case "chanceplayer2":
                    //changeButton(chanceplayer2, upperSectionSumPlayer2, LowerSectionSumPlayer2, false, true, false, true);
                    PlayerTwo.ChanceScore = int.Parse(chanceplayer2.Text);
                    chanceplayer2.Enabled = false;
                    rolldiceButton1.Enabled = true;
                    roll2.Enabled = false;
                    label3.Text = "Player 1 Turn!";

                    clearEnabledbuttons();
                    resetDiceLocation();
                    CurrentPlayer = PlayerOne;
                    break;

            }








        }

        private void text6player2_EnabledChanged(object sender, EventArgs e)
        {
            ShowtotalScoreForPlayer();
        }

        private void TexhChangedBlah(object sender, EventArgs e)
        {
            ShowtotalScoreForPlayer();
        }
    }
}
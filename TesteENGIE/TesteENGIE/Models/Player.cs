using System.Collections.Generic;
using System.Linq;
using ExercioPoker.Enums;

namespace ExercioPoker.Models
{
    public class Player
    {
        public List<int> ComparationValues;

        private readonly List<Card> Cards;
        private readonly HandRank Rank;

        public Player(string cardsStr)
        {
            //manually substringing cards
            Cards = new List<Card>
            {
                new Card(cardsStr[0].ToString(), cardsStr[1].ToString()),
                new Card(cardsStr[3].ToString(), cardsStr[4].ToString()),
                new Card(cardsStr[6].ToString(), cardsStr[7].ToString()),
                new Card(cardsStr[9].ToString(), cardsStr[10].ToString()),
                new Card(cardsStr[12].ToString(), cardsStr[13].ToString())
            };
            Cards.OrderBy(x => x.Value);

            //hands of 5 cards for both
            ComparationValues = new List<int> { 0, 0, 0, 0, 0 };

            Rank = getHandRank();
            setValueCardsToCompare();
        }
        public int CompareTo(Player otherPlayer)
        {
            //Black Wins
            if (Rank > otherPlayer.Rank)
                return 1;

            //White Wins
            if (Rank < otherPlayer.Rank)
                return -1;

            for (var i = 0; i < 5; i++)
            {
                //Black wins in case of higher card
                if (ComparationValues[i] > otherPlayer.ComparationValues[i])
                    return 1;

                //White wins in case of higher card
                if (ComparationValues[i] < otherPlayer.ComparationValues[i])
                    return -1;
            }

            //Tie
            return 0;
        }

        #region Private Methods

        private HandRank getHandRank()
        {
            var isStraightFlush = hasStraightFlush();            
            var isFourKind = hasFourOfAKind();
            var isThreeKind = hasThreeOfAKind();
            var isFlush = hasFlush();
            var numberOfPairs = getNumberOfPairs();

            //Check ranks in order of importante

            if (isStraightFlush)
                return HandRank.STRAIGHT_FLUSH;
            if (isFourKind)
                return HandRank.FOUR_KIND;
            if (isThreeKind && numberOfPairs == 2)
                return HandRank.FULL_HOUSE;
            if (isFlush)
                return HandRank.FLUSH;
            if (isStraightFlush)
                return HandRank.STRAIGHT;
            if (isThreeKind)
                return HandRank.THREE_KIND;
            if (numberOfPairs == 2)
                return HandRank.TWO_PAIR;
            if (numberOfPairs == 1)
                return HandRank.PAIR;

            return HandRank.HIGH_CARD;
        }
        private void setValueCardsToCompare()
        {
            switch (Rank)
            {
                case HandRank.STRAIGHT_FLUSH:
                case HandRank.STRAIGHT:
                    {
                        ComparationValues[0] = Cards[4].Value;
                        return;
                    }

                case HandRank.FLUSH:
                case HandRank.HIGH_CARD:
                    {
                        for (var i = 0; i < 5; ++i)
                        {
                            ComparationValues[i] = Cards[4 - i].Value;
                        }
                        return;
                    }

                case HandRank.FULL_HOUSE:
                case HandRank.FOUR_KIND:
                case HandRank.THREE_KIND:
                    {
                        ComparationValues[0] = Cards[2].Value;
                        return;
                    }

                case HandRank.TWO_PAIR:
                    {
                        if (Cards[0].Value != Cards[1].Value)
                        {
                            //5 77 88
                            ComparationValues[0] = Cards[4].Value;
                            ComparationValues[1] = Cards[2].Value;
                            ComparationValues[2] = Cards[0].Value;
                        }
                        else if (Cards[2].Value != Cards[3].Value)
                        {
                            //22 8 33
                            ComparationValues[0] = Cards[4].Value;
                            ComparationValues[1] = Cards[0].Value;
                            ComparationValues[2] = Cards[2].Value;
                        }
                        else
                        {
                            //88 66 7
                            ComparationValues[0] = Cards[2].Value;
                            ComparationValues[1] = Cards[0].Value;
                            ComparationValues[2] = Cards[4].Value;
                        }
                        return;
                    }

                case HandRank.PAIR:
                    {
                        var pairValue = 0;
                        for (int num = 0; num < 3; ++num)
                        {
                            if (Cards[num].Value == Cards[num + 1].Value)
                            {
                                pairValue = Cards[num].Value;
                                break;
                            }
                        }
                        ComparationValues[0] = pairValue;

                        for (int num = 4, value = 1; num >= 0; num--)
                        {
                            if (Cards[num].Value == pairValue)
                                continue;

                            ComparationValues[value++] = Cards[num].Value;
                        }
                        break;
                    }                 
            }
        }
        private bool hasStraightFlush()
        {
            for (var i = 0; i < 4; i++)
                if (Cards[i].Value + 1 != Cards[i + 1].Value)
                    return false;

            return true;
        }
        private bool hasFlush()
        {
            for (var i = 0; i < 4; i++)
                if (Cards[i].Suit != Cards[i + 1].Suit)
                    return false;

            return true;
        }
        private bool hasFourOfAKind()
        {
            if (Cards[0].Value == Cards[1].Value && Cards[1].Value == Cards[2].Value && Cards[2].Value == Cards[3].Value)
                return true;
            if (Cards[1].Value == Cards[2].Value && Cards[2].Value == Cards[3].Value && Cards[3].Value == Cards[4].Value)
                return true;

            return false;
        }
        private bool hasThreeOfAKind()
        {
            if (Cards[0].Value == Cards[2].Value)
                return true;

            if (Cards[1].Value == Cards[3].Value)
                return true;

            if (Cards[2].Value == Cards[4].Value)
                return true;

            return false;
        }
        private int getNumberOfPairs()
        {
            var counter = 0;
            for (var num = 0; num < 4; num++)
            {
                if (Cards[num].Value == Cards[num + 1].Value)
                {
                    counter++;
                    num++;
                }
            }

            return counter;
        }

        #endregion


    }
}

using System.Collections.Generic;
using System.Linq;
using ExercioPoker.Enums;

namespace ExercioPoker.Models
{
    public class Player
    {
        private readonly int CARDS_ON_HAND = 5;

        private readonly IList<int> ValuesOnHand;
        private readonly IList<Card> Cards;
        private HandRank Rank;

        public Player(string cardsStr)
        {
            Cards = new List<Card>();
            ValuesOnHand = new List<int>();

            SetHand(cardsStr);
        }
        public int CompareHands(Player otherPlayer)
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
                if (ValuesOnHand[i] > otherPlayer.ValuesOnHand[i])
                    return 1;
                //White wins in case of higher card
                if (ValuesOnHand[i] < otherPlayer.ValuesOnHand[i])
                    return -1;
            }

            //Tie
            return 0;
        }

        #region Private Methods

        private void SetHand(string cardsStr)
        {
            for (int i = 0; i < (CARDS_ON_HAND * 3); i += 3)
                Cards.Add(new Card(cardsStr[i], cardsStr[i + 1]));

            for (int i = 0; i < CARDS_ON_HAND; i++)
                ValuesOnHand.Add(0);

            Cards.OrderBy(x => x.Value);

            Rank = GetHandRank();
            SetValueCardsToCompare();
        }

        private HandRank GetHandRank()
        {
            var isStraightFlush = HasStraightFlush();
            var isFourKind = HasFourOfAKind();
            var isThreeKind = HasThreeOfAKind();
            var isFlush = HasFlush();
            var numberOfPairs = GetNumberOfPairs();

            //Check ranks in order of importante

            if (isStraightFlush)
                return HandRank.STRAIGHT_FLUSH;
            if (isFourKind)
                return HandRank.FOUR_OF_A_KIND;
            if (isThreeKind && numberOfPairs == 2)
                return HandRank.FULL_HOUSE;
            if (isFlush)
                return HandRank.FLUSH;
            if (isStraightFlush)
                return HandRank.STRAIGHT;
            if (isThreeKind)
                return HandRank.THREE_OF_A_KIND;
            if (numberOfPairs == 2)
                return HandRank.TWO_PAIRS;
            if (numberOfPairs == 1)
                return HandRank.ONE_PAIR;

            return HandRank.HIGH_CARD;
        }
        private void SetValueCardsToCompare()
        {
            switch (Rank)
            {
                case HandRank.STRAIGHT_FLUSH:
                case HandRank.STRAIGHT:
                    {
                        ValuesOnHand[0] = Cards[4].Value;
                        return;
                    }

                case HandRank.FLUSH:
                case HandRank.HIGH_CARD:
                    {
                        for (var i = 0; i < 5; ++i)
                        {
                            ValuesOnHand[i] = Cards[4 - i].Value;
                        }
                        return;
                    }

                case HandRank.FULL_HOUSE:
                case HandRank.FOUR_OF_A_KIND:
                case HandRank.THREE_OF_A_KIND:
                    {
                        ValuesOnHand[0] = Cards[2].Value;
                        return;
                    }

                case HandRank.TWO_PAIRS:
                    {
                        if (Cards[0].Value != Cards[1].Value)
                        {
                            //5 77 88
                            ValuesOnHand[0] = Cards[4].Value;
                            ValuesOnHand[1] = Cards[2].Value;
                            ValuesOnHand[2] = Cards[0].Value;
                        }
                        else if (Cards[2].Value != Cards[3].Value)
                        {
                            //22 8 33
                            ValuesOnHand[0] = Cards[4].Value;
                            ValuesOnHand[1] = Cards[0].Value;
                            ValuesOnHand[2] = Cards[2].Value;
                        }
                        else
                        {
                            //88 66 7
                            ValuesOnHand[0] = Cards[2].Value;
                            ValuesOnHand[1] = Cards[0].Value;
                            ValuesOnHand[2] = Cards[4].Value;
                        }
                        return;
                    }

                case HandRank.ONE_PAIR:
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
                        ValuesOnHand[0] = pairValue;

                        for (int num = 4, value = 1; num >= 0; num--)
                        {
                            if (Cards[num].Value == pairValue)
                                continue;

                            ValuesOnHand[value++] = Cards[num].Value;
                        }
                        break;
                    }
            }
        }
        private bool HasStraightFlush()
        {
            for (var i = 0; i < 4; i++)
                if (Cards[i].Value + 1 != Cards[i + 1].Value)
                    return false;

            return true;
        }
        private bool HasFlush()
        {
            for (var i = 0; i < 4; i++)
                if (Cards[i].Suit != Cards[i + 1].Suit)
                    return false;

            return true;
        }
        private bool HasFourOfAKind()
        {
            if (Cards[0].Value == Cards[1].Value && Cards[1].Value == Cards[2].Value && Cards[2].Value == Cards[3].Value)
                return true;
            if (Cards[1].Value == Cards[2].Value && Cards[2].Value == Cards[3].Value && Cards[3].Value == Cards[4].Value)
                return true;

            return false;
        }
        private bool HasThreeOfAKind()
        {
            if (Cards[0].Value == Cards[2].Value)
                return true;

            if (Cards[1].Value == Cards[3].Value)
                return true;

            if (Cards[2].Value == Cards[4].Value)
                return true;

            return false;
        }
        private int GetNumberOfPairs()
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

namespace ExercioPoker.Models
{
    public class Card
    {
        public string Suit;
        public int Value;

        public Card(string cardValue, string cardSuit)
        {
            ConvertValues(cardValue);
            Suit = cardSuit;
        }

        /// <summary>
        /// Transform from A to K into number.
        /// </summary>
        /// <param name="cardValue"></param>
        private void ConvertValues(string cardValue)
        {
            switch (cardValue)
            {
                case "A":
                    Value = 14;
                    return;

                case "K":
                    Value = 13;
                    return;

                case "Q":
                    Value = 12;
                    return;

                case "J":
                    Value = 11;
                    return;

                case "T":
                    Value = 10;
                    return;

                default:
                    Value = int.Parse(cardValue);
                    return;
            }
        }
    }
}

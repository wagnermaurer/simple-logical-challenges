namespace ExercioPoker.Models
{
    public class Card
    {
        public int Value { get; private set; }
        public char Suit { get; private set; }

        public Card(
            char cardValue,
            char cardSuit
            )
        {
            ConvertValues(cardValue);
            Suit = cardSuit;
        }

        /// <summary>
        /// Transform card letters into values.
        /// </summary>
        /// <param name="cardValue"></param>
        private void ConvertValues(char cardValue)
        {
            switch (cardValue)
            {
                case 'T':
                    Value = 10;
                    return;
                case 'J':
                    Value = 11;
                    return;
                case 'Q':
                    Value = 12;
                    return;
                case 'K':
                    Value = 13;
                    return;
                case 'A':
                    Value = 14;
                    return;
                default:
                    Value = int.Parse(cardValue.ToString());
                    return;
            }
        }
    }
}

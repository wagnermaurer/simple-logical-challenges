using ExercioPoker.Models;
using NUnit.Framework;

namespace UnitTests
{
    public class Exercicio1Tests
    {
        private int ComparePlayersHands(Player blacksPlayer, Player whitesPlayer)
            => blacksPlayer.CompareHands(whitesPlayer);

        [TestCase("2H 3D 5S 9C KD", "2C 3H 4S 8C KH")]
        public void MakeSureThatBlackWins(string hand1, string hand2)
        {
            var expectedResult = 1;
            Assert.IsTrue(ComparePlayersHands(new Player(hand1), new Player(hand2)) == expectedResult);
        }

        [TestCase("2H 3D 5S 9C KD", "2C 3H 4S 8C AH")]
        [TestCase("2H 4S 4C 2D 4H", "2S 8S AS QS 3S")]
        public void MakeSureThatWhiteWins(string hand1, string hand2)
        {
            var expectedResult = -1;
            Assert.IsTrue(ComparePlayersHands(new Player(hand1), new Player(hand2)) == expectedResult);
        }

        [TestCase("2H 3D 5S 9C KD", "2D 3H 5C 9S KH")]
        public void MakeSureThatItIsATie(string hand1, string hand2)
        {
            var expectedResult = 0;
            Assert.IsTrue(ComparePlayersHands(new Player(hand1), new Player(hand2)) == expectedResult);

        }
    }
}
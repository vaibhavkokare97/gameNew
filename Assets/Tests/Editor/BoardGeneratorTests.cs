using Assets.Scripts.Card;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Tests.Editor
{
	public class BoardGeneratorTests : MonoBehaviour
	{

        [Test]
        public void GenerateBoard_ReturnsCorrectCardCount()
        {
            List<int> board =
                BoardGenerator.GenerateBoard(4, 4, 25, 123);

            Assert.AreEqual(16, board.Count);
        }

        [Test]
        public void SameSeed_ProducesSameBoard()
        {
            List<int> boardA =
                BoardGenerator.GenerateBoard(4, 4, 25, 12345);

            List<int> boardB =
                BoardGenerator.GenerateBoard(4, 4, 25, 12345);

            CollectionAssert.AreEqual(boardA, boardB);
        }

        [Test]
        public void DifferentSeeds_ProduceDifferentBoards()
        {
            List<int> boardA =
                BoardGenerator.GenerateBoard(4, 4, 25, 12345);

            List<int> boardB =
                BoardGenerator.GenerateBoard(4, 4, 25, 54321);

            CollectionAssert.AreNotEqual(boardA, boardB);
        }

        [Test]
        public void EveryCardAppearsExactlyTwice_OnEvenBoard()
        {
            List<int> board =
                BoardGenerator.GenerateBoard(4, 4, 25, 123);

            Dictionary<int, int> counts = new();

            foreach (int card in board)
            {
                if (!counts.ContainsKey(card))
                    counts[card] = 0;

                counts[card]++;
            }

            foreach (var pair in counts)
            {
                Assert.AreEqual(2, pair.Value);
            }
        }

        [Test]
        public void OddBoardContainsSingleBlocker()
        {
            List<int> board =
                BoardGenerator.GenerateBoard(3, 3, 25, 123);

            int blockerCount = 0;

            foreach (int card in board)
            {
                if (card == -1)
                    blockerCount++;
            }

            Assert.AreEqual(1, blockerCount);
        }

        [Test]
        public void OddBoardHasCorrectCellCount()
        {
            List<int> board =
                BoardGenerator.GenerateBoard(3, 3, 25, 123);

            Assert.AreEqual(9, board.Count);
        }

        [Test]
        public void Throws_WhenBoardRequiresMoreUniqueCardsThanAvailable()
        {
            Assert.Throws<System.ArgumentException>(() =>
            {
                BoardGenerator.GenerateBoard(
                    rows: 6,
                    cols: 6,
                    availableUniqueCards: 5,
                    seed: 123);
            });
        }
    }
}
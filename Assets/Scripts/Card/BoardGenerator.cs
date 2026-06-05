using System;
using System.Collections.Generic;

namespace Assets.Scripts.Card
{
    public static class BoardGenerator
    {
        public static List<int> GenerateBoard(int rows, int cols, int availableUniqueCards, int seed)
        {
            if (availableUniqueCards <= 0)
                throw new ArgumentException("Error: available unique cards must be greater than zero.");

            int totalCells = rows * cols;

            bool hasBlocker = totalCells % 2 != 0;

            int pairCount = totalCells / 2;

            if (pairCount > availableUniqueCards) throw new ArgumentException("Error: not enough unique cards to fill the board.");

            Random random = new Random(seed);

            List<int> availableCards = new();

            for (int i = 0; i < availableUniqueCards; i++)
            {
                availableCards.Add(i);
            }

            Shuffle(availableCards, random);

            List<int> boardCards = new();

            for (int i = 0; i < pairCount; i++)
            {
                int cardId = availableCards[i];

                boardCards.Add(cardId);
                boardCards.Add(cardId);
            }

            if (hasBlocker)
            {
                boardCards.Add(-1);
            }

            Shuffle(boardCards, random);

            return boardCards;
        }

        private static void Shuffle<T>(List<T> list, Random random)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);

                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Card
{
    public class CardController : MonoBehaviour
    {
        public Sprite defaultSprite;
        public AllCardsGameData allCardsGameData;

        public Transform cardUIParent;
        public CardView cardViewPrefab;

        public CardView lastDrawnCard = null;

        public static Action<List<int>> OnBoardGenerateCallback;

        public static Action<int> OnTurnComplete;
        public static Action<int> OnMatchComplete;
        public static Action OnFlipInitiated;

        private void Start()
        {
            List<int> board = BoardGenerator.GenerateBoard(4, 4, allCardsGameData.AllCards.Length, 12345);
            OnBoardGenerateCallback?.Invoke(board);

            LayBoard(board);
        }

        void LayBoard(List<int> board)
        {
            foreach (int i in board)
            {
                if(i == -1)
                {
                    Instantiate(cardViewPrefab, cardUIParent).GetComponent<CardView>().cardSprite = defaultSprite;
                    continue;
                }

                CardView cardView = Instantiate(cardViewPrefab, cardUIParent).GetComponent<CardView>();
                cardView.cardSprite = allCardsGameData.AllCards[i];
                cardView.cardId = i;
            }
        }

        
    }
}
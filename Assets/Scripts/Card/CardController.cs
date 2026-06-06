using Assets.Scripts.Controller;
using Assets.Scripts.UI;
using System;
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

        public static Action<(int, int), int, int> OnBoardGenerateCallback;

        public static Action<int> OnTurnComplete;
        public static Action<int> OnMatchComplete;
        public static Action OnFlipInitiated;

        private void OnEnable()
        {
            UIManager.OnGameStopUI += ClearBoard;
        }

        private void ClearBoard()
        {
            foreach (Transform child in cardUIParent)
            {
                Destroy(child.gameObject);
            }
            lastDrawnCard = null;
        }

        public void Initiate((int, int) levelDimensions, int levelDifficult3y, int seed)
        {
            List<int> board = BoardGenerator.GenerateBoard(levelDimensions.Item1, levelDimensions.Item2, allCardsGameData.AllCards.Length, seed);
            OnBoardGenerateCallback?.Invoke(levelDimensions, levelDifficult3y,seed);

            LayBoard(board);
        }

        public bool IsCardSolved(int cardId)
        {
            return GameManager.Instance.IsCardSolved(cardId);
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

        private void OnDisable()
        {
            UIManager.OnGameStopUI -= ClearBoard;
        }
    }
}
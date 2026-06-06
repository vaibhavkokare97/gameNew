using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Scripts.Card;
using System;
using Assets.Scripts.State;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.Score;

namespace Assets.Scripts.UI
{
	public class GameMenuUI : MonoBehaviour
	{
        [SerializeField] private Transform gameMenuPanel;
        [SerializeField] private GridLayoutGroup gridLayoutGroup;

        [SerializeField] private Button homeButton;

        private void Awake()
        {
            homeButton.onClick.AddListener(() =>
            {
                UIManager.OnGameStopUI?.Invoke(false);
            });
        }

        private void Update()
        {
            if(AppState.currentState == AppState.State.InGame && Input.GetKeyDown(KeyCode.Escape))
            {
                UIManager.OnGameStopUI?.Invoke(false);
            }
        }

        private void OnEnable()
        {
            UIManager.OnGameStartUI += ((int, int) tuple, int arg2, int arg3) => gameMenuPanel.gameObject.SetActive(true);
            UIManager.OnGameStopUI += (bool? gameWon) => gameMenuPanel.gameObject.SetActive(false);
            UIManager.OnGameStartUI += AdjustGridLayout;
        }

        private void OnDisable()
        {
            UIManager.OnGameStartUI -= AdjustGridLayout;
            UIManager.OnGameStartUI = ((int, int) tuple, int arg2, int arg3) => gameMenuPanel.gameObject.SetActive(true);
            UIManager.OnGameStopUI -= (bool? gameWon) => gameMenuPanel.gameObject.SetActive(false);
        }

        private void AdjustGridLayout((int, int) levelDimensions, int levelDifficulty, int seed)
        {
            gridLayoutGroup.constraintCount = levelDimensions.Item2;

            if(levelDimensions.Item1 * levelDimensions.Item2 <= 4)
            {
                gridLayoutGroup.cellSize = new Vector2(200, 200);
            }
            else if (levelDimensions.Item1 * levelDimensions.Item2 <= 16)
            {
                gridLayoutGroup.cellSize = new Vector2(150, 150);
            }
            else
            {
                gridLayoutGroup.cellSize = new Vector2(120, 120);
            }
        }


    }
}
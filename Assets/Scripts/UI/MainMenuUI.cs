using Assets.Scripts.SaveLoad;
using Assets.Scripts.Score;
using Assets.Scripts.State;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Transform mainMenuPanel;

        [SerializeField] private ToggleGroup difficultyToggleGroup;
        [SerializeField] private TMP_InputField seedInput;
        [SerializeField] private Button startButton;
        [SerializeField] private Button loadButton;

        [SerializeField] private Button quitButton;

        private void Awake()
        {
            startButton.onClick.AddListener(delegate
            {
                Toggle activeToggle = difficultyToggleGroup.GetFirstActiveToggle();

                int selectedDifficulty = activeToggle == null
                    ? -1
                    : activeToggle.transform.GetSiblingIndex();

                int seed = int.TryParse(seedInput.text, out int parsedSeed) ? parsedSeed : UnityEngine.Random.Range(0, 999999999);
                if (selectedDifficulty == -1)
                {
                    Debug.LogError("Please select a difficulty level.");
                    return;
                }
                UIManager.OnGameStartUI?.Invoke(GetBoardSizeForDifficulty(selectedDifficulty), selectedDifficulty, seed);
            });

            loadButton.onClick.AddListener(delegate
            {
                SaveData saveData = SaveManager.Load();

                if(saveData == null)
                {
                    Debug.LogError("No save data found. Please start a new game.");
                    return;
                }

                int selectedDifficulty = saveData.difficultyLevel;

                int seed = saveData.seed;


                UIManager.OnGameStartUI?.Invoke(GetBoardSizeForDifficulty(selectedDifficulty), selectedDifficulty, seed);
                UIManager.OnLoadState?.Invoke(saveData.solvedIDs, saveData.matchCount, saveData.turnCount);
            });

            quitButton.onClick.AddListener(() => Application.Quit());

        }

        private void OnEnable()
        {
            UIManager.OnGameStartUI += ((int, int) tuple, int arg2, int arg3) => mainMenuPanel.gameObject.SetActive(false);
            UIManager.OnGameStopUI += () => mainMenuPanel.gameObject.SetActive(true);
        }

        private void Update()
        {
            if (AppState.currentState == AppState.State.MainMenu && Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

        private (int, int) GetBoardSizeForDifficulty(int difficulty)
        {
            return difficulty switch
            {
                0 => (2, 2),
                1 => (2, 3),
                2 => (3, 3),
                3 => (4, 3),
                4 => (4, 4),
                5 => (5, 3),
                6 => (5, 4),
                7 => (5, 5),
                _ => throw new ArgumentException("Invalid difficulty level selected.")
            };
        }

        private void OnDisable()
        {
            UIManager.OnGameStartUI -= ((int, int) tuple, int arg2, int arg3) => mainMenuPanel.gameObject.SetActive(false);
            UIManager.OnGameStopUI -= () => mainMenuPanel.gameObject.SetActive(true);
        }
    }
}
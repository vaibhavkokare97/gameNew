using Assets.Scripts.SaveLoad;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Score
{
    public class ScoreManager
    {
        public ScoreManager()
        {
            Reset();
        }

        public int Seed;
        public int DifficultyLevel;
        public (int, int) LevelDimension;
        public HashSet<int> SolvedIDs = new HashSet<int>();
        public int MatchCount { get; private set; }
        public int TurnCount { get; private set; }

        public static Action<int, int> OnScoreUpdated;
        public static Action OnGameWon;

        public void IncrementMatches()
        {
            MatchCount++;
            OnScoreUpdated?.Invoke(MatchCount, TurnCount);

            if(MatchCount == Mathf.FloorToInt((float)LevelDimension.Item1 * LevelDimension.Item2 / 2))
            {
                // All cards matched
                // GAME won
                Debug.Log("Game Won!");
                OnGameWon?.Invoke();
            }
        }

        public void IncrementTurns()
        {
            TurnCount++;
            OnScoreUpdated?.Invoke(MatchCount, TurnCount);
        }

        public void SetScore(int matchCount, int turnCount)
        {
            MatchCount = matchCount;
            TurnCount = turnCount;
            OnScoreUpdated?.Invoke(MatchCount, TurnCount);
        }

        public void Reset()
        {
            MatchCount = 0;
            TurnCount = 0;
            Seed = 0;
            DifficultyLevel = 0;
            SolvedIDs.Clear();

            OnScoreUpdated?.Invoke(MatchCount, TurnCount);
        }
    }
}
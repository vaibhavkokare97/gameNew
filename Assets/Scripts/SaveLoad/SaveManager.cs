using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.SaveLoad
{
    public static class SaveManager
    {
        private const string SaveKey = "MemoryGameSave";

        public static void Save(SaveData data)
        {
            string json = JsonUtility.ToJson(data);

            PlayerPrefs.SetString(SaveKey, json);
            PlayerPrefs.Save();
        }

        public static SaveData Load()
        {
            if (!PlayerPrefs.HasKey(SaveKey))
                return null;

            string json = PlayerPrefs.GetString(SaveKey);

            return JsonUtility.FromJson<SaveData>(json);
        }

        public static void DeleteSave()
        {
            PlayerPrefs.DeleteKey(SaveKey);
        }
    }

    [Serializable]
    public class SaveData
    {
        public int seed;
        public int difficultyLevel;
        public int[] solvedIDs;
        public int matchCount;
        public int turnCount;

        public SaveData(int difficultyLevel, int seed, int[] solvedIDs = null, int matchCount = 0, int turnCount = 0)
        {
            this.difficultyLevel = difficultyLevel;
            this.seed = seed;
            this.solvedIDs = solvedIDs ?? Array.Empty<int>();
            this.matchCount = matchCount;
            this.turnCount = turnCount;
        }
    }
}
using Main.Options;
using Main.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Main
{
    public static class SaveLoadSystem
    {
        private const string MaxCompletedLevelKey = "MaxLevel";
        private const string DifficultyKey = "Difficulty";
        public const string MasterVolumeKey = "MasterVolume";
        public const string MusicVolumeKey = "MusicVolume";
        public const string SFXVolumeKey = "SFXVolume";
        private static LevelPreviewData[] _levels;

        public static void Initialize(LevelPreviewData[] levels)
        {
            _levels = levels;
        }

        public static void SaveSound(SoundSettings settings)
        {
            PlayerPrefs.SetFloat(MasterVolumeKey, settings.Master);
            PlayerPrefs.SetFloat(SFXVolumeKey, settings.SFX);
            PlayerPrefs.SetFloat(MusicVolumeKey, settings.Music);
        }

        public static SoundSettings LoadSound()
        {
            SoundSettings sound = new();
            sound.Master = PlayerPrefs.GetFloat(MasterVolumeKey, 0.5f);
            sound.SFX = PlayerPrefs.GetFloat(SFXVolumeKey, 0.5f);
            sound.Music = PlayerPrefs.GetFloat(MusicVolumeKey, 0.5f);
            return sound;
        }

        public static void UnlockCompletedLevels()
        {
            int index = PlayerPrefs.GetInt(MaxCompletedLevelKey, 0);
            IEnumerable<LevelPreviewData> accessibleLevels = _levels.Take(index + 2);

            foreach (var level in accessibleLevels)
            {
                level.IsAccessible = true;
            }
        }

        public static void SaveLevel(int currentLevelIndex)
        {
            SaveMaxCompletedLevel(currentLevelIndex);
        }

        public static void SaveDifficulty(int difficultyIndex)
        {
            PlayerPrefs.SetInt(DifficultyKey, difficultyIndex);
        }

        public static int LoadDifficulty()
        {
            return PlayerPrefs.GetInt(DifficultyKey, 0);
        }

        private static void SaveMaxCompletedLevel(int currentLevelIndex)
        {
            int savedIndex = PlayerPrefs.GetInt(MaxCompletedLevelKey, 0);
            if (currentLevelIndex > savedIndex)
                PlayerPrefs.SetInt(MaxCompletedLevelKey, currentLevelIndex);
        }
    }
}
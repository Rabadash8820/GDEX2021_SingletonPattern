using FlappyClone.Shared;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.Events;

namespace FlappyClone.Singletons
{
    public class HighScoreManagerSingleton : Singleton<HighScoreManagerSingleton>
    {
        public const string DefaultPlayerPrefKey = "HighScore";
        public const bool DefaultShowNewHighEffectsFirstTime = false;
        public const float DefaultNewHighFontSize = 40f;
        public static readonly Color DefaultNewHighFontColor = Color.green;

        private bool _loaded = false;
        private bool _alreadyHigh = false;

        public int CurrentHighScore;
        public string PlayerPrefKey = DefaultPlayerPrefKey;

        [Header("New high effects")]
        public bool ShowNewHighEffectsFirstTime = DefaultShowNewHighEffectsFirstTime;
        public float NewHighFontSize = DefaultNewHighFontSize;
        public Color NewHighFontColor = DefaultNewHighFontColor;
        public UnityEvent NewHigh = new UnityEvent();

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Reset()
        {
            PlayerPrefKey = DefaultPlayerPrefKey;

            ShowNewHighEffectsFirstTime = DefaultShowNewHighEffectsFirstTime;
            NewHighFontSize = DefaultNewHighFontSize;
            NewHighFontColor = DefaultNewHighFontColor;
        }

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Start()
        {
            CurrentHighScore = PlayerPrefs.GetInt(PlayerPrefKey, 0);
            _loaded = CurrentHighScore > 0;
            Debug.Log(_loaded
                ? $"Successfully loaded high score of {CurrentHighScore} from PlayerPref '{PlayerPrefKey}'"
                : $"High score not loaded from PlayerPref '{PlayerPrefKey}'"
            );
        }

        public void Delete()
        {
            PlayerPrefs.DeleteKey(PlayerPrefKey);
            Debug.Log($"Successfully deleted high score from PlayerPref '{PlayerPrefKey}'");
        }

        public void CheckForNewHigh()
        {
            ScorekeeperSingleton scorekeeper = ScorekeeperSingleton.Instance;
            if (scorekeeper.CurrentScore <= CurrentHighScore)
                return;

            int prevHigh = CurrentHighScore;
            CurrentHighScore = scorekeeper.CurrentScore;
            if (_alreadyHigh)
                return;

            _alreadyHigh = true;
            Debug.Log($"New high score of {scorekeeper.CurrentScore} (previous was {prevHigh})");
            if (!_loaded && !ShowNewHighEffectsFirstTime) {
                Debug.Log($"Not showing new high effects because '{nameof(ShowNewHighEffectsFirstTime)}' flag was not set");
                return;
            }

            scorekeeper.ScoreTxt.fontSize = NewHighFontSize;
            scorekeeper.ScoreTxt.color = NewHighFontColor;
            NewHigh.Invoke();
        }

        public void Save()
        {
            PlayerPrefs.SetInt(PlayerPrefKey, CurrentHighScore);
            Debug.Log($"Successfully saved high score of {CurrentHighScore} at PlayerPref '{PlayerPrefKey}'");
        }
    }
}

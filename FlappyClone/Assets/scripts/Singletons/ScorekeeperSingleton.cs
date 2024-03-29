using System.Diagnostics.CodeAnalysis;
using TMPro;
using UnityEngine;

namespace FlappyClone.Singletons
{
    public class ScorekeeperSingleton : Singleton<ScorekeeperSingleton>
    {
        public const string DefaultFormatString = "Score: {0:##,##0}";
        public const int DefaultInitialScore = 0;
        public const int DefaultMaxScore = 99_999;
        public const string DefaultOverMaxString = "Score: 99,999+";

        public int CurrentScore = 0;

        [Space]
        public TMP_Text ScoreTxt;
        public string ScoreFormatString = DefaultFormatString;
        public int InitialScore = DefaultInitialScore;
        public int MaxScore = DefaultMaxScore;
        public string OverMaxString = DefaultOverMaxString;

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Reset()
        {
            ScoreTxt = null;
            ScoreFormatString = DefaultFormatString;
            InitialScore = DefaultInitialScore;
            MaxScore = DefaultMaxScore;
            OverMaxString = DefaultOverMaxString;
        }

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
        private void Start()
        {
            CurrentScore = InitialScore;
            updateText();
        }

        public void Increment()
        {
            ++CurrentScore;
            updateText();
        }

        private void updateText()
        {
            if (ScoreTxt != null)
                ScoreTxt.text = CurrentScore > MaxScore ? OverMaxString : string.Format(ScoreFormatString, CurrentScore);
        }
    }
}

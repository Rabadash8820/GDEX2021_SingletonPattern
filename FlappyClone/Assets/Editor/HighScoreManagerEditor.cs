using UnityEditor;
using UnityEngine;

namespace FlappyClone
{
    [CustomEditor(typeof(HighScoreManager))]
    public class HighScoreManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (GUILayout.Button("Delete saved high score"))
                (target as HighScoreManager).Delete();
        }
    }
}

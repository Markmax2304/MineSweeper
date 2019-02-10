using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MineSweeper
{
    [CustomEditor(typeof(GameEngine))]
    public class GameEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            GameEngine engine = (GameEngine)target;
            if (GUILayout.Button("Reset"))
            {
                engine.ResetMap();
            }
        }
    }
}

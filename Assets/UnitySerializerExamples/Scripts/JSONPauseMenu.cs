﻿using UnityEngine;
using System.Collections.Generic;

public class JSONPauseMenu : MonoBehaviour {
    [SerializeField]
    private bool paused = false;
    [SerializeField]
    private GUITexture pausedGUI;
    [SerializeField]
    private string gameName = "Your Game";

    [SerializeField]
    private List<Transform> myList = new List<Transform>();

    private void Start() {
        if (pausedGUI)
            pausedGUI.enabled = false;
    }

    private void Update() {
        if (Input.GetKeyUp(KeyCode.P)) {
            paused = !paused;

            if (paused) {
                Time.timeScale = 0.0f;
                Time.fixedDeltaTime = 0.0f;

                if (pausedGUI)
                    pausedGUI.enabled = true;
            }
            else {
                Time.timeScale = 1.0f;
                Time.fixedDeltaTime = Time.timeScale * 0.02f;

                if (pausedGUI)
                    pausedGUI.enabled = false;
            }
        }
    }

    private void OnGUI() {
        if (!paused) {
            GUILayout.BeginArea(new Rect(200, 10, 400, 20));
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Press P to Pause");
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            GUILayout.EndArea();
            return;
        }

        GUIStyle box = "box";
        GUILayout.BeginArea(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 300, 400, 600), box);

        GUILayout.BeginVertical();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Save Game")) {
            JSONLevelSerializer.SaveGame(gameName);
        }
        GUILayout.Space(60);
        foreach (JSONLevelSerializer.SaveEntry sg in JSONLevelSerializer.SavedGames[JSONLevelSerializer.PlayerName]) {
            if (GUILayout.Button(sg.Caption)) {
                sg.Load();
                Time.timeScale = 1.0f;
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
            }
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBarScript : MonoBehaviour
{
    public MazeScript mazeScript;
    public TextMeshProUGUI tmpTarget, tmpScore;

    void Start() {
        tmpTarget.text = mazeScript.maze.gel.path.Count.ToString();
    }

    void Update() {
        tmpScore.text = mazeScript.maze.totalTiles.ToString();
    }
}

using Assets.Code;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBarScript : MonoBehaviour
{
    public MazeScript mazeScript;
    public RectTransform rectTransform;
    public TextMeshProUGUI tmpTarget, tmpScore;

    void Start() {
        Vector3 anchoredPosition = rectTransform.anchoredPosition;
        anchoredPosition.y = 120;
        rectTransform.anchoredPosition = anchoredPosition;
    }

    void Update() {
        Vector3 anchoredPosition = rectTransform.anchoredPosition;
        float targetY = mazeScript == null ? 120 : -20;
        anchoredPosition.y = Util.Damp(anchoredPosition.y, targetY, .0001f, Time.deltaTime);
        rectTransform.anchoredPosition = anchoredPosition;
        if (mazeScript != null && mazeScript.maze != null) {
                tmpTarget.text = mazeScript.maze.par.ToString();
            tmpScore.text = mazeScript.maze.totalTiles.ToString();
        }
    }
}

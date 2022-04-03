using Assets.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveButtonsScript : MonoBehaviour
{
    public MazeScript mazeScript;
    public RectTransform undoTransform;
    public Image imageCheck;

    Color checkColor;

    void Start() {
        Vector2 anchoredPosition = undoTransform.anchoredPosition;
        anchoredPosition.y = -120;
        undoTransform.anchoredPosition = anchoredPosition;
        checkColor = imageCheck.color;
    }

    void Update() {
        Vector2 anchoredPosition = undoTransform.anchoredPosition;
        float targetY = mazeScript.undo == null ? -120 : 0;
        anchoredPosition.y = Util.Damp(anchoredPosition.y, targetY, .0001f, Time.deltaTime);
        undoTransform.anchoredPosition = anchoredPosition;

        if (mazeScript.undo != null && mazeScript.maze.gel.path.Count == 0) {
            imageCheck.color = new Color(.75f, .75f, .75f);
        } else {
            imageCheck.color = checkColor;
        }
    }

    public void ConfirmMove() {
        if (mazeScript.maze.gel.path.Count == 0) {
            return;
        }
        mazeScript.ConfirmMove();
    }
    public void UndoMove() {
        mazeScript.UndoMove();
    }
}

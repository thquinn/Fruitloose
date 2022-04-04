using Assets.Code;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoveButtonsScript : MonoBehaviour
{
    public MazeScript mazeScript;
    public RectTransform rectTransform, undoTransform;
    public Image imageCheck;
    public TextMeshProUGUI tmpWarning;
    public CanvasGroup canvasGroupWarning;

    Color checkColor;

    void Start() {
        Vector2 anchoredPosition = rectTransform.anchoredPosition;
        anchoredPosition.y = -120;
        rectTransform.anchoredPosition = anchoredPosition;
        anchoredPosition = undoTransform.anchoredPosition;
        anchoredPosition.y = -120;
        undoTransform.anchoredPosition = anchoredPosition;
        checkColor = imageCheck.color;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            ConfirmMove();
        }

        // Overall transform.
        Vector2 anchoredPosition = rectTransform.anchoredPosition;
        float targetY = mazeScript == null ? -120 : 20;
        anchoredPosition.y = Util.Damp(anchoredPosition.y, targetY, .0001f, Time.deltaTime);
        rectTransform.anchoredPosition = anchoredPosition;
        if (mazeScript == null) {
            return;
        }
        // Undo transform.
        anchoredPosition = undoTransform.anchoredPosition;
        targetY = mazeScript.undo == null ? -120 : 0;
        anchoredPosition.y = Util.Damp(anchoredPosition.y, targetY, .0001f, Time.deltaTime);
        undoTransform.anchoredPosition = anchoredPosition;

        if (mazeScript.maze.gel.coor != mazeScript.maze.exit && mazeScript.maze.gel.path.Count == 0) {
            imageCheck.color = new Color(.75f, .75f, .75f);
            tmpWarning.text = "You need to leave a path open.";
            canvasGroupWarning.alpha = Util.Damp(canvasGroupWarning.alpha, 1, .0001f, Time.deltaTime);
        } else {
            imageCheck.color = checkColor;
            canvasGroupWarning.alpha = Util.Damp(canvasGroupWarning.alpha, 0, .0001f, Time.deltaTime);
        }
    }

    public void ConfirmMove() {
        if (mazeScript == null || mazeScript.maze == null || mazeScript.maze.gel.path.Count == 0) {
            return;
        }
        mazeScript.ConfirmMove();
    }
    public void UndoMove() {
        mazeScript.UndoMove();
    }
}

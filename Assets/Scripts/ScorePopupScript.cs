using Assets.Code;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScorePopupScript : MonoBehaviour
{
    public RectTransform rectTransform;
    public TextMeshProUGUI tmpNumbers, tmpLabel, tmpLevel;

    bool dismissed;

    void Start() {
        rectTransform.anchoredPosition = new Vector2(0, -400);
    }

    void Update()
    {
        Vector2 anchoredPosition = rectTransform.anchoredPosition;
        float targetY = dismissed ? 400 : 0;
        anchoredPosition.y = Util.Damp(anchoredPosition.y, targetY, .0001f, Time.deltaTime);
        if (anchoredPosition.y > 399) {
            Destroy(gameObject);
            return;
        }
        rectTransform.anchoredPosition = anchoredPosition;
    }

    public void SetText(int levelIndex, int levelMax, int par, int score) {
        tmpLevel.text = "Level " + levelIndex + " of " + levelMax;
        tmpNumbers.text = par + "<space=.03em><size=70%><voffset=.2em>►</voffset></size>" + score;
        int delta = score - par;
        string label = LABELS[0].Item2;
        for (int i = 0; i < LABELS.Length; i++) {
            if (delta >= LABELS[i].Item1) {
                label = LABELS[i].Item2;
            }
        }
        tmpLabel.text = label;
    }
    public void Dismiss() {
        dismissed = true;
    }

    public static (int, string)[] LABELS = new (int, string)[] {
        (int.MinValue, "er..."),
        (0, "par"),
        (1, "fine"),
        (2, "not bad!"),
        (3, "nice!"),
        (4, "good!"),
        (6, "great!"),
        (8, "excellent!"),
        (11, "wonderful!"),
        (14, "fantastic!"),
        (17, "amazing!"),
        (21, "incredible!"),
        (25, "impossible!"),
    };
}

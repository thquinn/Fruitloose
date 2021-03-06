using Assets.Code;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalPopupScript : MonoBehaviour
{
    public RectTransform rectTransform;
    public TextMeshProUGUI tmpScore;

    void Start() {
        rectTransform.anchoredPosition = new Vector2(0, -400);
    }
    void Update() {
        Vector2 anchoredPosition = rectTransform.anchoredPosition;
        anchoredPosition.y = Util.Damp(anchoredPosition.y, 0, .0001f, Time.deltaTime);
        rectTransform.anchoredPosition = anchoredPosition;

        if (Input.GetKeyDown(KeyCode.Return)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void SetText(int score) {
        tmpScore.text = score.ToString();
    }
}

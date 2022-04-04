using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour {
    public CanvasGroup canvasGroup;
    float time;

    void Update() {
        bool pressed = Input.GetKey(KeyCode.Escape) && Application.platform != RuntimePlatform.WebGLPlayer;
        canvasGroup.alpha = Mathf.Clamp01(canvasGroup.alpha + Time.deltaTime * (pressed ? 1 : -1));
        if (pressed) {
            time += Time.deltaTime;
            if (time > 2) {
                Application.Quit();
            }
        } else {
            time = 0;
        }
    }
}

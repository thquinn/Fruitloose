using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    CanvasGroup canvasGroup;
    float time;
    
    void Start() {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Update() {
        time += Time.deltaTime;
        if (time < 10) {
            canvasGroup.alpha = Mathf.Clamp01(canvasGroup.alpha + Time.deltaTime);
        } else {
            canvasGroup.alpha = Mathf.Clamp01(canvasGroup.alpha - Time.deltaTime);
            if (canvasGroup.alpha == 0) {
                Destroy(this);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScript : MonoBehaviour
{
    public CanvasGroup groupLogo, groupPrompt;
    float time;

    void Start()
    {
        groupLogo.alpha = 0;
        groupPrompt.alpha = 0;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > 1) {
            groupLogo.alpha += Time.deltaTime;
        }
        if (time > 2) {
            groupPrompt.alpha += Time.deltaTime;
        }
    }

    public void Dismiss() {
        Destroy(gameObject);
    }
}

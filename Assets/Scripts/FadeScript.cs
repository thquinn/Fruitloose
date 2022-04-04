using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScript : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        if (!Application.isEditor) {
            canvasGroup.alpha = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        canvasGroup.alpha -= .01f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoSingleton<FadeManager>
{
    public float fadeSpeed = 0.8f;
    public bool fadeInOnStart = true;
    public bool fadeOutOnExit = true;

    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (fadeInOnStart)
        {
            canvasGroup.alpha = 1f;
            StartCoroutine(FadeOut());
        }
    }

    public void Alpah1()
    {
        canvasGroup.alpha = 1f;
    }

    public IEnumerator FadeIn()
    {
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }
    }

    public IEnumerator FadeOut()
    {
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * fadeSpeed;
            yield return null;
        }
    }
}

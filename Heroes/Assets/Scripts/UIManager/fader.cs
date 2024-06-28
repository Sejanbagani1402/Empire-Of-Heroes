using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private IEnumerator FadeTo(float targetAlpha, float duration)
    {
        float startAlpha = image.color.a;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            Color newColor = image.color;
            newColor.a = alpha;
            image.color = newColor;
            yield return null;
        }

        // Ensure the final alpha is set
        Color finalColor = image.color;
        finalColor.a = targetAlpha;
        image.color = finalColor;
    }

    private IEnumerator FadeIn(float time)
    {
        yield return StartCoroutine(FadeTo(1f, time));
    }

    private IEnumerator FadeOut(float time)
    {
        yield return StartCoroutine(FadeTo(0f, time));
    }
}

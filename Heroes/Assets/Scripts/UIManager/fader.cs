using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public IEnumerator FadeAndLoadScene(string sceneName, float fadeDuration)
    {
        yield return FadeIn(fadeDuration);
        SceneManager.LoadScene(sceneName);
        yield return new WaitForSeconds(1f); // Wait a second for the scene to load
        yield return FadeOut(fadeDuration);
    }
}

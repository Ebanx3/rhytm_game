using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpawnAnimation : MonoBehaviour
{
    [SerializeField] private float duration = 0.4f;
    [SerializeField] private float initialScale = 0.5f;

    private Image image;
    private RectTransform rectTransform;

    void Awake()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void PlayAnimation()
    {
        StartCoroutine(AnimateSpawn());
    }

    private IEnumerator AnimateSpawn()
    {
        float elapsed = 0f;

        Vector3 finalScale = rectTransform.localScale;

        rectTransform.localScale = finalScale * initialScale;

        Color color = image.color;
        color.a = 0f;
        image.color = color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float progress = Mathf.Clamp01(elapsed / duration);

            rectTransform.localScale = Vector3.Lerp(
                finalScale * initialScale,
                finalScale,
                progress
            );

            color.a = progress;
            image.color = color;

            yield return null;
        }

        rectTransform.localScale = finalScale;

        color.a = 1f;
        image.color = color;
    }
}
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using PrimeTween;

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
        AnimateSpawn();
    }

    private void AnimateSpawn()
    {
        Vector3 finalScale = rectTransform.localScale;

        Sequence.Create()
            .Group(Tween.Scale(
               rectTransform,
               startValue: finalScale * initialScale,
               endValue: finalScale,
               duration: duration,
              ease: Ease.OutCubic
            ))
            .Group(Tween.Alpha(
                image,
                startValue: 0f,
                endValue: 1f,
                duration: duration,
                ease: Ease.OutCubic
            ));
    }
}
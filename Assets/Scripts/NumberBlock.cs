using PrimeTween;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;

public class NumberBlock : MonoBehaviour
{
    [SerializeField] private Sprite[] numberSprites;
    [SerializeField] private GameObject lockButton;

    [Range(0, 9)]
    private int value;

    private bool isLocked;

    [SerializeField] private float timeToDisappear;

    private Image blockImage;

    void Awake()
    {
        blockImage = GetComponent<Image>();
    }

    void Start()
    {
        StartCoroutine(DisappearAtTime());
    }

    public void SetValue(int v)
    {
        value = v;

        int spriteIndex = value == 0 ? 9 : value - 1;

        blockImage.sprite = numberSprites[spriteIndex];

        Debug.Log(
            $"CREACIÓN | Valor guardado: {value} | " +
            $"Índice del sprite: {spriteIndex} | " +
            $"Sprite: {blockImage.sprite.name}"
        );
    }

    public int GetValue()
    {
        return value;
    }

    public void SetIsLocked(bool il)
    {
        isLocked = il;

        if (lockButton != null)
            lockButton.SetActive(il);
    }

    public bool GetIsLocked()
    {
        return isLocked;
    }

    public void Unlock()
    {
        isLocked = false;

        if (lockButton != null)
            lockButton.SetActive(false);
    }

    public void DestroyGO()
    {
        Destroy(gameObject);
    }

    IEnumerator DisappearAtTime()
    {
        yield return new WaitForSecondsRealtime(timeToDisappear);

        Color startColor = gameObject.GetComponent<UnityEngine.UI.Image>().color;
        Color endColor = startColor;

        startColor.a = 1f;
        endColor.a = 0f;

        gameObject.GetComponent<UnityEngine.UI.Image>().color = startColor;

        Tween.Custom(
            startValue: startColor,
            endValue: endColor,
            duration: 0.4f,
            onValueChange: newColor => gameObject.GetComponent<UnityEngine.UI.Image>().color = newColor,
            ease: Ease.InOutCubic
        );
        Tween.Scale(
            gameObject.transform,
            endValue: Vector3.zero,
            duration: 0.4f,
            ease: Ease.InOutCubic
            ).OnComplete(() =>
            {
                DestroyGO();
            });
    }
}

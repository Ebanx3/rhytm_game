using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.InputSystem.Controls;

public class BlockGenerator : MonoBehaviour
{
    [SerializeField] private RectTransform canvas;
    [SerializeField] private GameObject numberPrefab;
    [SerializeField] private float blockSpawnSpeed;

    [SerializeField] private TextMeshProUGUI pressedKey;

    [SerializeField] float width;
    [SerializeField] float height;

    private NumberBlock[] blocks;

    void Start()
    {
        canvas = GetComponentInParent<RectTransform>();
        width = canvas.rect.width;
        height = canvas.rect.height;
        StartCoroutine(HandleSpawns());
    }

    void Update()
    {
        blocks = GetComponentsInChildren<NumberBlock>();

        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            foreach (KeyControl key in Keyboard.current.allKeys)
            {
                if (key.wasPressedThisFrame)
                {
                    pressedKey.text = key.name;
                }
            }
            
            int currentBlockValue = blocks[0].GetValue();

            for (int i = 0; i <= 9; i++)
            {
                if (Keyboard.current[Key.Digit0 + i].wasPressedThisFrame)
                {
                    if (currentBlockValue == i)
                    {
                        Debug.Log("¡Número correcto!");
                        blocks[0].DestroyGO();
                    }
                    else
                    {
                        Debug.Log("Número incorrecto");
                    }
                }
            }
        }
    }

    void SpawnBlock()
    {
        int randomValue = Random.Range(0, 10);
        bool isLocked = Random.Range(0, 2) == 0;

        GameObject obj = Instantiate(numberPrefab, canvas);
        NumberBlock nb = obj.GetComponent<NumberBlock>();
        nb.SetValue(randomValue);
        nb.SetIsLocked(isLocked);
        SpawnAnimation animation = obj.GetComponent<SpawnAnimation>();
        animation.PlayAnimation();

        RectTransform rect = obj.GetComponent<RectTransform>();

        float halfWidth = rect.rect.width / 2;
        float halfHeight = rect.rect.height / 2;

        float randomX = Random.Range(-width / 2 + halfWidth, width / 2 - halfWidth);
        float randomY = Random.Range(-height / 2 + halfHeight, height / 2 - halfHeight);

        rect.anchoredPosition = new Vector2(randomX, randomY);
    }

    IEnumerator HandleSpawns()
    {
        int numberAmount = 10;
        while (numberAmount > 0)
        {
            yield return new WaitForSecondsRealtime(1f);
            SpawnBlock();
            numberAmount--;
        }
    }
}

using System.Collections;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    [SerializeField] private RectTransform canvas;
    [SerializeField] private GameObject numberPrefab;
    [SerializeField] private float blockSpawnSpeed;

    float width;
    float height;

    public NumberBlock[] blocks;

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
    }

    void SpawnBlock()
    {
        int randomValue = Random.Range(0, 10);
        bool isLocked = Random.Range(0, 3) == 0;

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
        int numberAmount = 30;
        while (numberAmount > 0)
        {
            yield return new WaitForSecondsRealtime(blockSpawnSpeed);
            SpawnBlock();
            numberAmount--;
        }
    }
}

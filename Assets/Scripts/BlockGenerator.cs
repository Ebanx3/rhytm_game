using System.Collections;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    [SerializeField] private RectTransform canvas;
    [SerializeField] private GameObject[] numbersPrefabs;
    [SerializeField] private float blockSpawnSpeed;

    private float width;
    private float height;

    public NumberBlock[] blocks;

    void Start()
    {
        if (canvas == null)
            canvas = GetComponentInParent<RectTransform>();

        if (canvas == null)
        {
            Debug.LogError("BlockGenerator no encuentra el Canvas.");
            enabled = false;
            return;
        }

        width = canvas.rect.width;
        height = canvas.rect.height;

        StartCoroutine(HandleSpawns());
    }

    public NumberBlock[] GetBlocks()
    {
        if (canvas == null)
            return new NumberBlock[0];

        blocks = canvas.GetComponentsInChildren<NumberBlock>();

        return blocks;
    }

    void SpawnBlock()
    {
        // Genera números del 0 al 9.
        int randomValue = Random.Range(0, 10);

        bool isLocked = Random.Range(0, 3) == 0;

        GameObject obj = Instantiate(numbersPrefabs[randomValue], canvas);

        NumberBlock nb = obj.GetComponent<NumberBlock>();

        if (nb == null)
        {
            Debug.LogError("El prefab no tiene NumberBlock.");
            Destroy(obj);
            return;
        }

        nb.SetValue(randomValue);
        nb.SetIsLocked(isLocked);

        SpawnAnimation animation = obj.GetComponent<SpawnAnimation>();

        if (animation != null)
            animation.PlayAnimation();

        RectTransform rect = obj.GetComponent<RectTransform>();

        if (rect == null)
        {
            Debug.LogError("El prefab no tiene RectTransform.");
            return;
        }

        float halfWidth = rect.rect.width / 2;
        float halfHeight = rect.rect.height / 2;

        float randomX = Random.Range(
            -width / 2 + halfWidth,
            width / 2 - halfWidth
        );

        float randomY = Random.Range(
            -height / 2 + halfHeight,
            height / 2 - halfHeight
        );

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
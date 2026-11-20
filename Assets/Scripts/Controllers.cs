using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using static Unity.Collections.AllocatorManager;

public class Controllers : MonoBehaviour
{
    private readonly Key[] numberKeys =
    {
        Key.Digit1,
        Key.Digit2,
        Key.Digit3,
        Key.Digit4,
        Key.Digit5,
        Key.Digit6,
        Key.Digit7,
        Key.Digit8,
        Key.Digit9,
        Key.Digit0
    };

    [SerializeField] Points PointsScript;

    [SerializeField] private GameObject blockGenerator;
    [SerializeField] private TextMeshProUGUI pressedKey;

    private BlockGenerator generator;

    void Start()
    {
        if (blockGenerator == null)
        {
            Debug.LogError("No has asignado BlockGenerator.");
            enabled = false;
            return;
        }

        generator = blockGenerator.GetComponent<BlockGenerator>();

        if (generator == null)
        {
            Debug.LogError("El objeto asignado no tiene BlockGenerator.");
            enabled = false;
        }
    }

    void Update()
    {
        if (Keyboard.current == null)
            return;

        Keyboard keyboard = Keyboard.current;

        // Actualizar el texto de la tecla pulsada.
        if (keyboard.anyKey.wasPressedThisFrame)
        {
            foreach (KeyControl key in keyboard.allKeys)
            {
                if (key.wasPressedThisFrame && pressedKey != null)
                {
                    pressedKey.text = key.name;
                }
            }
        }

        int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };

        for (int i = 0; i < numberKeys.Length; i++)
        {
            if (!keyboard[numberKeys[i]].wasPressedThisFrame)
                continue;

            int pressedNumber = numbers[i];

            Debug.Log($"TECLA PULSADA: {pressedNumber}");

            NumberBlock[] blocks = generator.GetBlocks();

            foreach (NumberBlock block in blocks)
            {
                if (block == null)
                    continue;

                if (block.GetIsLocked())
                    continue;

                Debug.Log(
                    $"COMPARACIÓN | Valor guardado: {block.GetValue()} | " +
                    $"Tecla: {pressedNumber} | " +
                    $"Sprite: {block.GetComponent<UnityEngine.UI.Image>().sprite.name}"
                );

                if (block.GetValue() == pressedNumber)
                {
                    Debug.Log("¡¡COINCIDENCIA!!");




                    Color startColor = block.GetComponent<UnityEngine.UI.Image>().color;
                    Color endColor = startColor;

                    startColor.a = 1f;
                    endColor.a = 0f;

                    block.GetComponent<UnityEngine.UI.Image>().color = startColor;

                    Tween.Custom(
                        startValue: startColor,
                        endValue: endColor,
                        duration: 0.4f,
                        onValueChange: newColor => block.GetComponent<UnityEngine.UI.Image>().color = newColor,
                        ease: Ease.InOutCubic
                    );
                    Tween.Scale(
                        block.transform,
                        endValue: new Vector3(2, 2, 2),
                        duration: 0.4f,
                        ease: Ease.InOutCubic
                        ).OnComplete(() =>
                        {
                            block.DestroyGO();
                        });
                    

                    PointsScript.points += 1;
                }
            }
        }
    }
}
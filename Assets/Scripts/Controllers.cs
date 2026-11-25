using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

[RequireComponent(typeof(BlockGenerator))]
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

    [SerializeField] private BlockGenerator blockGenerator;
    [SerializeField] private TextMeshProUGUI pressedKey;


    void Start()
    {
        blockGenerator = GetComponent<BlockGenerator>();
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

            NumberBlock[] blocks = blockGenerator.GetBlocks();

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

                    block.DestroyGO();

                    PointsScript.points += 1;
                }
            }
        }
    }
}
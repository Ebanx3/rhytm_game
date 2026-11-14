using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;
using UnityEngine;
using TMPro;

public class Controllers : MonoBehaviour
{
    [SerializeField] private GameObject blockGenerator;
    private NumberBlock[] blocks;
    [SerializeField] private TextMeshProUGUI pressedKey;

    void Start()
    {
        blocks = blockGenerator.GetComponent<BlockGenerator>().blocks;
    }

    void Update()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            foreach (KeyControl key in Keyboard.current.allKeys)
            {
                if (key.wasPressedThisFrame)
                {
                    pressedKey.text = key.name;
                }
            }


            if (blocks.Length == 0 || blocks[0].GetIsLocked())
            {
                return;
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
}
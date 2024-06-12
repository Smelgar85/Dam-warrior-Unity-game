using UnityEngine;
using TMPro;

public class TabNavigation : MonoBehaviour
{
    public TMP_InputField[] inputFields; // Lista de campos de entrada

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SelectNextInputField();
        }
    }

    void SelectNextInputField()
    {
        // Encuentra el campo de entrada que actualmente tiene el foco
        for (int i = 0; i < inputFields.Length; i++)
        {
            if (inputFields[i].isFocused)
            {
                // Selecciona el siguiente campo de entrada
                int nextIndex = (i + 1) % inputFields.Length;
                inputFields[nextIndex].ActivateInputField();
                break;
            }
        }
    }
}

/**
 * TabNavigation.cs
 * Este script permite la navegación entre campos de entrada utilizando la tecla Tab.
 */

using UnityEngine;
using TMPro;

public class TabNavigation : MonoBehaviour
{
    public TMP_InputField[] inputFields; // Lista de campos de entrada.

    void Update()
    {
        // Detecta si se presiona la tecla Tab.
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SelectNextInputField();
        }
    }

    void SelectNextInputField()
    {
        // Encuentra el campo de entrada que actualmente tiene el foco y selecciona el siguiente.
        for (int i = 0; i < inputFields.Length; i++)
        {
            if (inputFields[i].isFocused)
            {
                int nextIndex = (i + 1) % inputFields.Length;
                inputFields[nextIndex].ActivateInputField();
                break;
            }
        }
    }
}

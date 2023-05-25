using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Answer : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private TMP_Text _text;

    private void Update()
    {
        if (_inputField.text == "5")
        {
            _text.text = "Ответ верный";
        }
    }
}

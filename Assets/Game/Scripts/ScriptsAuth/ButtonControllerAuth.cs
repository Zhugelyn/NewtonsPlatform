using System;
using TMPro;
using UnityEngine;

public class ButtonControllerAuth : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _inputField;
    private DataBase _dataBase;

    void Start()
    {
        _dataBase = GetComponent<DataBase>();
    }

    public void SetEmail()
    {
        try
        {
            _dataBase.SaveData(_inputField.text);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
}

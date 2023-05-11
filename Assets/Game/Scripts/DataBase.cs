using UnityEngine;
using Firebase.Database;
using System;
using Firebase.Auth;
using TMPro;
using System.Threading;
using UnityEngine.SceneManagement;

public class DataBase : MonoBehaviour
{
    DatabaseReference _databaseReference;
    FirebaseAuth _authentication;

    [SerializeField]
    private TMP_InputField _inputFieldEmail;
    [SerializeField]
    private TMP_InputField _inputFieldPassword;
    [SerializeField]
    private TMP_Text _authenticationStatus;

    private void Start()
    {
        _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        _authentication = FirebaseAuth.DefaultInstance;
    }

    public void SaveData(string email)
    {
        var user = new User("default", email);
        string jsonObject = JsonUtility.ToJson(user);
        _databaseReference.Child("User").SetValueAsync(jsonObject);
    }

    public void Login()
    {
        try
        {
            _authentication.SignInWithEmailAndPasswordAsync(_inputFieldEmail.text, _inputFieldPassword.text);
            _authenticationStatus.text = "Вход выполнен успешно";
        }
        catch
        {
            _authenticationStatus.text = "не удалось войти";
        }
        SceneManager.LoadScene("MainMenu");

    }

    public void RegisterUser()
    {
        try
        {
            _authentication.CreateUserWithEmailAndPasswordAsync(_inputFieldEmail.text, _inputFieldPassword.text);
            _authenticationStatus.text = "Регистрация прошла успешно"; 
        }
        catch
        {
            _authenticationStatus.text = "Не удалось зарегистрироваться";
        }
    }
}

[Serializable]
public class User
{
    public string _Nick;
    public string _Email;

    public User(string nick, string email)
    {
        _Nick = nick;
        _Email = email;
    }
}

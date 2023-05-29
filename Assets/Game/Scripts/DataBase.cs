using UnityEngine;
using Firebase.Database;
using System;
using Firebase.Auth;
using TMPro;

public class DataBase : MonoBehaviour
{
    DatabaseReference _databaseReference;
    FirebaseAuth _authentication;

    [SerializeField]
    private TMP_InputField _inputFieldEmail;
    [SerializeField]
    private TMP_InputField _inputFieldPassword;
    [SerializeField]
    public TMP_Text _authenticationStatus;

    private void Start()
    {
        _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        _authentication = FirebaseAuth.DefaultInstance;
    }

    public void Update()
    {
        if (_authenticationStatus.text == "Вход выполнен успешно")
        {
            var scenLoader = new SceneLoader();
            scenLoader.LoadMainMenuScene();
        }
    }

    public void SaveData(string email)
    {
        var user = new User("default", email);
        string jsonObject = JsonUtility.ToJson(user);
        _databaseReference.Child("User").SetValueAsync(jsonObject);
    }

    public void Login()
    {
         _authentication.SignInWithEmailAndPasswordAsync(_inputFieldEmail.text, _inputFieldPassword.text)
            .ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    _authenticationStatus.text = "SignInWithEmailAndPasswordAsync was canceled.";
                    return;
                }
                if (task.IsFaulted)
                {
                    _authenticationStatus.text = "Неверно введен пароль";
                    return;
                }
                if (task.IsCompleted)
                {
                    _authenticationStatus.text = "Вход выполнен успешно";
                }
            });
    }

    public void RegisterUser()
    {
        _authentication.CreateUserWithEmailAndPasswordAsync(_inputFieldEmail.text, _inputFieldPassword.text)
            .ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                    return;
                }

                if (task.IsFaulted)
                {
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    return;
                }

            });
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

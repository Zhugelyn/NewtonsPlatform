using UnityEngine;
using Firebase.Database;
using System;
using Firebase.Auth;
using TMPro;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using UnityEngine.Video;
using Firebase.Extensions;
using Newtonsoft.Json;

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
        if (_authenticationStatus != null)
            if (_authenticationStatus.text == "Вход выполнен успешно")
            {
                var scenLoader = new SceneLoader();
                scenLoader.LoadMainMenuScene();
            }
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
                   var user = _authentication.CurrentUser;

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
                if (task.IsCompleted)
                {
                    var user = new User
                    (
                        GetGuide(),
                        _inputFieldEmail.text,
                        "1",
                        DateTime.Now,
                        null
                    );
                    SaveEntity("Users", user.Email, JsonUtility.ToJson(user));
                }

            });
    }

    public void SaveEntity(string name, string secondName, string json)
    {
        _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        _databaseReference.Child(name).Child(secondName).SetRawJsonValueAsync(json);
    }

    public List<UserCourses> GetUserCourses()
    {
        var list = new List<UserCourses>();
        _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        var userCourses = _databaseReference.Child("UserCourses").Child(GetCurrentUserEmail()).GetValueAsync();
        DataSnapshot snapshot = userCourses.Result;
        var json = snapshot.GetRawJsonValue();
        JsonUtility.FromJsonOverwrite(json, list);

        return list;
    }

    public string GetCurrentUserEmail()
    {
        _authentication = FirebaseAuth.DefaultInstance;
        return _authentication.CurrentUser.Email;
    }

    public string GetGuide() => Guid.NewGuid().ToString();
}

[Serializable]
public class User
{
    public string Guid;
    public string Email;
    public string RoleId;
    public DateTime FirstLogin;
    public DateTime? LastLogin;
    public User(string guid, string email, string roleId, DateTime firstLogin, DateTime? lastLogin)
    {
        Guid = guid;
        Email = email;
        RoleId = roleId;
        FirstLogin = firstLogin;
        LastLogin = lastLogin;
    }
}

using UnityEngine;
using Firebase.Database;
using System;
using Firebase.Auth;
using TMPro;
using System.Collections.Generic;


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
        var userCourses = _databaseReference.Child("UserCourses")
            .EqualTo(GetCurrentUserEmail())
            .GetValueAsync();
        DataSnapshot snapshot = userCourses.Result;
        var json = snapshot.GetRawJsonValue();
        var items = JsonHelper.FromJson<UserCourses>(json);

        foreach (var item in items)
            list.Add(item);


        return list;
    }

    public List<Course> GetCourses(string courseGuid)
    {
        var listUserCourses = new List<Course>();
        _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        var courses = _databaseReference.Child("Courses")
            .EqualTo(courseGuid)
            .GetValueAsync();
        DataSnapshot snapshot = courses.Result;
        var json = snapshot.GetRawJsonValue();
        var items = JsonHelper.FromJson<Course>(json);
        foreach (var item in items)
            listUserCourses.Add(item);

        return listUserCourses;
    }

    public List<Theme> GetThemesByCourseGuid(string courseGuid)
    {
        var listTheme = new List<Theme>();
        _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        var courses = _databaseReference.Child("Courses")
            .Child(GetCurrentUserEmail())
            .OrderByChild("guid")
            .StartAt("courseId")
            .EqualTo(courseGuid)
            .GetValueAsync();
        DataSnapshot snapshot = courses.Result;
        var json = snapshot.GetRawJsonValue();
        var items = JsonHelper.FromJson<Theme>(json);
        foreach (var item in items)
            listTheme.Add(item);

        return listTheme;
    }

    public List<Chapter> GetChapterByThemeGuid(string themeGuid)
    {
        var listTheme = new List<Chapter>();
        _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        var courses = _databaseReference.Child("Courses")
            .EqualTo(themeGuid)
            .GetValueAsync();
        DataSnapshot snapshot = courses.Result;
        var json = snapshot.GetRawJsonValue();
        var items = JsonHelper.FromJson<Chapter>(json);
        foreach (var item in items)
            listTheme.Add(item);

        return listTheme;
    }

    public List<Task> GetTaskByChapterGuid(string chapterGuid)
    {
        var listTheme = new List<Task>();
        _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        var courses = _databaseReference.Child("Courses")
            .EqualTo(chapterGuid)
            .GetValueAsync();
        DataSnapshot snapshot = courses.Result;
        var json = snapshot.GetRawJsonValue();
        var items = JsonHelper.FromJson<Task>(json);
        foreach (var item in items)
            listTheme.Add(item);

        return listTheme;
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

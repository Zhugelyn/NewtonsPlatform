using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class Course : MonoBehaviour
{
    [SerializeField] private TMP_InputField _courseName;
    [SerializeField] private TMP_InputField _themeName;
    [SerializeField] private TMP_InputField _chapterName;
    [SerializeField] private TMP_InputField _taskText;
    [SerializeField] private TMP_InputField _taskAnswer;
    private bool _stateCourse = true;

    private List<Task> tasks = new List<Task>();
    public void createCourse()
    {
        var status = string.Empty;
        if (tasks.Any())
            status = "добавьте хот€бы одну заадчу";

        var theme = CreateSimpleObject(_themeName.text);
        var chapter = CreateSimpleObject(_chapterName.text);
    }

    public void ChageStateCourse()
    {
        _stateCourse = true? false: true;
    }

    public void AddTask()
    {
        tasks.Add(new Task(GetGuide(), _taskText.text, _taskAnswer.text));
    }

    public SimpleObject CreateSimpleObject(string name) => new SimpleObject(GetGuide(), name);
 

    public string GetGuide() => Guid.NewGuid().ToString();


}




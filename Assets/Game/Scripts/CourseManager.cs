using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class CourseManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField _courseName;
    [SerializeField] private TMP_InputField _themeName;
    [SerializeField] private TMP_InputField _chapterName;
    [SerializeField] private TMP_InputField _taskText;
    [SerializeField] private TMP_InputField _taskAnswer;
    [SerializeField] private GameObject _creatTaskWindow;
    [SerializeField] private TMP_Text _statusCreatedCourse;
    [SerializeField] private TMP_InputField _joinCourse;
    [SerializeField] private TMP_Text _statusJoinCourse;
    [SerializeField] private TMP_Text _countTask;

    private int _numberTask = 0;
    private DataBase _db = new DataBase();
    private Course _course;
    private Theme _theme;
    private Chapter _chapter;
    private List<StructTask> tasksStructList = new List<StructTask>();
    private string _stateCourse = "true";

    public void CreateCourse()
    {
        try
        {
            _course = CreateCourse(_courseName.text);
            _theme = CreateTheme(_themeName.text);
            _chapter = CreateChapter(_chapterName.text);

            SaveCourse("Courses", _course);
            SaveTheme("Themes", _theme);
            SaveChapter("Chapters", _chapter);
            SaveTasks("Tasks", CreateTask());

            _statusCreatedCourse.text = "Курс успешно создан";
        }
        catch (Exception ex)
        {
            _statusCreatedCourse.text = ex.Message;
        }
    }


    public void ChageStateCourse()
    {
        _stateCourse = _stateCourse == "true" ? "false" : "true";
    }

    public Course CreateCourse(string name) => new Course(GetGuide(), name, _db.GetCurrentUserEmail(), _stateCourse);

    public Theme CreateTheme(string name) => new Theme(GetGuide(), name, _course.Guid);

    public Chapter CreateChapter(string name) => new Chapter(GetGuide(), name, _theme.Guid);

    public void AddStructTask()
    {
        tasksStructList.Add(new StructTask(GetGuide(), _taskText.text, _taskAnswer.text));
        _creatTaskWindow.SetActive(false);
        _numberTask++;
        _countTask.text = _numberTask.ToString();
    }

    public List<Task> CreateTask()
    {
        var tasksList = new List<Task>();
        foreach (var task in tasksStructList)
            tasksList.Add(new Task(task.guid, task.textTask, task.answerTask, _chapter.Guid));

        return tasksList;
    }
    public void SaveCourse(string name, Course course)
    {
        var json = JsonUtility.ToJson(course);
        _db.SaveEntity(name, course.Guid, json);
    }

    public void SaveTheme(string name, Theme theme)
    {
        var json = JsonUtility.ToJson(theme);
        _db.SaveEntity(name, theme.Guid, json);
    }
    public void SaveChapter(string name, Chapter chapter)
    {
        var json = JsonUtility.ToJson(chapter);
        _db.SaveEntity(name, chapter.Guid, json);
    }
    public void SaveTasks(string name, List<Task> tasks)
    {
        foreach (var task in tasks)
        {
            var json = JsonUtility.ToJson(task);
            _db.SaveEntity(name, task.Guid, json);
        }
    }

    public void JoinCourse()
    {
        try
        {
            var userCourse = new UserCourses(_db.GetCurrentUserEmail(), _joinCourse.text);
            var json = JsonUtility.ToJson(userCourse);
            _db.SaveEntity("UserCourses", userCourse.Email, json);
            _statusJoinCourse.text = "Курс добавлен";
        }
        catch (Exception ex)
        {
            _statusJoinCourse.text = ex.Message;
        }
    }

    public string GetGuide() => Guid.NewGuid().ToString();

}

public struct StructTask
{
    public string guid;
    public string textTask;
    public string answerTask;

    public StructTask(string guid, string textTask, string answerTask)
    {
        this.guid = guid;
        this.textTask = textTask;
        this.answerTask = answerTask;
    }
}



using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Practice : MonoBehaviour
{
    [SerializeField] TMP_Text _textTask;
    [SerializeField] TMP_InputField _inputFieldAnswer;
    [SerializeField] TMP_Text _statusAnswer;
    [SerializeField] GameObject _uiEndPractice;

    DataBase _db;
    private List<Task> _listTasks;
    private int _numberPractice = 0;
    private string _correctAnswer;

    void Start()
    {
        _db = GetComponent<DataBase>();
        var courseGuid = GetComponent<CourseManager>().GetCourseGuid();
        var themeGuid = _db.GetThemesByCourseGuid(courseGuid).First().Guid;
        var chapterGuid = _db.GetChapterByThemeGuid(themeGuid).First().Guid;
        _listTasks = _db.GetTaskByChapterGuid(chapterGuid);
        _textTask.text = _listTasks.First().TextTask;
        _correctAnswer = _listTasks.First().AnswerTask;
    }

    public void UpdateTask()
    {
        if (_numberPractice >= _listTasks.Count())
        {
            _uiEndPractice.SetActive(true);
            return;
        }

        _textTask.text = _listTasks[_numberPractice].TextTask;
        _correctAnswer = _listTasks[_numberPractice].AnswerTask;
        _statusAnswer.text = string.Empty;
        _inputFieldAnswer.text = string.Empty;
    }

    public void NextTask()
    {
        if (_numberPractice >= _listTasks.Count() - 1)
        {
            _numberPractice = 0;
        }

        _textTask.text = _listTasks[_numberPractice].TextTask;
        _correctAnswer = _listTasks[_numberPractice].AnswerTask;
        _statusAnswer.text = string.Empty;
        _inputFieldAnswer.text = string.Empty;
    }

    public void PreviousTask()
    {
        if (_numberPractice == 0)
        {
            _numberPractice = _listTasks.Count() - 1;
        }

        _textTask.text = _listTasks[_numberPractice].TextTask;
        _correctAnswer = _listTasks[_numberPractice].AnswerTask;
        _statusAnswer.text = string.Empty;
        _inputFieldAnswer.text = string.Empty;
    }

    public void CheckAnswer()
    {
        if (_inputFieldAnswer != null)
        {
            if (_inputFieldAnswer.text == _listTasks[_numberPractice].AnswerTask)
            {
                _statusAnswer.text = "Ответ верный";
                _numberPractice++;
                UpdateTask();
            }
            else
                _statusAnswer.text = "Неверный ответ";
        }
    }
}

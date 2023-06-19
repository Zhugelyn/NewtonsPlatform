using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Practice : MonoBehaviour
{
    [SerializeField] TMP_Text _textTask;
    [SerializeField] TMP_InputField _inputFieldAnswer;
    [SerializeField] TMP_Text _statusAnswer;
    [SerializeField] GameObject _uiEndPractice;

    private static string _courseGuid = string.Empty;
    private Task _task;
    private List<Task> _listTasks;
    private int _numberPractice = 0;
    private string _correctAnswer = string.Empty;
    private List<UserCourses> _userCoursesList;

    void Start()
    {
        _listTasks = new List<Task>();
        _listTasks.Add(new Task(string.Empty, "Моторная лодка проходит по реке расстояние между двумя пунктами (в обе стороны) за 14 часов. чему равно это расстояние, если скорость лодки в стоячей воде 35 км/ ч, а скорость течения реки – 5 км/ч?", "240", string.Empty));
        _listTasks.Add(new Task(string.Empty, "Масса пустой пол-литровой бутылки равна 400 г. каков ее наружный объем?", "0,66", string.Empty));

        _textTask.text = _listTasks[_numberPractice].TextTask;
        _correctAnswer = _listTasks[_numberPractice].AnswerTask;
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

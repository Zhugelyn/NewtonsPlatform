using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private Task _task;
    private List<Task> _resourceTask;
    private int _numberPractice = 0;
    private string _correctAnswer = string.Empty;

    void Start()
    {
        _resourceTask = new List<Task>();
        _resourceTask.Add(new Task("Тело движется прямолинейно под действием силы 16 Н. Зависимость пути от времени имеет вид S=10−5t+2t2, м. Найти массу тела.", "10"));

        _textTask.text = _resourceTask[_numberPractice].TextTask;
        _correctAnswer = _resourceTask[_numberPractice].AnswerTask;
    }

    public void UpdateTask()
    {
        if (_numberPractice >= _resourceTask.Count())
        {
            _uiEndPractice.SetActive(true);
            return;
        }

        _textTask.text = _resourceTask[_numberPractice].TextTask;
        _correctAnswer = _resourceTask[_numberPractice].AnswerTask;
        _statusAnswer.text = string.Empty;
        _inputFieldAnswer.text = string.Empty;
    }

    public void CheckAnswer()
    {
        if (_inputFieldAnswer != null)
        {
            if (_inputFieldAnswer.text == _resourceTask[_numberPractice].AnswerTask)
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

public class Task
{
    public string TextTask { get; set; }
    public string AnswerTask { get; set; }

    public Task(string textTask, string answerTask)
    {
        this.TextTask = textTask;
        this.AnswerTask = answerTask;
    }
}

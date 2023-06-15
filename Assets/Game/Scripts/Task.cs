using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    private string GlobalId { get; set; }
    public string TextTask { get; set; }
    public string AnswerTask { get; set; }

    public Task(string GlobalId, string textTask, string answerTask)
    {
        this.GlobalId = GlobalId;
        this.TextTask = textTask;
        this.AnswerTask = answerTask;
    }
}

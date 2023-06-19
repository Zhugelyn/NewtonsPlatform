using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Task : MonoBehaviour
{
    public string Guid;
    public string TextTask;
    public string AnswerTask;
    public string ChapterId;

    public Task(string GlobalId, string textTask, string answerTask, string chapterId)
    {
        Guid = GlobalId;
        TextTask = textTask;
        AnswerTask = answerTask;
        ChapterId = chapterId;
    }
}

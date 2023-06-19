using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

[Serializable]
public class Course : SimpleObject
{
    public string MentorId;
    public string Private;

    public Course(string guid, string name, string mentorId, string stateCourse) : base(guid, name)
    {
        MentorId = mentorId;
        Private = stateCourse;
    }
}




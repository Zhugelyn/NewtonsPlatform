using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Chapter : SimpleObject
{
    public string CourseId;

    public Chapter(string guid, string name, string courseId) : base(guid, name)
    {
        CourseId = courseId;
    }
}

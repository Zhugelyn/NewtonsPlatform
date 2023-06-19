using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Theme : SimpleObject
{
    public string CourseId;

    public Theme(string guid, string name, string courseId) : base(guid, name)
    {
        CourseId = courseId;
    }
}

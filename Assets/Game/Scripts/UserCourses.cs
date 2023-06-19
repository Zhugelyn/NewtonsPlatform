using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserCourses : MonoBehaviour
{
    public string Email;
    public string CourseId;

    public UserCourses(string email, string courseId)
    {
        Email = email;
        CourseId = courseId;
    }
}

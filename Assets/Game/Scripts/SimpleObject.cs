using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleObject : MonoBehaviour
{
    public string Guid;
    public string Name;

    public SimpleObject(string guid, string name)
    {
        this.Guid = guid;
        this.Name = name;
    }
}

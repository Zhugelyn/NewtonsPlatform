using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleObject : MonoBehaviour
{
    private string GlobalId { get; set; }
    public string Name { get; set; }

    public SimpleObject(string GlobalId, string name)
    {
        this.GlobalId = GlobalId;
        this.Name = name;
    }
}

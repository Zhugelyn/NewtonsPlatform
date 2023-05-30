using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

public class Line : MonoBehaviour
{
    [SerializeField] private GameObject _object1;
    [SerializeField] private GameObject _object2;
    [SerializeField] private TMP_Text _range;

    private List<Vector3> GetObjectPosition()
    {
        var listPosition = new List<Vector3>();
        listPosition.Add(_object1.transform.position);
        listPosition.Add(_object2.transform.position);

        return listPosition;
    }

    public string GetRangeObjects()
    {
        var list = GetObjectPosition();
        var x = Mathf.Pow(list[0].x - list[1].x, 2);
        var y = Mathf.Pow(list[0].y - list[1].y, 2);
        var segmentLength = Mathf.Sqrt(x + y);
        return segmentLength.ToString("F2");
    }

    public void ShowRange()
    {
        _range.text = GetRangeObjects() + "ì.";
    }
}

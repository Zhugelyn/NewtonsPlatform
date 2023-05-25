using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    [SerializeField] private GameObject _object2;
    public void ImageObj()
    {
        if (_object.gameObject.active)
        {
            _object.gameObject.SetActive(false);
        }
        else
        {
            _object.gameObject.SetActive(true);
        }
    }

    public void ImageObj2()
    {
        if (_object2.gameObject.active)
        {
            _object2.gameObject.SetActive(false);
        }
        else
        {
            _object2.gameObject.SetActive(true);
        }
    }
}

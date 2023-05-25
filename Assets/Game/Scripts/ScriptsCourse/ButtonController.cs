using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private GameObject _object;
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
}

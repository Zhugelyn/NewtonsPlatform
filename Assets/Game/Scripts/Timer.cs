using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float _timeStart;
    [SerializeField] private TMP_Text _timerText;
    private bool _timerRunning = false;

    void Update()
    {
        if (_timerRunning)
        {
            _timeStart += Time.deltaTime;
            _timerText.text = _timeStart.ToString("F2");
        }
    }

    public void StartTimer()
    {
        _timerRunning = true;
    }

    public void StopTimer()
    {
        _timerRunning = false;
    }

    public void RestartTimer()
    {
        _timerRunning = false;
        _timeStart = 0.00f;
        _timerText.text = "0.00";
    }
}

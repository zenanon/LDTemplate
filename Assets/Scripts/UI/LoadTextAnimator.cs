using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadTextAnimator : MonoBehaviour
{
    public TextMeshProUGUI targetText;

    public string baseText;

    public int numDots = 3;

    public float timePerDot = .75f;

    public bool useUnscaledTime = true;

    public string dotText = ".";
    
    private int _currentDots;
    private float _currentTime;

    private void Update()
    {
        _currentTime += useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
        if (_currentTime >= timePerDot)
        {
            _currentDots = (_currentDots + 1) % (numDots + 1);
            _currentTime -= timePerDot;

            string text = baseText;
            for (int i = 0; i < _currentDots; i++)
            {
                text += dotText;
            }

            targetText.text = text;
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public UIScreen[] screens;

    public UIScreen CurrentScreen => _currentScreen;

    private readonly Dictionary<Type, UIScreen> _screenTypeMap = new Dictionary<Type, UIScreen>();
    private readonly List<UIScreen> _stack = new List<UIScreen>();
    private UIScreen _currentScreen;

    private void Awake()
    {
        if (Instance != null)
        {
            throw new Exception("Creating second instance of singleton UIManager");
        }

        Instance = this;
        foreach (UIScreen screen in screens)
        {
            if (_screenTypeMap.ContainsKey(screen.GetType()))
            {
                throw new Exception("Cannot have multiple screens for type " + screen.GetType().Name);
            }
            screen.gameObject.SetActive(false);
            _screenTypeMap[screen.GetType()] = screen;
        }
    }
    
    private void Update()
    {
        if (_currentScreen != null && !_currentScreen.HandleInput())
        {
            // Request any non-UI stuff to handle input.
        }
    }

    public void ShowScreen<T>(UIScreenParams<T> screenParams = null) where T : UIScreen
    {
        // Check if we have this kind of screen available.
        if (!_screenTypeMap.ContainsKey(typeof(T)))
        {
            throw new Exception("No screen defined for type " + typeof(T).Name);
        }

        UIScreen screenToShow = _screenTypeMap[typeof(T)];
        
        // Initialize the screen.
        screenToShow.Initialize(screenParams);

        if (screenToShow == _currentScreen)
        {
            return;
        }

        // Hide the current screen.
        if (_currentScreen != null)
        {
            _currentScreen.Hide();
        }
        
        _stack.Add(_currentScreen);
        
        // Show the new screen.
        _currentScreen = screenToShow;
        screenToShow.Show();
    }

    public void Back()
    {
        if (_currentScreen != null)
        {
            _currentScreen.Hide();
        }

        if (_stack.Count > 0)
        {
            _currentScreen = _stack[_stack.Count - 1];
            _currentScreen.Show();
            _stack.RemoveAt(_stack.Count - 1);
        }
        else
        {
            _currentScreen = null;
        }
    }
}

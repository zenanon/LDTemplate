using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIScreen : MonoBehaviour
{
    public virtual void Initialize<T>(UIScreenParams<T> screenParams) where T : UIScreen
    {
        // Do nothing.
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

    public virtual bool IsOverlay()
    {
        return false;
    }

    // Return true if the input has been handled.
    public virtual bool HandleInput()
    {
        if (IsOverlay() && Input.GetButtonDown("Cancel"))
        {
            UIManager.Instance.Back();
            return true;
        }
        return false;
    }
}

public interface UIScreenParams<T> where T : UIScreen
{
        
}
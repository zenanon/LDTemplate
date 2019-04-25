using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameScreen : UIScreen
{
    public override bool HandleInput()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            GameStateManager.Instance.PauseGame();
            return true;
        }
        return base.HandleInput();
    }
}

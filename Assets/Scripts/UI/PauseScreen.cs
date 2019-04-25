using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : UIScreen
{
    public override bool IsOverlay() => true;

    public void Resume()
    {
        GameStateManager.Instance.ResumeGame();
    }

    public void Quit()
    {
        GameStateManager.Instance.QuitGame();
    }
}

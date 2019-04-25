using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScreen : UIScreen
{
    public override bool IsOverlay() => true;

    public void PlayGame()
    {
        GameStateManager.Instance.StartGame();
    }

    public void Back()
    {
        UIManager.Instance.Back();
    }
}

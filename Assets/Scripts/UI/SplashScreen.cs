using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : UIScreen
{
    public float duration = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowTitleScreenAfterTime());
    }

    private IEnumerator ShowTitleScreenAfterTime()
    {
        yield return new WaitForSecondsRealtime(duration);
        UIManager.Instance.ShowScreen<TitleScreen>();
    }
}

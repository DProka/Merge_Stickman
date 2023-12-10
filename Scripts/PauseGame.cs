using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public bool focus;

    private void OnApplicationFocus(bool _focus)
    {
        focus = _focus;

        if (_focus)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
    }

}

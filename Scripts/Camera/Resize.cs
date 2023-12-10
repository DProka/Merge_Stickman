using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;
        float dif = Math.Min(((float)screenWidth / screenHeight)*2,1);
        Vector3 localScale = gameObject.transform.localScale;
        gameObject.transform.localScale = localScale / dif;
    }
}

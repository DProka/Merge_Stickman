using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Camera camera = Camera.main;
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;
        float dif = Math.Min(((float)screenWidth / screenHeight)*2,1);
        camera.orthographicSize /= dif;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

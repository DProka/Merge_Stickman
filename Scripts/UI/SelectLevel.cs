using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevel : MonoBehaviour
{
    public Canvas mainMenu;

    public void Back()
    {
        gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }
    
}

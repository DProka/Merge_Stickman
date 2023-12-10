using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Canvas selectMenu;
    public Canvas optionsMenu;
    public GameObject gameMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Play()
    {
        gameObject.SetActive(false);
        gameMenu.SetActive(true);
    }

    public void SelectLevel()
    {
        gameObject.SetActive(false);
        selectMenu.gameObject.SetActive(true);
    }

    public void Options()
    {
        gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}

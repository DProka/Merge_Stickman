using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnits : MonoBehaviour
{
    public GameObject[] unitArray;
    public int[] isMeleeArray;
    public int[] unitLvlArray;

    private const string saveKey = "mainUnits";

    void Start()
    {
        Load();
    }

    public void UpdatePlayerUnits()
    {
        Slot[] array = GameController.gameController.playerArena._slots;
        //GameObject[] uni = new GameObject[15];
        int[] isMeleeU = new int[15];
        int[] level = new int[15];

        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].slimeScr != null)
            {
                bool isMelee = array[i].slimeScr._isMelee;
                int lvl = array[i].slimeScr._level;
                isMeleeU[i] = array[i].slimeScr._isMelee ? 1 : 2 ;
                level[i] = array[i].slimeScr._level;
                //uni[i] = isMelee ? SlimeBase.slimeBase.playerMeelePrefab[lvl - 1] : SlimeBase.slimeBase.playerRangePrefab[lvl - 1];
            }
            else
            {
                //uni[i] = null;
                isMeleeU[i] = 0;
                level[i] = 0;
            }

        }
        
        //unitArray = uni;
        isMeleeArray = isMeleeU;
        unitLvlArray = level;
        Save();
    }

    //SaveLoad__________________________________________________________

    public void Load()
    {
        var data = SaveManager.Load<SaveData.PlayerUnits>(saveKey);

        //unitArray = data._playerUnits;
        isMeleeArray = data._isMeleeArray;
        unitLvlArray = data._unitLvlArray;
    }

    public void Save()
    {
        SaveManager.Save(saveKey, GetSaveSnapshot());
    }

    private SaveData.PlayerUnits GetSaveSnapshot()
    {
        SaveData.PlayerUnits data = new SaveData.PlayerUnits()
        {
            //_playerUnits = unitArray,
            _isMeleeArray = isMeleeArray,
            _unitLvlArray = unitLvlArray,
        };

        return data;
    }

    public void Reset()
    {
        unitArray = new GameObject[15];
        isMeleeArray = new int[15];
        unitLvlArray = new int[15];
    }
}

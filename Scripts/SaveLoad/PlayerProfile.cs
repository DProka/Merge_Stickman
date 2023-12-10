
using UnityEngine;

public class PlayerProfile : MonoBehaviour
{
    public int level;
    public ulong money;
    public uint meleeCounter;
    public uint rangeCounter;
    public int mapSpriteNumber;

    public int meleeOpened;
    public int rangeOpened;

    private const string saveKey = "mainSave";

    void Start()
    {
        Load();
    }

    public void SetNextLevel()
    {
        level++;
        Save();
    }

    public ulong GetMoney()
    {
        return money;
    }

    public void AddMoney(ulong _money)
    {
        money += _money;

        Save();
    }

    public void SubtractMoney(ulong _price)
    {
        if (money >= _price)
            money -= _price;

        Save();
    }

    public void UnitAdded(bool isMelee)
    {
        if (isMelee)
            meleeCounter++;
        else
            rangeCounter++;

        Save();
    }

    public void UnitOpened(bool isMelee, int lvl)
    {
        int count = isMelee ? meleeOpened : rangeOpened;

        if (isMelee && lvl > count)
        {
            meleeOpened++;
            GameController.gameController.uiScript.ShowNewUnit(isMelee, lvl);
        }

        else if (!isMelee && lvl > count)
        {
            rangeOpened++;
            GameController.gameController.uiScript.ShowNewUnit(isMelee, lvl);
        }

        Save();
    }

    public void ChangeMapBack()
    {
        mapSpriteNumber = GameController.gameController.arenaSpriteNumber;
        Save();
    }

    public void Reset()
    {
        money = 500;
        level = 0;
        meleeCounter = 0;
        rangeCounter = 0;
        meleeOpened = 0;
        rangeOpened = 0;
        mapSpriteNumber = 0;

        Save();
    }

    //SaveLoad__________________________________________________________

    public void Load()
    {
        var data = SaveManager.Load<SaveData.PlayerProfile>(saveKey);

        level = data._level;
        money = data._money;
        meleeCounter = data._meleeCounter;
        rangeCounter = data._rangeCounter;
        mapSpriteNumber = data._mapSpriteNumber;

        meleeOpened = data._meleeOpened;
        rangeOpened = data._rangeOpened;
        //Debug.Log("payerdata " + mapSpriteNumber);
    }

    public void Save()
    {
        SaveManager.Save(saveKey, GetSaveSnapshot());
    }

    private SaveData.PlayerProfile GetSaveSnapshot()
    {
        SaveData.PlayerProfile data = new SaveData.PlayerProfile()
        {
            _level = level,
            _money = money,
            _meleeCounter = meleeCounter,
            _rangeCounter = rangeCounter,
            _mapSpriteNumber = mapSpriteNumber,

            _meleeOpened = meleeOpened,
            _rangeOpened = rangeOpened,
        };

        return data;
    }
}




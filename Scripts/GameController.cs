using System;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Main Status")]

    public int gameLevel;
    public UIScript uiScript;
    public ulong coinsOnLvl;
    public PlayerProfile playerData;
    public PlayerUnits playerUnits;
    [HideInInspector] public bool playerUIActive = true;
    public SoundController soundController;
    public PauseGame pause;

    public bool levelIsLoading = true;

    [Header("Arena Controller")]

    public static GameController gameController;
    public SpriteRenderer arenaBack;
    public SpriteRenderer arenaGrid;
    public int arenaSpriteNumber;

    [HideInInspector] public bool arenaActive = false;

    public CreateArena playerArena;
    public CreateArena enemyArena;

    public List<Slime> enemyList;
    public List<Slime> playerList;
    public List<Projectile> projectList;
    
    private int numberOfAliveUnits;
    private int numberOfAliveEnemys;


    [Header("Enemy Generator")]
    
    public int unitsPerLvl;
    public float progress;

    public int[] slimesOnLvl = new int[10];

    public int enemyCount;
    public int lvlCount;

    [Header("Training LVL")]

    public bool trainigActive = false;
    public int stepCount;
    public GameObject step2arrow;

    private Vector3 choosedSlimePos;

    [Header("Mouse Position")]

    public Camera mainCamera;
    public GameObject choosedSlime;

    private Vector3Int mousePositionInt32;

    //Body______________________________________________________________________

    private void Awake()
    {
        gameController = this;
        playerData.Load();
        playerUnits.Load();
        gameLevel = playerData.level;
    }

    public void Update()
    {
        if (pause.focus)
        {
            if (levelIsLoading)
            {
                LoadLevel();
            }

            if (arenaActive)
            {
                if (!trainigActive)
                {
                    CheckArenaState();
                }
                else
                {
                    CheckTrainingArenaState();
                }
            }
            else
            {
                if (!trainigActive)
                {
                    MoveUnits();
                }
            }

            if (trainigActive)
            {
                CheckTrainingState();
                uiScript.UpdateTrainingUI();
            }
            else
            {
                uiScript.trainingUI.SetActive(false);
            }

            if (!playerUIActive && uiScript.newCardShown && Input.GetMouseButton(0))
            {
                uiScript.CloseUnitCard();
                uiScript.CloseMapCard();
            }
        }
    }

    public void FixedUpdate()
    {
        if (pause.focus)
        {
            UpdateSlimes();
            uiScript.FixUpdateUI();
        }
    }

    private void UpdateSlimes()
    {
        for(int i = 0; i < playerList.Count; i++)
        {
            playerList[i].UpdateUnit();
        }
        
        for(int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].UpdateUnit();
        }
        
        for(int i = 0; i < projectList.Count; i++)
        {
            projectList[i].UpdateProj();
        }
    }

    //Arena Controller______________________________________________________
    #region Arena controller
    public void LoadLevel()
    {
        if (gameLevel < 1)
        {
            CallTraining();
        }
        else
        {
            GenerateEnemies();
            LoadPlayerUnits();
            CheckArenaSprite(true);
        }

        uiScript.UpdateUI();
        levelIsLoading = false;
    }

    public void ArenaStart()
    {
        if (enemyList.Count != 0 || playerList.Count != 0)
        {
            arenaActive = true;
        }
    }

    public void ResetArena()
    {
        LoadPlayerUnits();
        ResetPlayerUnits();
        ResetEnemyList();
        GenerateEnemies();
        CheckArenaSprite(false);
    }

    public void CheckArenaState()
    {
        int numOfAlivePlayerSlimes = playerList.Count;
        int numOfAliveEnemySlimes = enemyList.Count;

        foreach (Slime playerSlime in playerList)
        {
            numOfAlivePlayerSlimes = playerSlime.isDead ? --numOfAlivePlayerSlimes : numOfAlivePlayerSlimes;
        }
        foreach (Slime enemySlime in enemyList)
        {
            numOfAliveEnemySlimes = enemySlime.isDead ? --numOfAliveEnemySlimes : numOfAliveEnemySlimes;
        }
        if (numOfAlivePlayerSlimes == 0)
        {
            Debug.Log("Enemy Won");
            LevelReward(false);
            arenaActive = false;
            soundController.PlayWin(false);
            uiScript.UpdateUI();
            if(!uiScript.wheelActive)
                uiScript.playerButt.SetActive(true);
        }

        if (numOfAliveEnemySlimes == 0)
        {
            Debug.Log("Player Won");
            gameLevel++;
            playerData.level = gameLevel;
            LevelReward(true);
            arenaActive = false;
            soundController.PlayWin(true);
            uiScript.UpdateUI();
            if(!uiScript.wheelActive)
                uiScript.playerButt.SetActive(true);
        }
    }

    private void LevelReward(bool playerWon)
    {
        if (playerWon)
        {
            coinsOnLvl = (ulong)(100 * Math.Pow(1.25, gameLevel));
            uiScript.StartWheel(playerWon);
        }
        else
        {
            coinsOnLvl = (ulong)(50 * Math.Pow(1.25, gameLevel));
            uiScript.StartWheel(playerWon);
        }
    }

    public void FillEnemyList(bool isPlayer, Slime slime)
    {
        if (!isPlayer)
            enemyList.Add(slime);
        else
        {
            playerList.Add(slime);
            playerUnits.UpdatePlayerUnits();
        }
            
    }

    public void RemoveFromList(Slime slime)
    {
        if (slime != null)
        {
            if (slime._isPlayerSlime)
            {
                playerList.Remove(slime);
                playerUnits.UpdatePlayerUnits();
            }
                
            else
                enemyList.Remove(slime);
        }
    }

    public void LoadPlayerUnits()
    {
        if(playerList.Count == 0)
        {
            //GameObject[] pArray = playerUnits.unitArray;
            int[] isMelee = playerUnits.isMeleeArray;
            int[] level = playerUnits.unitLvlArray;

            for (int i = 0; i < isMelee.Length; i++)
            {
                if (isMelee[i] != 0)
                {
                    bool melee = false;
                    if (isMelee[i] == 1)
                        melee = true;
                    else
                        melee = false;

                    playerArena._slots[i].SpawnSlime(true, melee, level[i]);
                }
            }
            
            //for (int i = 0; i < pArray.Length; i++)
            //{
            //    if (pArray[i] != null)
            //    {
            //        Slime slime = pArray[i].GetComponent<Slime>();
            //        playerArena._slots[i].SpawnSlime(true, slime._isMelee, slime._level);
            //    }
            //}
        }
    }

    public void CheckArenaSprite(bool isLoading)
    {
        if (isLoading)
        {
            arenaSpriteNumber = playerData.mapSpriteNumber;
            arenaBack.sprite = SlimeBase.slimeBase.arenaBack[arenaSpriteNumber];
            arenaGrid.sprite = SlimeBase.slimeBase.arenaGrid[arenaSpriteNumber];
        }
        else
        {
            int spriteID = gameLevel / 10;
            if (arenaSpriteNumber != spriteID)
            {
                arenaSpriteNumber = spriteID;

                if (spriteID < 10)
                {
                    arenaBack.sprite = SlimeBase.slimeBase.arenaBack[spriteID];
                    arenaGrid.sprite = SlimeBase.slimeBase.arenaGrid[spriteID];
                    uiScript.ShowNewMap(spriteID);
                }
                else
                {
                    arenaBack.sprite = SlimeBase.slimeBase.arenaBack[0];
                    arenaGrid.sprite = SlimeBase.slimeBase.arenaGrid[0];
                    uiScript.ShowNewMap(0);
                }

                playerData.ChangeMapBack();
            }
        }
    }
    #endregion
    //Enemy Generator________________________________________________________________

    public void GenerateSlimesOnFirstLvl()
    {
        uiScript.slimeSpawner.SpawnMeleeSlime(false);
        uiScript.slimeSpawner.SpawnRangeSlime(false);
    }

    public void GenerateEnemies()
    {
        if(gameLevel < 25)
        {
            GameObject[] array = SlimeBase.slimeBase.levelsArray[gameLevel];

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != null)
                {
                    Slime slime = array[i].GetComponent<Slime>();
                    SlimeBase.slimeBase.enemyArena[i].SpawnSlime(false, slime._isMelee, slime._level);
                }
            }
        }
        else
        {
            int totalEnemyLvl = 0;
            float progression = gameLevel / progress;
            progression *= 1.5f;
            totalEnemyLvl = (int)progression;

            int totalLvlMelee = totalEnemyLvl / 2;
            int totalLvlRange = totalEnemyLvl / 2;

            GetEnemyType(totalEnemyLvl);

            for (int i = 0; i < enemyCount; i++)
            {
                for (int j = 0; j < slimesOnLvl.Length; j++)
                {
                    if (slimesOnLvl[j] != 0)
                    {
                        SpawnEnemy(true, j);
                        SpawnEnemy(false, j);
                    }
                }
            }
        }

        //if (gameLevel <= 3)
        //{
        //    totalEnemyLvl = unitsPerLvl + (gameLevel - 1) * unitsPerLvl;
        //}
        //else if (gameLevel <= 10)
        //{
        //    totalEnemyLvl = (int)progression;
        //}
        //else
        //{
            
        //}

        //Debug.Log("enemyCount " + totalEnemyLvl);
    }

    private void GetEnemyType(int count)
    {
        slimesOnLvl = new int[10];

        int totalLvl = count;
        
        if(count >= 512)
        {
            slimesOnLvl[9] = (int)(count / 512);
            totalLvl -= slimesOnLvl[9] * 512;
        }
        if(totalLvl >= 256)
        {
            slimesOnLvl[8] = (int)(totalLvl / 256);
            totalLvl -= slimesOnLvl[8] * 256;
        }
        if(totalLvl >= 128)
        {
            slimesOnLvl[7] = (int)(totalLvl / 128);
            totalLvl -= slimesOnLvl[7] * 128;
        }
        if(totalLvl >= 64)
        {
            slimesOnLvl[6] = (int)(totalLvl / 64);
            totalLvl -= slimesOnLvl[6] * 64;
        }
        if(totalLvl >= 32)
        {
            slimesOnLvl[5] = (int)(totalLvl / 32);
            totalLvl -= slimesOnLvl[5] * 32;
        }
        if(totalLvl >= 16)
        {
            slimesOnLvl[4] = (int)(totalLvl / 16);
            totalLvl -= slimesOnLvl[4] * 16;
        }
        if(totalLvl >= 8)
        {
            slimesOnLvl[3] = (int)(totalLvl / 8);
            totalLvl -= slimesOnLvl[3] * 8;
        }
        if(totalLvl >= 4)
        {
            slimesOnLvl[2] = (int)(totalLvl / 4);
            totalLvl -= slimesOnLvl[2] * 4;
        }
        if(totalLvl >= 2)
        {
            slimesOnLvl[1] = (int)(totalLvl / 2);
            totalLvl -= slimesOnLvl[1] * 2;
        }

        SetEnemyCount();
        Debug.Log("numberOfEnemys " + totalLvl);
    }

    private void SetEnemyCount()
    {
        enemyCount = 0;

        for(int i = 0; i < slimesOnLvl.Length; i++)
        {
            enemyCount += slimesOnLvl[i];
        }
    }

    public void SpawnEnemy(bool isMelee, int lvl)
    {
        Slot[] slots = enemyArena._slots;

        if (isMelee)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].IsEmpty())
                {
                    slots[i].SpawnSlime(false, true, lvl);
                    Instantiate(SlimeBase.slimeBase.spawnAnimation, slots[i].position);
                    break;
                }
            }
        }
        else
        {
            for (int i = slots.Length - 1; i > 1; i--)
            {
                if (slots[i].IsEmpty())
                {
                    slots[i].SpawnSlime(false, false, lvl);
                    Instantiate(SlimeBase.slimeBase.spawnAnimation, slots[i].position);
                    break;
                }
            }
        }
    }

    public void ResetEnemyList()
    {
        foreach (Slot slot in enemyArena._slots)
        {
            slot.Clear();
        }
    }

    public void ResetPlayerUnits()
    {
        foreach (Slime slime in playerList)
        {
            slime.ResetSlimePos();
        }
    }

    public void ClearArena()
    {
        foreach (Slot slot in enemyArena._slots)
        {
            slot.Clear();
        }
        foreach (Slot slot in playerArena._slots)
        {
            slot.Clear();
        }
    }

    //Generate Training Level______________________________________________

    public void CallTraining()
    {
        stepCount = 0;
        trainigActive = true;
    }

    private void CheckTrainingState()
    {
        //uiScript.UpdateUI();
        uiScript.UpdateTrainingUI();

        if (stepCount == 0)
        {
            CallStep1();
        }

        if (stepCount == 3)
        {
            CallStep3();
        }

        if (stepCount == 5)
        {
            CallStep5();
        }
    }

    private void CallStep1()
    {
        GenerateTrainingLvl();
        stepCount = 1;
    }

    private void CallStep3()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetTrainingUnit();
        }
        if (Input.GetMouseButton(0))
        {
            MoveTrainingUnit();
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (choosedSlime != null)
            {
                Slime slime = choosedSlime.GetComponent<Slime>();
                slime.CheckUnitPos();
                Vector3Int slotpos = ConvertPositionToInt(playerArena._slots[6].transform.position.x, playerArena._slots[6].transform.position.y);
                Debug.Log(slotpos);
                if (choosedSlimePos == slotpos)
                {
                    Destroy(choosedSlime);
                    step2arrow.SetActive(false);
                    stepCount = 4;
                }
                choosedSlime = null;
            }
        }
    }

    private void CallStep5()
    {
        uiScript.UpdateTrainingUI();
        trainigActive = false;
        uiScript.reclame = true;
        gameLevel += 1;
        //levelIsLoading = true;
        playerUnits.UpdatePlayerUnits();
    }

    private void CheckTrainingArenaState()
    {
        int numOfAlivePlayerSlimes = playerList.Count;
        int numOfAliveEnemySlimes = enemyList.Count;

        foreach (Slime playerSlime in playerList)
        {
            numOfAlivePlayerSlimes = playerSlime.isDead ? --numOfAlivePlayerSlimes : numOfAlivePlayerSlimes;
        }
        foreach (Slime enemySlime in enemyList)
        {
            numOfAliveEnemySlimes = enemySlime.isDead ? --numOfAliveEnemySlimes : numOfAliveEnemySlimes;
        }

        if (numOfAlivePlayerSlimes == 0)
        {
            Debug.Log("Enemy Won");
            LevelReward(false);
            soundController.PlayWin(false);
            //ResetTrainingArena();
            arenaActive = false;
            stepCount = 2;

        }

        if (numOfAliveEnemySlimes == 0)
        {
            Debug.Log("Player Won");
            LevelReward(true);
            soundController.PlayWin(true);
            //ResetTrainingArena();
            stepCount = 5;
            arenaActive = false;
        }
    }

    public void ResetTrainingArena()
    {
        foreach (Slime slime in playerList)
        {
            slime.ResetSlimePos();
        }
        foreach (Slime slime in enemyList)
        {
            slime.ResetSlimePos();
        }
    }

    private void GenerateTrainingLvl()
    {
        enemyArena._slots[1].SpawnSlime(false, true, 1);
        enemyArena._slots[2].SpawnSlime(false, false, 1);
        playerArena._slots[6].SpawnSlime(true, true, 1);
        playerArena._slots[8].SpawnSlime(true, false, 1);

    }
    
    public void GetTrainingUnit()
    {
        choosedSlime = playerArena._slots[7]._slimeObj;
    }

    public void MoveTrainingUnit()
    {
        Vector3 mousePos = CheckMousePosition(false);
        float x1 = playerArena._slots[6].transform.position.x;
        float y1 = playerArena._slots[6].transform.position.y;
        float x2 = playerArena._slots[6].transform.position.x + 2;
        float y2 = playerArena._slots[6].transform.position.y + 1;

        if (choosedSlime != null)
        {
            Vector3Int slimepos = ConvertPositionToInt(choosedSlime.transform.position.x, choosedSlime.transform.position.y);
            choosedSlimePos = slimepos;

            if (mousePos.x > x1 && mousePos.y > y1 && mousePos.x < x2 && mousePos.y < y2)
                choosedSlime.transform.position = CheckMousePosition(false);
        }
    }

    public void RestartTraining()
    {
        gameLevel = 0;
        stepCount = 0;
        levelIsLoading = true;
        trainigActive = true;
        arenaSpriteNumber = 0;
        arenaBack.sprite = SlimeBase.slimeBase.arenaBack[arenaSpriteNumber];
        arenaGrid.sprite = SlimeBase.slimeBase.arenaGrid[arenaSpriteNumber];
        playerData.ChangeMapBack();
    }

    //Check Mouse Position_______________________________________________

    public Vector3 CheckMousePosition(bool int32)
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        mousePositionInt32 = ConvertPositionToInt(mousePosition.x, mousePosition.y);

        if (!int32)
            return mousePosition;
        else
            return mousePositionInt32;
    }

    private Vector3Int ConvertPositionToInt(float x, float y)
    {
        int x1 = (int)x;
        int y1 = (int)y;
        Vector3Int vector3Int = new Vector3Int(x1, y1);

        return vector3Int;
    }

    public void MoveUnits()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CheckMousePosition(false).x > 0.01 && CheckMousePosition(false).y > 0.01)
                FindPlayerUnit();
        }
        if (Input.GetMouseButton(0))
        {
            MovePlayerUnit();
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (choosedSlime != null)
            {
                Slime slime = choosedSlime.GetComponent<Slime>();
                slime.CheckUnitPos();
                choosedSlime = null;
            }
        }
    }

    public void FindPlayerUnit()
    {
        foreach (Slot slot in playerArena._slots)
        {
            if(slot._slimeObj != null && slot.transform.position == CheckMousePosition(true))
            {
                choosedSlime = slot._slimeObj;
            }
        }
    }

    public void MovePlayerUnit()
    {
        if(choosedSlime != null)
        {
            choosedSlime.transform.position = CheckMousePosition(false);
        }
    }
}

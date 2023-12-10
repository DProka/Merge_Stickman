using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UIScript : MonoBehaviour
{
    [Header("Main Text")]

    public GameObject playerUI;
    public GameObject playerButt;
    public TextMeshProUGUI gameLevel;
    public TextMeshProUGUI playerCoins;
    public MAXInit maxInit;
    public bool playerUIActive = true;

    [Header("Play Button")]

    public Button _buttonComponent;
    public Image _buttonImage;
    public TextMeshProUGUI _buttonText;

    [Header("Buy Buttons")]

    public SlimeSpawner slimeSpawner;
    public Button buyMeleeButt;
    public Button buyRangeButt;
    public TextMeshProUGUI textPriceMeele;
    public TextMeshProUGUI textPriceRange;
    public Image buyMeleeImg;
    public Image buyRangeImg;
    public Sprite buyForCash;
    public Sprite buyForRecl;
    public GameObject priceMelee;
    public GameObject priceRange;
    public bool buyMeleeSwitch;

    [Header("Debug Menu")]

    public GameObject debugMenu;
    public GameObject debugMenuButton;
    public GameObject slimeSpawnerMenu;

    public TextMeshProUGUI reclameText;
    public bool reclame = true;

    [Header("Cards Menu")]

    public GameObject cardsMenu;
    public SlimeBase slimeBase;
    public CardScript[] cardsArray;

    [Header("New Unit Card")]
    public bool newCardShown = false;
    public GameObject unitCard;
    public Image unitImage;
    public GameObject imageObj;
    public Image backImage;
    public Sprite backEmpty;
    public TextMeshProUGUI unitName;
    public TextMeshProUGUI unitHP;
    public TextMeshProUGUI unitAtk;
    
    [Header("New Map Card")]
    public bool newMapCardShown = false;
    public GameObject mapCard;
    public Image mapImage;
    public TextMeshProUGUI mapName;
    public Sprite[] mapSpriteArray;

    [Header("Casino Wheel")]

    public RewardManager rewardPile;
    public GameObject wheelObject;
    public Transform arrow;
    public float arrowSpeed;
    public TextMeshProUGUI earnedCoins;
    public TextMeshProUGUI prizeText;
    public TextMeshProUGUI victoryText;
    public TextMeshProUGUI lvlCompletedText;
    public bool wheelActive = true;

    private int arrowAngle;
    private int prize;

    [Header("Options Menu")]

    public GameObject optionsMenu;
    public Image soundButton;
    public Sprite soundOn;
    public Sprite soundOff;
    private bool isSound = true;

    [Header("Tarining Level")]

    public GameObject trainingUI;
    public GameObject trainingStartButt;
    public GameObject trainingBuyMeleeButt;
    public GameObject trainingBuyRangeButt;
    public GameObject trainingBack;

    //Main Code__________________________________________________

    void Start()
    {
        UpdateUI();
        rewardPile.Init();
    }

    public void FixUpdateUI()
    {
        if (wheelActive)
        {
            
            RotateWheel();
        }
    }

    public void UpdateUI()
    {
        if (!GameController.gameController.trainigActive)
            gameLevel.text = "Level " + GameController.gameController.gameLevel;
        else
            gameLevel.text = "Training";

        playerCoins.text = "" + CombineMoney(GameController.gameController.playerData.GetMoney());
        textPriceMeele.text = "" + CombineMoney(GetStickManPrice(true));
        CheckBuyingPoss(true);
        textPriceRange.text = "" + CombineMoney(GetStickManPrice(false));
        CheckBuyingPoss(false);
    }

    private string CombineMoney(ulong money)
    {
        float price = 0;
        string text = "";

        if (money / 1000 != 0)
        {
            price = (float)money / 1000;
            text = string.Format("{0: 0.0}", price) + "K";
            //Debug.Log(price);
        }
        else
        {
            price = money;
            text = "" + price;
        }

        if(money / 1000000 != 0)
        {
            price = (float)money / 1000000;
            text = string.Format("{0: 0.0}", price) + "M";
        }
        
        if(money / 1000000000 != 0)
        {
            price = (float)money / 1000000000;
            text = string.Format("{0: 0.0}", price) + "B";
        }
        
        if(money / 1000000000000 != 0)
        {
            price = (float)money / 1000000000000;
            text = string.Format("{0: 0.0}", price) + "T";
        }

        return text;
    }

    //Player Buttons Code_______________________________________________________

    public void Play()
    {
        if (playerUIActive)
        {
            GameController.gameController.ArenaStart();
            GameController.gameController.playerUnits.Save();
            playerButt.SetActive(false);
        }
    }

    public void BuyMeele()
    {
        if (playerUIActive && GameController.gameController.playerList.Count < 15)
        {
            ulong price = GetStickManPrice(true);

            if (price <= GameController.gameController.playerData.GetMoney())// && !GameController.gameController.arenaActive)
            {
                GameController.gameController.playerData.SubtractMoney(price);
                GameController.gameController.playerData.UnitAdded(true);
                slimeSpawner.SpawnMeleeSlime(true);
            }
            else
            {
                maxInit.ShowRewardedAd(true);
                buyMeleeSwitch = true;
            }

            UpdateUI();
        }
    }
    
    public void BuyRange()
    {
        if (playerUIActive && GameController.gameController.playerList.Count < 15)
        {
            ulong price = GetStickManPrice(false);

            if (price <= GameController.gameController.playerData.GetMoney()) //&& !GameController.gameController.arenaActive)
            {
                GameController.gameController.playerData.SubtractMoney(price);
                GameController.gameController.playerData.UnitAdded(false);
                slimeSpawner.SpawnRangeSlime(true);
            }
            else
            {
                maxInit.ShowRewardedAd(true);
                buyMeleeSwitch = false;
            }

            UpdateUI();
        }
    }

    public void CheckBuyingPoss(bool isMelee)
    {
        ulong price = GetStickManPrice(isMelee);

        if (isMelee)
        {
            if (GameController.gameController.playerData.GetMoney() < price)
            {
                buyMeleeImg.sprite = buyForRecl;
                priceMelee.SetActive(false);
            }

            else
            {
                buyMeleeImg.sprite = buyForCash;
                priceMelee.SetActive(true);
            }
        }
        else
        {
            if (GameController.gameController.playerData.GetMoney() < price)
            {
                buyRangeImg.sprite = buyForRecl;
                priceRange.SetActive(false);
            }

            else
            {
                buyRangeImg.sprite = buyForCash;
                priceRange.SetActive(true);
            }    
        }
    }

    private ulong GetStickManPrice(bool isMelee)
    {
        float counter = isMelee ? GameController.gameController.playerData.meleeCounter : GameController.gameController.playerData.rangeCounter;
        return (ulong)(100 * Math.Pow(1.175, counter));
    }

    //Debug Menu Code________________________________________________________________

    public void ToggleDebugMenu(bool on)
    {
        debugMenuButton.SetActive(!on);
        slimeSpawnerMenu.SetActive(on);
    }

    public void ResetPlayer()
    {
        ClearArena();
        GameController.gameController.playerData.Reset();
        GameController.gameController.playerUnits.Reset();
        UpdateUI();
        GameController.gameController.RestartTraining();
    }

    public void ClearArena()
    {
        foreach (Slot slot in GameController.gameController.playerArena._slots)
        {
            slot.Clear();
        }
        foreach (Slot slot in GameController.gameController.enemyArena._slots)
        {
            slot.Clear();
        }
    }

    public void AddMoney()
    {
        GameController.gameController.playerData.AddMoney(1000);
        UpdateUI();
    }

    public void ChangeReclameStatus()
    {
        if (reclame)
        {
            reclame = false;
            reclameText.text = "Rec OFF";
        }
        else
        {
            reclame = true;
            reclameText.text = "Rec ON";
        }
    }

    //Cards Menu Code________________________________________________________________

    public void UpdateCards(bool isMelee)
    {
        int openedUnits = isMelee ? GameController.gameController.playerData.meleeOpened : GameController.gameController.playerData.rangeOpened;

        for (int i = 0; i < cardsArray.Length; i++)
        {
            Slime slime = isMelee ? slimeBase.playerMeelePrefab[i].GetComponent<Slime>() : slimeBase.playerRangePrefab[i].GetComponent<Slime>();
            Sprite image = isMelee ? SlimeBase.slimeBase.playerMeleeSprites[i] : SlimeBase.slimeBase.playerRangeSprites[i];


            if (i <= openedUnits)
            {
                cardsArray[i].SetCard(image, slime.unitName, slime._maxHealth, slime._damage, true);
            }
            else
            {
                cardsArray[i].SetCard(image, slime.unitName, slime._maxHealth, slime._damage, false);
            }
        }
    }

    public void OpenCardsMenu()
    {
        playerUIActive = false;
        UpdateCards(true);
        cardsMenu.SetActive(true);
    }
    
    public void CloseCardsMenu()
    {
        playerUIActive = true;
        cardsMenu.SetActive(false);
    }

    //New Unit Card Code_____________________________________________________________

    public void ShowNewUnit(bool isMelee, int lvl)
    {
        newCardShown = true;
        playerUIActive = false;

        GameObject[] slimePref = isMelee ? SlimeBase.slimeBase.playerMeelePrefab : SlimeBase.slimeBase.playerRangePrefab;
        Slime slime = slimePref[lvl].GetComponent<Slime>();

        unitImage.sprite = isMelee ? SlimeBase.slimeBase.playerMeleeSprites[lvl] : SlimeBase.slimeBase.playerRangeSprites[lvl];
        unitName.text = slime.unitName;
        unitHP.text = "" + slime._maxHealth;
        unitAtk.text = "" + slime._damage;
        unitCard.SetActive(true);
    }

    public void CloseUnitCard()
    {
        newCardShown = false;
        playerUIActive = true;
        unitCard.SetActive(false);
    }

    //New Map Code__________________________________________________________________

    public void ShowNewMap(int spriteNum)
    {
        //newMapCardShown = true;
        newCardShown = true;
        playerUIActive = false;
        mapImage.sprite = mapSpriteArray[spriteNum];
        mapName.text = SlimeBase.slimeBase.arenaBack[spriteNum].name;
        mapCard.SetActive(true);
    }

    public void CloseMapCard()
    {
        //newCardShown = false;
        newCardShown = false;
        playerUIActive = true;
        mapCard.SetActive(false);
    }

    //Casino Wheel Code_____________________________________________________________

    public void StartWheel(bool victory)
    {
        if (victory)
        {
            victoryText.text = "VICTORY";
            victoryText.color = Color.white;
            lvlCompletedText.text = "Level Completed";
            lvlCompletedText.color = Color.white;
        }
        else
        {
            victoryText.text = "DEFEAT";
            victoryText.color = Color.red;
            lvlCompletedText.text = "Level NOT Completed";
            lvlCompletedText.color = Color.red;
        }

        earnedCoins.text = "" + CombineMoney((ulong)GameController.gameController.coinsOnLvl);
        wheelActive = true;
        //playerUI.SetActive(false);
        playerButt.SetActive(false);
        trainingUI.SetActive(false);
        wheelObject.SetActive(true);
        //RotateWheel();
        //StartCoroutine(RotateWheel());
    }

    public void RotateWheel()
    {
        arrow.Rotate(0, 0, -arrowSpeed);
        //yield return new WaitForSeconds(0.1f);

        arrowAngle = Mathf.RoundToInt(arrow.eulerAngles.z);

        switch (arrowAngle)
        {
            case int n when n >= 315 && n < 390:
                prize = 2;
                break;

            case int n when n >= 270 && n < 315:
                prize = 3;
                break;

            case int n when n >= 225 && n < 270:
                prize = 2;
                break;

            case int n when n >= 180 && n < 225:
                prize = 4;
                break;

            case int n when n >= 135 && n < 180:
                prize = 2;
                break;

            case int n when n >= 90 && n < 135:
                prize = 3;
                break;

            case int n when n >= 45 && n < 90:
                prize = 5;
                break;

            case int n when n >= 0 && n < 45:
                prize = 4;
                break;
        }

        prizeText.text = "X" + prize;
    }

    public void StopWheel()
    {
        wheelActive = false;
        
        if(!reclame)
            StartCoroutine(StopRotateWheel());
    }

    public IEnumerator StopRotateWheel()
    {
        ulong win = GameController.gameController.coinsOnLvl * (ulong)prize;
        earnedCoins.text = "" + CombineMoney(win);
        //GameController.gameController.playerData.AddMoney(win);
        GameController.gameController.soundController.PlayCasinoReward();
        //UpdateUI();
        StartCoroutine(rewardPile.AddMoney(win));
        rewardPile.RewardPileOfCoins(10);

        yield return new WaitForSeconds(1f);

        CloseWheelScreen();
    }

    public void StopWithoutPrize()
    {
        wheelActive = false;
        //GameController.gameController.playerData.AddMoney((ulong)GameController.gameController.coinsOnLvl);
        rewardPile.RewardPileOfCoins(10);
        StartCoroutine(rewardPile.AddMoney((ulong)GameController.gameController.coinsOnLvl));
        //UpdateUI();
        CloseWheelScreen();
        GameController.gameController.soundController.PlayCasinoReward();
    }

    public void CloseWheelScreen()
    {
        wheelObject.SetActive(false);

        if (!GameController.gameController.trainigActive)
        {
            //playerUI.SetActive(true);
            playerButt.SetActive(true);
            GameController.gameController.ResetArena();
        }
        else
        {
            trainingUI.SetActive(true);
            CallStep2();
            GameController.gameController.ResetTrainingArena();
        }
    }

    //Options Menu Code_____________________________________________________________

    public void OpenOptions()
    {
        playerUIActive = false;
        optionsMenu.SetActive(true);
    }

    public void TurnSound()
    {
        if (isSound)
        {
            AudioListener.volume = 0f;
            soundButton.sprite = soundOff;
            isSound = false;
        }

        else
        {
            AudioListener.volume = 1f;
            soundButton.sprite = soundOn;
            isSound = true;
        }
    }

    public void CloseOptions()
    {
        playerUIActive = true;
        optionsMenu.SetActive(false);
    }

    //Training Level UI Code___________________________________________________________

    public void UpdateTrainingUI()
    {
        if (!GameController.gameController.arenaActive)
        {
            playerButt.SetActive(false);
            trainingUI.SetActive(true);
        }
        
        if(GameController.gameController.stepCount == 1)
        {
            CallStep1();
        }
        
        if(GameController.gameController.stepCount == 4)
        {
            CallStep1();
        }
        
        if(GameController.gameController.stepCount == 5)
        {
            CallStep3();
        }
    }

    public void CallStep1()
    {
        reclame = false;
        trainingBuyMeleeButt.SetActive(false);
        trainingBuyRangeButt.SetActive(false);
        
        if (GameController.gameController.arenaActive)
            trainingStartButt.SetActive(false);
        else
            trainingStartButt.SetActive(true);
    }

    public void CallStep2()
    {
        playerUI.SetActive(true);
        trainingBack.SetActive(true);
        trainingStartButt.SetActive(false);
        trainingBuyMeleeButt.SetActive(true);
        trainingBuyRangeButt.SetActive(false);
    }

    public void BuyTrainingUnit()
    {
        GameController.gameController.playerArena._slots[7].SpawnSlime(true, true, 1);
        trainingBack.SetActive(false);
        trainingBuyMeleeButt.SetActive(false);
        GameController.gameController.step2arrow.SetActive(true);
        GameController.gameController.stepCount = 3;
    }

    public void CallStep3()
    {
        playerButt.SetActive(true);
        trainingUI.SetActive(false);
        //reclame = true;
    }

    public void StartTrainingArena()
    {
        if (playerUIActive)
        {
            GameController.gameController.ArenaStart();
            GameController.gameController.playerUnits.Save();
            trainingStartButt.SetActive(false);
        }
    }
}

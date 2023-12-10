
using UnityEngine;

public class SlimeBase : MonoBehaviour
{
    public static SlimeBase slimeBase;

    public GameObject mergeAnimation;
    public GameObject spawnAnimation;

    [Header("Unit Base")]
    public GameObject[] playerMeelePrefab;
    public GameObject[] playerRangePrefab;
    public GameObject[] enemyMeelePrefab;
    public GameObject[] enemyRangePrefab;

    [Header("Unit Images")]

    public Sprite[] playerMeleeSprites;
    public Sprite[] playerRangeSprites;

    [Header("Arena Sprites")]

    public Sprite[] arenaBack;
    public Sprite[] arenaGrid;

    [Header("Game Levels")]
    public Slot[] enemyArena;
    public GameObject[] level1;
    public GameObject[] level2;
    public GameObject[] level3;
    public GameObject[] level4;
    public GameObject[] level5;
    public GameObject[] level6;
    public GameObject[] level7;
    public GameObject[] level8;
    public GameObject[] level9;
    public GameObject[] level10;
    public GameObject[] level11;
    public GameObject[] level12;
    public GameObject[] level13;
    public GameObject[] level14;
    public GameObject[] level15;
    public GameObject[] level16;
    public GameObject[] level17;
    public GameObject[] level18;
    public GameObject[] level19;
    public GameObject[] level20;
    public GameObject[] level21;
    public GameObject[] level22;
    public GameObject[] level23;
    public GameObject[] level24;
    public GameObject[] level25;
    public GameObject[] level26;
    public GameObject[] level27;
    public GameObject[] level28;
    public GameObject[] level29;
    public GameObject[] level30;
    public GameObject[][] levelsArray;

    private void Awake()
    {
        slimeBase = this;
        FillLevels();
    }

    private void FillLevels()
    {
        levelsArray = new GameObject[30][];
        levelsArray[1] = level1;
        levelsArray[2] = level2;
        levelsArray[3] = level3;
        levelsArray[4] = level4;
        levelsArray[5] = level5;
        levelsArray[6] = level6;
        levelsArray[7] = level7;
        levelsArray[8] = level8;
        levelsArray[9] = level9;
        levelsArray[10] = level10;
        levelsArray[11] = level11;
        levelsArray[12] = level12;
        levelsArray[13] = level13;
        levelsArray[14] = level14;
        levelsArray[15] = level15;
        levelsArray[16] = level16;
        levelsArray[17] = level17;
        levelsArray[18] = level18;
        levelsArray[19] = level19;
        levelsArray[20] = level20;
        levelsArray[21] = level21;
        levelsArray[22] = level22;
        levelsArray[23] = level23;
        levelsArray[24] = level24;
    }
}

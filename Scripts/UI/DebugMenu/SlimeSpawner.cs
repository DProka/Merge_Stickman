using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class SlimeSpawner : MonoBehaviour
{

    public GameObject sliderObj;

    public CreateArena playerArena;
    public CreateArena enemyArena;
    
    public void SpawnMeleeSlime(bool isPlayer)
    {
        UnityEngine.UI.Slider slider = sliderObj.GetComponent<UnityEngine.UI.Slider>();
        int slimeLevel = (int) slider.value;

        Slot[] slot = isPlayer ? playerArena._slots : enemyArena._slots;


        for (int i = 0; i < slot.Length; i++)
        {
            if (slot[i].IsEmpty())
            {
                slot[i].SpawnSlime(isPlayer, true, slimeLevel);
                Instantiate(SlimeBase.slimeBase.spawnAnimation, slot[i].position);
                break;
            }
        }
    }
    
    public void SpawnRangeSlime(bool isPlayer)
    {
        UnityEngine.UI.Slider slider = sliderObj.GetComponent<UnityEngine.UI.Slider>();
        int slimeLevel = (int) slider.value;

        Slot[] slot = isPlayer ? playerArena._slots : enemyArena._slots;


        for (int i = slot.Length - 1; i >= 0; i--)
        {
            if (slot[i].IsEmpty())
            {
                slot[i].SpawnSlime(isPlayer, false, slimeLevel);
                Instantiate(SlimeBase.slimeBase.spawnAnimation, slot[i].position);
                break;
            }
        }
    }
}

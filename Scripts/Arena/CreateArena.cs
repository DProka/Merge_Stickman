using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class CreateArena : MonoBehaviour
{
    //public Arena arena;
    //public GameObject startedPos;
    //public Slot slotPrefab;
    //public SpriteAtlas slotAtlas;

    //public int slotsInRow;
    //public int slotsInColumn;

    

    public Slot[] _slots;


    void Start()
    {
        //Vector3 startingPos = startedPos.transform.position;
        //_slots = new Slot[slotsInRow * slotsInColumn];
        
        //for (int i = 0; i < slotsInRow*slotsInColumn; i++)
        //{
        //    if (i % slotsInRow == 0 && i != 0)
        //    {
        //        startingPos.y -= 1;
        //        startingPos.x -= slotsInRow;
        //    }
        //    _slots[i] = Instantiate(slotPrefab, startingPos, Quaternion.identity);
        //    _slots[i].transform.SetParent(gameObject.transform);
        //    _slots[i].name = "Slot" + i;
        //    _slots[i].GetComponent<SpriteRenderer>().sprite = i % 2 == 0
        //        ? slotAtlas.GetSprite(Const.SlotSprites.SlotForest1.ToString())
        //        : slotAtlas.GetSprite(Const.SlotSprites.SlotForest2.ToString());

        //    startingPos.x += + 1;
        //}
    }
}

using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections.Generic;

public class DragAndDropSlimes : MonoBehaviour
{
    //private Slime slime;

    //private void OnMouseDrag()
    //{
    //    if (GameController.gameController.arenaActive || !GameController.gameController.uiScript.playerUIActive)
    //        return;

    //    if (Input.GetMouseButton(0) && GameController.gameController.uiScript.playerUIActive)
    //    {
    //        Vector3 touchPos = GameController.gameController.CheckMousePosition();
    //        touchPos.x += 0.5f;
    //        touchPos.z += 0.5f;
    //        touchPos.z = 0;

    //        Debug.Log(touchPos);

    //        List<Slime> slimeList = GameController.gameController.playerList;

    //        for (int i = 0; i < slimeList.Count; i++)
    //        {
    //            if(slimeList[i].transform.position == touchPos)
    //            {
    //                slime = slimeList[i];
    //            }
    //        }

    //        slime.transform.position = touchPos;

    //        Vector3 unitpos = ConvertUnitpositionToInt(touchPos.x, touchPos.y);

    //        Slot[] playerSlots = GameController.gameController.playerArena._slots;

    //        for (int i = 0; i < playerSlots.Length; i++)
    //        {
    //            if (playerSlots[i].transform.position == unitpos)
    //            {
    //                slime._slotToChange = playerSlots[i];
    //            }
    //        }
    //    }
    //}

    //private void OnMouseUp()
    //{
    //    if (GameController.gameController.arenaActive)
    //    { return; }

    //    if (slime._slotToChange != null)
    //    {
    //        Slot parentSlot = slime._slotParent;
    //        Slot nextSlot = slime._slotToChange;

    //        if (nextSlot != parentSlot)
    //        {
    //            if (nextSlot.isEmpty)
    //            {
    //                parentSlot.SetSlime(null);
    //                nextSlot.SetSlime(slime);
    //                slime._slotParent = nextSlot;
    //                transform.position = nextSlot.position.position;
    //                slime._originPos = transform.position;
    //            }
    //            else
    //            {
    //                Slime slimeNext = nextSlot.slimeScr;
    //                if (slimeNext._level == slime._level && slimeNext._isMelee == slime._isMelee)
    //                {
    //                    parentSlot.SetSlime(null);
    //                    nextSlot.SpawnSlime(slime._isPlayerSlime, slime._isMelee, slime._level + 1);
    //                    GameController.gameController.playerData.UnitOpened(slime._isMelee, slime._level);
    //                    Instantiate(SlimeBase.slimeBase.mergeAnimation, nextSlot.position);
    //                    GameController.gameController.RemoveFromList(slimeNext);
    //                    Destroy(slimeNext.gameObject);
    //                    GameController.gameController.RemoveFromList(slime);
    //                    Destroy(gameObject);

    //                    GameController.gameController.soundController.PlayUnitUp();

    //                }
    //            }
    //        }

    //        slime._slotToChange = null;
    //    }

    //    gameObject.transform.position = slime._originPos;
    //    gameObject.transform.Translate(0, 0, -0.5f);
    //    slime.attackTimer = 0;
    //}

    //private Vector3Int ConvertUnitpositionToInt(float x, float y)
    //{
    //    int x1 = (int)x;
    //    int y1 = (int)y;
    //    Vector3Int vector3Int = new Vector3Int(x1, y1);

    //    return vector3Int;
    //}
}

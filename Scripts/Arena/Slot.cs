
using UnityEngine;

public class Slot : MonoBehaviour
{
    public Transform position;
    public GameObject slimePrefab;
    public Slime slimeScr;
    public GameObject spawnAnimation;
    public GameObject _slimeObj;

    public bool isEmpty = true;
    public bool IsPlayer;

    public void SpawnSlime(bool isPlayer, bool isMelee, int level)
    {
        if (isPlayer)
        {
            _slimeObj = isMelee
            ? Instantiate(SlimeBase.slimeBase.playerMeelePrefab[level-1], position.position, Quaternion.identity)
            : Instantiate(SlimeBase.slimeBase.playerRangePrefab[level-1], position.position, Quaternion.identity);
        }
        else
        {
            _slimeObj = isMelee
            ? Instantiate(SlimeBase.slimeBase.enemyMeelePrefab[level-1], position.position, Quaternion.identity)
            : Instantiate(SlimeBase.slimeBase.enemyRangePrefab[level-1], position.position, Quaternion.identity);
        }

        //Instantiate(SlimeBase.slimeBase.spawnAnimation, transform);
        //_slimeObj.transform.SetParent(position);
        slimeScr = _slimeObj.GetComponent<Slime>();
        slimeScr._slotParent = this;
        slimeScr._isPlayerSlime = isPlayer;
        slimeScr._isMelee = isMelee;
        slimeScr._originPos = position.position;
        GameController.gameController.FillEnemyList(IsPlayer, slimeScr);
        slimeScr.ResetSlimePos();
        //UpdateSlot();
        isEmpty = false;
    }

    public bool IsEmpty()
    {
        return _slimeObj == null;
    }

    public void SetSlime(Slime slime1)
    {
        slimeScr = slime1;

        if (slimeScr == null)
        {
            _slimeObj = null;
            isEmpty = true;
        }
        else
        {
            _slimeObj = slime1.gameObject;
            isEmpty = false;
            if (slime1._isPlayerSlime)
            {
                GameController.gameController.playerUnits.UpdatePlayerUnits();
            }
        }
    }

    public void UpdateSlot()
    {
        if (slimeScr != null)
        {
            _slimeObj = slimeScr.gameObject;
            isEmpty = false;
        }
        else
        {
            _slimeObj = null;
            isEmpty = true;
        }
    }

    public void Clear()
    {
        GameController.gameController.RemoveFromList(slimeScr);
        Destroy(_slimeObj);
        _slimeObj = null;
        slimeScr = null;
        isEmpty = true;
    }
    
}

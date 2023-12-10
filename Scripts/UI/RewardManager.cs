
using DG.Tweening;
using System.Collections;
using UnityEngine;


public class RewardManager : MonoBehaviour
{
    public UIScript uiScript;
    public GameObject pileOfCoinParent;
    public RectTransform finishPosition;
    public Transform finishPos;

    private Vector3[] initialPosition;
    private Quaternion[] initialRotation;

    public void Init()
    {
        initialPosition = new Vector3[pileOfCoinParent.transform.childCount];
        initialRotation = new Quaternion[pileOfCoinParent.transform.childCount];

        for (int i = 0; i < pileOfCoinParent.transform.childCount; i++)
        {
            initialPosition[i] = pileOfCoinParent.transform.GetChild(i).position;
            initialRotation[i] = pileOfCoinParent.transform.GetChild(i).rotation;
        }
    }

    private void Reset()
    {
        for (int i = 0; i < pileOfCoinParent.transform.childCount; i++)
        {
            pileOfCoinParent.transform.GetChild(i).position = initialPosition[i];
            pileOfCoinParent.transform.GetChild(i).rotation = initialRotation[i];
        }
    }

    public void RewardPileOfCoins(int noCoin)
    {
        Reset();

        float delay = 0f;

        pileOfCoinParent.SetActive(true);

        for (int i = 0; i < pileOfCoinParent.transform.childCount; i++)
        {
            pileOfCoinParent.transform.GetChild(i).DOScale(1f, 0.3f).SetDelay(delay).SetEase(Ease.OutBack);

            pileOfCoinParent.transform.GetChild(i).GetComponent<RectTransform>().DOMove(finishPosition.position, 0.5f)
                .SetDelay(delay + 0.5f).SetEase(Ease.InCirc);

            pileOfCoinParent.transform.GetChild(i).DOScale(0f, 0.3f).SetDelay(delay + 1.5f).SetEase(Ease.OutBack);

            

            delay += 0.1f;
        }

        //StartCoroutine(AddMoney());

    }

    public IEnumerator AddMoney(ulong money)
    {
        for (int i = 0; i < pileOfCoinParent.transform.childCount; i++)
        {
            GameController.gameController.playerData.AddMoney(money / 10);
            yield return new WaitForSeconds(0.2f);
            uiScript.UpdateUI();
        }
        
    }
}


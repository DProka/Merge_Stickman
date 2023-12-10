using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    public Image unitImage;
    public TextMeshProUGUI unitName;
    public TextMeshProUGUI unitHP;
    public TextMeshProUGUI unitAtk;

    public bool isOpen = false;

    public void SetCard(Sprite image, string name, int hp, int atk, bool isOpen)
    {
        if (isOpen)
        {
            unitImage.color = Color.white;
            unitImage.sprite = image;
            unitName.text = name;
            unitHP.text = "" + hp;
            unitAtk.text = "" + atk;
        }
        else
        {
            unitImage.color = Color.black;
            unitImage.sprite = image;
            unitName.text = "Closed";
            unitHP.text = "--";
            unitAtk.text = "--";
        }
        
    }

    public void CloseCard()
    {
        unitImage.color = Color.black;
        unitName.text = "Closed";
        unitHP.text = "--";
        unitAtk.text = "--";
    }
}

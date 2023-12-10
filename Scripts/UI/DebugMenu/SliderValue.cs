using TMPro;
using UnityEngine;

public class SliderValue : MonoBehaviour
{
    public GameObject slider;

    private UnityEngine.UI.Slider _slider;

    public void Start()
    {
        _slider = slider.GetComponent<UnityEngine.UI.Slider>();
    }

    public void SliderScript()
    {
        // Debug.Log("Slider ValueChanged "+ _slider.value);
        gameObject.GetComponent<TextMeshProUGUI>().SetText("" + _slider.value);
    }

}

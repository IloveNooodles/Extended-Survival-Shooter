using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderController : MonoBehaviour
{
    public TMP_Text valueText;

    public void Start() {
        valueText.text = Mathf.Round(PlayerPrefs.GetFloat("Volume", 0.5f) * 100).ToString();
        GetComponent<Slider>().value = PlayerPrefs.GetFloat("Volume", 0.5f);
    }

    public void onSliderChanged(float value) {
        value = Mathf.Round(value * 100);
        valueText.text = value.ToString();
    }
}

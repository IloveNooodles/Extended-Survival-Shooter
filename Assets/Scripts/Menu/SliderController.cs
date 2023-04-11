using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderController : MonoBehaviour
{
    public TMP_Text valueText;

    public void onSliderChanged(float value) {
        value = Mathf.Round(value * 100);
        valueText.text = value.ToString();
    }
}

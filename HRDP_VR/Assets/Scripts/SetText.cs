using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetText : MonoBehaviour
{
    private Slider sliderAsset;//Actual value in slide
    public Text textAsset;//show Text
    public float value;
    void Start()
    {
        sliderAsset = GetComponent<Slider>();
        SetTextToValue();
    }
    public void SetTextToValue()
    {

        value = (float)(Mathf.Round(sliderAsset.value * 1000)) / 1000;//(float)sliderAsset.value;
        textAsset.text = value + "";
    }

    // Update is called once per frame
    
}

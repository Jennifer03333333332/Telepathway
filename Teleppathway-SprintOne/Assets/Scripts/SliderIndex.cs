using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderIndex : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    public Text text;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = slider.value.ToString();
    }
}

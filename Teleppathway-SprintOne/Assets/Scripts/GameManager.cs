using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] UISteps;
    public GameObject Garden;
    public GameObject KSlider;
    int step = 0;
    public int K=0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NextStep()
    {
        UISteps[step].SetActive(false);
        if (step == 0)
        {
            Garden.GetComponent<PlantSeed>().enabled = false;
        }
        else if(step == 1)
        {
            K = (int)KSlider.GetComponent<Slider>().value;
        }
        step++;
        if (step < UISteps.Length)
        {
            UISteps[step].SetActive(true);
        }
        

    }
}

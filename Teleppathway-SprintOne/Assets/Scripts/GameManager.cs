using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] UISteps;
    public GameObject Garden;
    int step = 0;
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
        step++;
        if (step < UISteps.Length)
        {
            UISteps[step].SetActive(true);
        }
        

    }
}

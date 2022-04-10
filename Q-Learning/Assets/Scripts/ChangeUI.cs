using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] UISteps;
    public GameObject Slider;
    public GameObject wantFood;
    int step = 0;
    void Start()
    {
        UISteps[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextStep()
    {
        UISteps[step].SetActive(false);
        step++;
        if (step < UISteps.Length)
        {
            UISteps[step].SetActive(true);

            if(SceneManager.GetActiveScene().name=="Level 2")
            {
                if (step == 1)
                {
                    wantFood.SetActive(true);
                }
                if (step == 3)
                {
                    Slider.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(195, -587, 0);
                    wantFood.SetActive(false);
                }
                
            }
        }
    }


}

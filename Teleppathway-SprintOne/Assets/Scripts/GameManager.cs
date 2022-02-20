using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager Instance = null;
    public GameObject[] UISteps;
    public GameObject Garden;
    public GameObject KSlider;
    public GameObject SortFlower;
    public Text Step7Text;
    public Text Step3text;
    public int step = 0;
    public int K=0;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
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
        if (step == 1)
        {
            Garden.GetComponent<PlantSeed>().enabled = false;
        }
        else if(step == 2)
        {
            K = (int)KSlider.GetComponent<Slider>().value;
            SortFlower.GetComponent<SortFlower>().Kvalue = K;
            SortFlower.GetComponent<SortFlower>().means=  new double[K][];
        }
        step++;
        if (step < UISteps.Length)
        {
            UISteps[step].SetActive(true);
            
            if (step == 1)
            {
                Garden.GetComponent<PlantSeed>().enabled = true;
            }
            if (step == 3)
            {
                Step3text.text = "Choose "+ K + " sprout. They are the initial mean for each clusters.\n\n(Mean is the center or centroid of each clusters, the solid dot is the Mean point.)";
            }
            if (step == 5)
            {
                SortFlower.GetComponent<SortFlower>().showmean = true;
                SortFlower.GetComponent<SortFlower>().CalculateMean();
            }
            if (step == 6)
            {
                SortFlower.GetComponent<SortFlower>().firstround = false;
                SortFlower.GetComponent<SortFlower>().CalculateKeans();
            }
            if (step == 7)
            {
                Step7Text.text = "Congratulations!  You successfully sort the sprouts into " + K + " clusters!";
            }
            Time.timeScale = 1;
        }
        

    }

    public void Restart()
    {
        SceneManager.LoadScene("Sprint1-A");
    }
}

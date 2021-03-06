using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] UISteps;
    public GameObject Slider;
    public GameObject wantFood;
    public Text lavatext;
    public GameObject Detail;
    int step = 0;
    int maxLava = 3;
    //int count = 0;
    void Start()
    {
        if (UISteps[0] != null)
        {
            UISteps[0].SetActive(true);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level 2" && step==2&&!UISteps[step].transform.GetChild(0).gameObject.activeSelf)
        {
            if (GetComponent<GridEnvironment>().trainingtimes > 100)
            {
                UISteps[step].transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        if (SceneManager.GetActiveScene().name == "Level 3" && step == 3 && !UISteps[step].transform.GetChild(0).gameObject.activeSelf)
        {
            if (GetComponent<GridEnvironment>().trainingtimes > 100)
            {
                UISteps[step].transform.GetChild(0).gameObject.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
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
                
                if (step == 2)
                {
                    Slider.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(195, -670, 0);
                    wantFood.SetActive(false);
                    //StartCoroutine(ShowNextLevelButton(UISteps[step].transform.GetChild(0).gameObject, 30));
                    
                }
                
            }
            if (SceneManager.GetActiveScene().name == "Level 3")
            {
                if (step == 3)
                {
                    Slider.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(195, -587, 0);
                    //wantFood.SetActive(false);
                    //StartCoroutine(ShowNextLevelButton(UISteps[step].transform.GetChild(0).gameObject, 30));

                }
            }
            if (SceneManager.GetActiveScene().name == "Level 4")
            {
                if (step == 3)
                {
                    Slider.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(195, -366, 0);
                    //wantFood.SetActive(false);
                }
            }
            if (SceneManager.GetActiveScene().name == "Level 5")
            {
                if (step == 3)
                {
                    Slider.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(195, -316, 0);
                    //wantFood.SetActive(false);
                }
            }
        }
        
    }

    public void CountLava()
    {
        maxLava--;
        lavatext.text = "X " + maxLava;
        if (maxLava == 0)
        {
            NextStep();
        }

    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    IEnumerator ShowNextLevelButton(GameObject button,float waittime)
    {
        yield return new WaitForSeconds(waittime);
        button.SetActive(true);
    }

    public void ShowDetail()
    {
        if (Detail.activeSelf)
        {
            Detail.SetActive(false);
        }
        else
        {
            Detail.SetActive(true);
        }
    }

    public void QuitProgram()
    {
        Application.Quit();
    }

}

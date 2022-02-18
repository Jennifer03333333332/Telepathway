using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPositon : MonoBehaviour
{
    // Start is called before the first frame update
    public Text text;
    void Start()
    {
        text.text = "X:" + Math.Round(transform.position.x,2) + "\n" + "Y:" + Math.Round(transform.position.z,2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeleteSeed()
    {
        if (GameManager.Instance.step == 0 || GameManager.Instance.step == 1)
        {
            Destroy(gameObject);
        }
        
    }
    
}

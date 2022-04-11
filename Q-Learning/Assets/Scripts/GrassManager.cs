using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject grass;
    public GameObject[,] grassarray = new GameObject[10,10];
    void Start()
    {
        //Transform[] father = grass.GetComponentsInChildren<Transform>();
        for (int i= 0;i<10;i++)
        {
            //Transform[] son = grass.transform.GetChild(i).GetComponentsInChildren<Transform>();
            for(int j = 0; j< 10; j++)
            {
                grassarray[i, j] = grass.transform.GetChild(i).GetChild(j).gameObject;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //grassarray[0, 0].transform.GetChild(0).GetComponent<GrassColor>().ChangeColor(Color.black);
    }

    public void HideGrass(int x, int z)
    {
        grassarray[z, x].SetActive(false);
    }

    public void ChangeColor(int x,int z,Color color)
    {
        if (grassarray[z, x].activeSelf)
        {
            grassarray[z, x].transform.GetChild(0).GetComponent<GrassColor>().ChangeColor(color);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SortFlower : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject flowersparent;
    public GameObject flowerPrefeb;
    private KMeansResults result;
    public int Kvalue;
    public GameObject Slier;
    public GameObject inputx, inputy;
    List<GameObject> flowers = new List<GameObject>();
    public Text Kvaluetext, XText, YText;
    void Start()
    {
        //flowers = this.gameObject.transform.GetChild();
        Kvalue = (int) Slier.GetComponent<Slider>().value;
        
    }

    // Update is called once per frame
    void Update()
    {
        Kvaluetext.text = "Kvalue: " + Slier.GetComponent<Slider>().value;
        XText.text = "X: " + inputx.GetComponent<Slider>().value;
        YText.text = "Y: " + inputy.GetComponent<Slider>().value;
    }

    public void StartKmeans()
    {
        flowers.Clear();
        foreach (Transform child in transform)
        {         
                flowers.Add(child.gameObject);
            
        }

        Kvalue = (int)Slier.GetComponent<Slider>().value;
        result = KMeans.Cluster(flowers.ToArray(), Kvalue, 1000, 101);
        for (int i = 0; i < result.clusters.Length; i++)
        {
            Color color = Color.HSVToRGB(1f * i / result.clusters.Length, 1f, 1f);
            for (int j = 0; j < result.clusters[i].Length; j++)
            {

                flowers[result.clusters[i][j]].GetComponent<MeshRenderer>().material.color = color;
            }
        }
    }

    public void CreateNewPoint()
    {
        Instantiate(flowerPrefeb, new Vector3(inputx.GetComponent<Slider>().value, 0.3f, inputy.GetComponent<Slider>().value), Quaternion.identity,flowersparent.transform);
        
        

    }
}

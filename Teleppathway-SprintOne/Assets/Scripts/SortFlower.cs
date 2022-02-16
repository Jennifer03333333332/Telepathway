using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SortFlower : MonoBehaviour
{
    public GameObject FlowerFolder;//Jennifer
    // Start is called before the first frame update
    public GameObject seedsParent;
    public GameObject SeedPrefeb;
    public GameObject flowerParent;
    public GameObject[] FlowerPrefeb;
    private KMeansResults result;
    public int Kvalue;
    public GameObject Slier;
    public GameObject inputx, inputy;
    public static List<GameObject> flowers = new List<GameObject>();
    public Text Kvaluetext, XText, YText;
    double[][] means;
    int chooseintialK = 0;

    

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
        List<Vector2> flowersPos = new List<Vector2>();//Jennifer
        List<Vector2> meansPos = new List<Vector2>();//Jennifer
        flowers.Clear();
        foreach (Transform child in flowerParent.transform)
        {
            Destroy(child.gameObject);

        }
        foreach (Transform child in seedsParent.transform)
        {         
                flowers.Add(child.gameObject);
            
        }

        Kvalue = (int)Slier.GetComponent<Slider>().value;
        result = KMeans.Cluster(flowers.ToArray(), Kvalue, 1000, 1);
        seedsParent.SetActive(false);
        for (int i = 0; i < result.clusters.Length; i++)
        {
            for (int j = 0; j < result.clusters[i].Length; j++)
            {
                Vector3 tmppos = flowers[result.clusters[i][j]].transform.position;//Jennifer
                flowersPos.Add(new Vector2(tmppos.x, tmppos.z));//Jennifer
                GameObject f= Instantiate(FlowerPrefeb[i], flowers[result.clusters[i][j]].transform.position, flowers[result.clusters[i][j]].transform.rotation, flowerParent.transform);
                f.transform.localScale = new Vector3(5, 5, 5);
                //flowers[result.clusters[i][j]].GetComponent<MeshRenderer>().material.color = color;
            }
        }
        //Jennifer TODO
        foreach(var pos in result.means)
        {
            meansPos.Add(new Vector2((float)pos[0], (float)pos[1]));
        }
        //Debug.Log(result.means[0][0]+"means");
        //Debug.Log(result.means[0][1]+"means");
        //Voronoi3DManager.SendMessage("StartGenerateMap", flowersPos);
        Voronoi3DProperty.MapProperty(flowersPos, meansPos);
        //FlowerFolder.SendMessage("GenerateVMap");
    }

    public void CreateNewPoint()
    {
        Instantiate(SeedPrefeb, new Vector3(inputx.GetComponent<Slider>().value, 1f, inputy.GetComponent<Slider>().value), Quaternion.identity, seedsParent.transform);
        
        

    }
}

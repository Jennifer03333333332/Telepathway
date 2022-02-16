using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Linq;

public class SortFlower : MonoBehaviour, IPointerDownHandler
{
    public GameObject FlowerFolder;//Jennifer
    // Start is called before the first frame update
    public GameObject seedsParent;
    public GameObject flowerParent;
    public GameObject[] FlowerPrefeb;
    private KMeansResults result;
    public int Kvalue;
    public static List<GameObject> flowers = new List<GameObject>();
   
    int chooseintialK = 0;
    public GameObject NextStepButton;

    double[][] data;
    public double[][] means;
    List<int[]> clusters = new List<int[]>();

    List<int>[] individualcluster = new List<int>[9];
    
    //int[][] clusters;

    void Start()
    {
        for(int i = 0; i < 9; i++)
        {
            individualcluster[i] = new List<int>();
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
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

        //Kvalue = (int)Slier.GetComponent<Slider>().value;
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
    */

    public void CalculateKeans()
    {
        StartCoroutine(WaitTimeforEachStep(1));

        //Debug.Log(means[4][0] +"  "+ means[4][1]);
        
    }

    public double[] ReturnDistance( double[] index)
    {
        double[] distance = new double [Kvalue];
        for(int i=0; i < Kvalue; i++)
        {
            distance[i]=CalculateDistance(index, means[i]);

        }
        return distance;
    }

    public void OnPointerDown(PointerEventData data)
    {
        
        if (chooseintialK<Kvalue&&GameManager.Instance.step==2)
        {
            
            Ray ray = Camera.main.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "Seed")
                {
                    hit.transform.gameObject.SetActive(false);
                    GameObject f=Instantiate(FlowerPrefeb[chooseintialK], hit.transform.position, Quaternion.identity, flowerParent.transform);
                    f.transform.localScale = new Vector3(5, 5, 5);
                    means[chooseintialK] = new double[] { hit.transform.position.x, hit.transform.position.z };
                    chooseintialK++;
                    
                }

            }
            if (chooseintialK == Kvalue && GameManager.Instance.step == 2)
            {
                NextStepButton.SetActive(true);
            }
        }
        
        

    }


    public double[][] Cluster(GameObject[] items)
    {
        double[][] data = new double[items.Length][];
        for (int i = 0; i < items.Length; i++)
        {

            Vector2 v = new Vector2(items[i].transform.position.x, items[i].transform.position.z);
            data[i] = new double[] { v.x, v.y };
        }
        return data;
    }

    private static double CalculateDistance(double[] point, double[] centroid)
    {
        // For each attribute calculate the squared difference between the centroid and the point
        double sum = 0;
        for (int i = 0; i < point.Length; i++)
            sum += Math.Pow(centroid[i] - point[i], 2);

        return Math.Sqrt(sum);
    }

    IEnumerator WaitTimeforEachStep(float waittime)
    {
        //means = new double[Kvalue][];
        clusters.Clear();
        int i = 0;
        foreach (Transform child in seedsParent.transform)
        {
            flowers.Add(child.gameObject);
            if (!child.gameObject.activeSelf)
            {
                if (i < Kvalue)
                {
                    //means[i] = new double[] { child.gameObject.transform.position.x, child.gameObject.transform.position.z };
                    i++;
                }
            }
        }
        data = Cluster(flowers.ToArray());
        for (int j = 0; j < data.Length; j++)
        {
            double[] distance = new double[Kvalue];
            for (int k = 0; k < Kvalue; k++)
            {
                distance = ReturnDistance(data[j]);
            }
            double shortestdistance = double.MaxValue;
            int shortestindex = -1;
            for (int a = 0; a < Kvalue; a++)
            {
                if (distance[a] < shortestdistance)
                {
                    shortestdistance = distance[a];
                    shortestindex = a;
                }
            }
            Debug.Log(shortestdistance);
            Debug.Log(shortestindex);
            individualcluster[shortestindex].Add(j);
            seedsParent.transform.GetChild(j).gameObject.SetActive(false);
            GameObject f = Instantiate(FlowerPrefeb[shortestindex], seedsParent.transform.GetChild(j).transform.position, Quaternion.identity, flowerParent.transform);
            f.transform.localScale = new Vector3(5, 5, 5);
            yield return new WaitForSeconds(waittime);
        }
        
    }
}

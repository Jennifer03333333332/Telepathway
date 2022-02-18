using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Linq;
using TMPro;

public class SortFlower : MonoBehaviour, IPointerDownHandler
{
    public GameObject FlowerFolder;//Jennifer
    // Start is called before the first frame update
    public GameObject seedsParent;
    public GameObject flowerParent;
    public GameObject[] FlowerPrefeb;
    public GameObject InitialMeanFlowers;
    public GameObject MeanPrefeb;
    public GameObject MeanParent;
    public GameObject NextStepButton3;
    public GameObject NextStepButton4;
    //public GameObject NextStepButton5;
    public GameObject NextStepButton6;
    public GameObject[] FasterButton;
    public GameObject DistanceTextPrefeb;
    private Color32[] flowercolor;
    private bool pause = false;
    public GameObject[] PauseButton;
    public float StepTime = 1f;
    private bool faster = false;
    public int Kvalue;
    public static List<GameObject> flowers = new List<GameObject>();
    public Text TextUI;
    public bool firstround=true;
    public bool showmean = false;
   
    int chooseintialK = 0;
    
    int round = 0;
    double[][] data;
    public double[][] means;
    List<int[]> clusters = new List<int[]>();

    List<int>[] individualcluster = new List<int>[9];
    private List<GameObject> MeansPoints = new List<GameObject>();

    //int[][] clusters;

    void Start()
    {
        for(int i = 0; i < 9; i++)
        {
            individualcluster[i] = new List<int>();
        }
        flowercolor = new Color32[9];
        flowercolor[0] = new Color32 (154, 125, 233, 255 );
        flowercolor[1] = new Color32(69, 163, 200, 255);
        flowercolor[2] = new Color32(204, 204, 204, 255);
        flowercolor[3] = new Color32(184, 43, 33, 255);
        flowercolor[4] = new Color32(253, 192, 30, 255);
        flowercolor[5] = new Color32(200, 100, 77, 255);
        flowercolor[6] = new Color32(238, 171, 164, 255);
        flowercolor[7] = new Color32(236, 118, 35, 255);
        flowercolor[8] = new Color32(57, 76, 193, 255);


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
        StartCoroutine(WaitTimeforEachStep());

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
        
        if (chooseintialK<Kvalue&&GameManager.Instance.step==3)
        {
            
            Ray ray = Camera.main.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "Seed")
                {
                    Color color = flowercolor[chooseintialK];
                    hit.transform.gameObject.SetActive(false);
                    GameObject f=Instantiate(FlowerPrefeb[chooseintialK], hit.transform.position, Quaternion.identity, InitialMeanFlowers.transform);
                    f.transform.localScale = new Vector3(5, 5, 5);
                    means[chooseintialK] = new double[] { hit.transform.position.x, hit.transform.position.z };
                    chooseintialK++;
                    GameObject m = Instantiate(MeanPrefeb, hit.transform.position, Quaternion.identity, MeanParent.transform);
                    m.GetComponent<MeshRenderer>().material.color = color;
                    m.GetComponent<LineRenderer>().startColor = color;
                    m.GetComponent<LineRenderer>().endColor = color;
                    m.GetComponent<LineRenderer>().material.color = color;
                    MeansPoints.Add(m);

                }

            }
            if (chooseintialK == Kvalue && GameManager.Instance.step == 3)
            {
                NextStepButton3.SetActive(true);
            }
        }
        else if(GameManager.Instance.step != 3)
        {
            Ray ray = Camera.main.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "Seed")
                {
                    if (!hit.transform.GetChild(3).gameObject.activeSelf)
                    {
                        hit.transform.GetChild(3).gameObject.SetActive(true);
                    }
                    else hit.transform.GetChild(3).gameObject.SetActive(false);
                }
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

    IEnumerator WaitTimeforEachStep()
    {
        //means = new double[Kvalue][];
        clusters.Clear();
        int i = 0;
        flowers.Clear();
        for(int ii = 0; ii < Kvalue; ii++)
        {
            individualcluster[ii].Clear();
        }
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
            TextUI.text = seedsParent.transform.GetChild(j).name + "\n";
            double[] distance = new double[Kvalue];
            distance = ReturnDistance(data[j]);
            for (int k = 0; k < Kvalue; k++)
            {
                TextUI.text = TextUI.text + "Distance to Mean " + (k+1).ToString() +": " + System.Math.Round(distance[k],2) +"\n";
                Vector3 position1 = MeansPoints[k].transform.position;
                Vector3 position2 = seedsParent.transform.GetChild(j).transform.position; 
                MeansPoints[k].GetComponent<LineRenderer>().SetPosition(0, position1);
                MeansPoints[k].GetComponent<LineRenderer>().SetPosition(1, position2);
                GameObject d = Instantiate(DistanceTextPrefeb,new Vector3((position1.x+position2.x)/2, 2, (position1.z + position2.z) / 2), Quaternion.Euler(70, 0, 0));
                d.GetComponent<TextMeshPro>().text = Math.Round(distance[k], 2).ToString();
                Destroy(d, StepTime);
                
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
            
            
            individualcluster[shortestindex].Add(j);
            seedsParent.transform.GetChild(j).gameObject.SetActive(false);
            if (round != 0)
            {
                Destroy(flowerParent.transform.GetChild(0).gameObject);
            }
            GameObject f = Instantiate(FlowerPrefeb[shortestindex], seedsParent.transform.GetChild(j).transform.position, Quaternion.identity, flowerParent.transform);
            f.transform.localScale = new Vector3(5, 5, 5);
            yield return new WaitForSeconds(StepTime);
        }
        if (firstround)
        {
            NextStepButton4.SetActive(true);
            InitialMeanFlowers.SetActive(false);

        }
        else
        {
            CalculateMean();
        }
        
        
        
       
        
    }

    public void CalculateMean()
    {
        bool changed = false;
        double[][] premean =(double[][]) means.Clone();
        Debug.Log(premean[0][0] + " " + premean[0][1]);
        for (int i = 0; i < Kvalue; i++)
        {
            double sumX = 0;
            double sumY = 0;
            for(int j = 0; j < individualcluster[i].ToArray().Length; j++)
            {
                sumX = sumX + data[individualcluster[i][j]][0];
                sumY = sumY + data[individualcluster[i][j]][1];
            }
            means[i] = new double[] {sumX/ individualcluster[i].ToArray().Length,sumY/ individualcluster[i].ToArray().Length };
            MeanParent.transform.GetChild(i).transform.position = new Vector3((float)means[i][0], MeanParent.transform.GetChild(i).transform.position.y, (float)means[i][1]);
            

            if (Math.Round(means[i][0],3) != Math.Round(premean[i][0],3)|| Math.Round(means[i][1],3) != Math.Round(premean[i][1],3))
            {
                changed = true;
            }

        }
        
        if (changed)
        {
            round++;
            if (!firstround)
            {
                StartCoroutine(WaitTimeforEachStep());
            }
            
        }
        else
        {
            Debug.Log(premean[0][0] +" "+ premean[0][1]);
            Debug.Log(means[0][0] + " " + means[0][1]);
            Debug.Log("Finish");
            for(int i = 0; i < Kvalue; i++)
            {
                MeansPoints[i].GetComponent<LineRenderer>().enabled = false;
            }
            NextStepButton6.SetActive(true);
        }

    }

    public void Faster()
    {
        if (faster)
        {
            faster = false;
            StepTime = 1f;
            if (GameManager.Instance.step == 4)
            {
                FasterButton[0].GetComponent<Image>().color = Color.white;
            }
            else if(GameManager.Instance.step == 6)
            {
                FasterButton[1].GetComponent<Image>().color = Color.white;
            }
        }
        else
        {
            faster = true;
            StepTime = 0.3f;
            if (GameManager.Instance.step == 4)
            {
                FasterButton[0].GetComponent<Image>().color = new Color32(114, 126, 233, 255);
            }
            else if (GameManager.Instance.step == 6)
            {
                FasterButton[1].GetComponent<Image>().color = new Color32(114, 126, 233, 255);
            }


        }
    }

    public void Pause()
    {
        if (pause)
        {
            pause = false;
            Time.timeScale = 1;
            if (GameManager.Instance.step == 4)
            {
                PauseButton[0].GetComponent<Image>().color = Color.white;
            }
            else if(GameManager.Instance.step == 6)
            {
                PauseButton[1].GetComponent<Image>().color = Color.white;
            }
            
        }
        else
        {
            pause = true;
            Time.timeScale = 0;
            if (GameManager.Instance.step == 4)
            {
                PauseButton[0].GetComponent<Image>().color = new Color32(114, 126, 233, 255);
            }
            else if (GameManager.Instance.step == 6)
            {
                PauseButton[1].GetComponent<Image>().color = new Color32(114, 126, 233, 255);
            }
        }
    }
}

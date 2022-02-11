using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateFlowers : MonoBehaviour
{
    public GameObject showPointsCube;
    [System.Serializable]
    public struct FlowerType
    {
        public string Name;
        public GameObject FlowerPrefab;
    }
    public List<FlowerType> Flowers;

    public void CreateFlowers(List<Vector3> flowerPoints)
    {
        //1 For creation
        //Randomly change the rotation, size, flower's type
        //foreach (var node in flowerPoints)
        //{
        //    //Type
        //    int fakeRandom = Random.Range(1, 9);
        //    GameObject Flower = Flowers[fakeRandom - 1].FlowerPrefab;
        //    //Rotation
        //    int fakeRandom_Rot = Random.Range(1, 100);
        //    //Size
        //    int fakeRandom_Size = Random.Range(25, 45);


        //    GameObject FlowerObj = Instantiate(Flower, node, Quaternion.Euler(0, fakeRandom_Rot, 0),transform);
        //    FlowerObj.transform.localScale = new Vector3(fakeRandom_Size, fakeRandom_Size, fakeRandom_Size);
        //}
        //2 For translate
        Debug.Log(SortFlower.flowers.Count);
        Debug.Log(flowerPoints.Count);
        foreach (var flower in SortFlower.flowers)
        {
            foreach (var pos in flowerPoints)
            {
                flower.transform.position = pos;
                //Debug.Log(pos);
            }
        }


    }

    public int FindIndex(string _name)
    {
        int i = 0;
        while (i < Flowers.Count)
        {
            if (Flowers[i].Name == _name)
            {
                return i;
            }
            i++;
        }
        return i;
    }

    public void createFlowersBy2DPoints(List<Vector2> points)
    {
        foreach (var node2d in points)
        {
            GameObject Obj = Instantiate(showPointsCube, new Vector3(node2d.x, 25f, node2d.y), Quaternion.Euler(0, 0, 0), transform);
        }
    }

    //
    public void GenerateVMap()
    {
        //MapGeneratorPreview.StartWhenK()
        //MapGeneratorPreview generator;
        //generator.GenerateMap();

    }
}

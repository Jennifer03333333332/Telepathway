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
        //Randomly change the rotation, size, flower's type
        foreach (var node in flowerPoints)
        {
            //Type
            int fakeRandom = Random.Range(1, 9);
            GameObject Flower = Flowers[fakeRandom - 1].FlowerPrefab;
            //Rotation
            int fakeRandom_Rot = 0;//Random.Range(1, 100);
            //Size
            int fakeRandom_Size = 1;//Random.Range(25, 45);


            GameObject FlowerObj = Instantiate(Flower, node, Quaternion.Euler(0, fakeRandom_Rot, 0),transform);
            FlowerObj.transform.localScale = new Vector3(fakeRandom_Size, fakeRandom_Size, fakeRandom_Size);
        }
    }

    public int FindSoundIndex(string _name)
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
}

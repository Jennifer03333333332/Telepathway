using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateFlowers : MonoBehaviour
{
    
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
            int fakeRandom_Rot = Random.Range(1, 100);
            //Size
            int fakeRandom_Size = Random.Range(25, 45);


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
}

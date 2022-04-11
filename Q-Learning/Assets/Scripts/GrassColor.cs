using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassColor : MonoBehaviour
{
    // Start is called before the first frame update
    public Material grassshader;
    void Start()
    {
        GetComponent<MeshRenderer>().material.CopyPropertiesFromMaterial(grassshader);
        //

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeColor(Color color)
    {
        GetComponent<MeshRenderer>().material.SetColor("_ColorTop", color); 
             //GetComponent<MeshRenderer>().material.SetColor("_ColorBottom", color);
    }
}

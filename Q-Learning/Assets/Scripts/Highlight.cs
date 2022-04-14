using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Highlight : MonoBehaviour
{
    private Material myMaterial;
    public float pulseTime = 0.8f;
    // Start is called before the first frame update
    void Start()
    {
        myMaterial = GetComponent<MeshRenderer>().material;
        StartCoroutine(Appear(myMaterial, 0.01f, pulseTime));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Transparent(Material i, float smoothness, float duration)
    {

        float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
        float increment = smoothness / duration; //The amount of change to apply.
        while (progress < 1)
        {
            Debug.Log("Getting Transparent");
            i.color = Color.Lerp(new Color(i.color.r, i.color.g, i.color.b, 1f), new Color(i.color.r, i.color.g, i.color.b, 0), progress);
            progress += increment;
            yield return new WaitForSeconds(smoothness);
        }
        StartCoroutine(Appear(myMaterial, 0.01f, pulseTime));
    }

    IEnumerator Appear(Material i, float smoothness, float duration)
    {

        float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
        float increment = smoothness / duration; //The amount of change to apply.
        while (progress < 1)
        {

            i.color = Color.Lerp(new Color(i.color.r, i.color.g, i.color.b, 0), new Color(i.color.r, i.color.g, i.color.b, 1f), progress);
            progress += increment;
            yield return new WaitForSeconds(smoothness);
        }
        StartCoroutine(Transparent(myMaterial, 0.01f, pulseTime));
    }
}

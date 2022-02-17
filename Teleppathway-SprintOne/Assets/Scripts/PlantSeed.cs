using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlantSeed : MonoBehaviour, IPointerDownHandler
{
    // Start is called before the first frame update
    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.5f;
    public GameObject seedsParent;
    public GameObject SeedPrefeb;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerDown(PointerEventData data)
    {
        clicked++;
        if (clicked == 1) clicktime = Time.time;

        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            clicked = 0;
            clicktime = 0;
            Ray ray = Camera.main.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "Garden")
                {
                    Instantiate(SeedPrefeb, new Vector3(hit.point.x, 1f, hit.point.z), Quaternion.identity, seedsParent.transform);
                }
                
            }

        }
        else if (clicked > 2 || Time.time - clicktime > 1) clicked = 0;

    }
}

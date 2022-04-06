using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class AIMove : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject AIagent;
    bool isMouseDown = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isMouseDown)
        {
            isMouseDown = true;
            Ray ray = Camera.main.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.name == "Up")
                {
                    Collider[] blockTest = Physics.OverlapBox(new Vector3(AIagent.transform.position.x , 0, AIagent.transform.position.z + 1), new Vector3(0.3f, 0.3f, 0.3f));
                    if (blockTest.Where(col => col.gameObject.tag == "wall").ToArray().Length == 0)
                    {
                        AIagent.transform.position = new Vector3(AIagent.transform.position.x , 0, AIagent.transform.position.z + 1);
                    }
                }
                else if (hit.transform.gameObject.name == "Down")
                {
                    Collider[] blockTest = Physics.OverlapBox(new Vector3(AIagent.transform.position.x , 0, AIagent.transform.position.z - 1), new Vector3(0.3f, 0.3f, 0.3f));
                    if (blockTest.Where(col => col.gameObject.tag == "wall").ToArray().Length == 0)
                    {
                        AIagent.transform.position = new Vector3(AIagent.transform.position.x , 0, AIagent.transform.position.z - 1);
                    }
                }
                else if (hit.transform.gameObject.name == "Left")
                {
                    Collider[] blockTest = Physics.OverlapBox(new Vector3(AIagent.transform.position.x - 1, 0, AIagent.transform.position.z), new Vector3(0.3f, 0.3f, 0.3f));
                    if (blockTest.Where(col => col.gameObject.tag == "wall").ToArray().Length == 0)
                    {
                        AIagent.transform.position = new Vector3(AIagent.transform.position.x - 1, 0, AIagent.transform.position.z);
                    }
                }
                else if (hit.transform.gameObject.name == "Right")
                {
                    Collider[] blockTest = Physics.OverlapBox(new Vector3(AIagent.transform.position.x + 1, 0, AIagent.transform.position.z), new Vector3(0.3f, 0.3f, 0.3f));
                    if (blockTest.Where(col => col.gameObject.tag == "wall").ToArray().Length == 0)
                    {
                        AIagent.transform.position = new Vector3(AIagent.transform.position.x + 1, 0, AIagent.transform.position.z);
                    }
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MovingPunishment : MonoBehaviour
{
    // Start is called before the first frame update
    bool down = true;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveStep()
    {
        if (down)
        {
            Collider[] blockTest = Physics.OverlapBox(new Vector3(transform.position.x + 1, 0, transform.position.z), new Vector3(0.3f, 0.3f, 0.3f));
            if (blockTest.Where(col => col.gameObject.tag == "wall").ToArray().Length == 0)
            {
                transform.position = new Vector3(transform.position.x + 1, 0, transform.position.z);
            }
            else down = false;
        }
        else
        {
            Collider[] blockTest = Physics.OverlapBox(new Vector3(transform.position.x - 1, 0, transform.position.z), new Vector3(0.3f, 0.3f, 0.3f));
            if (blockTest.Where(col => col.gameObject.tag == "wall").ToArray().Length == 0)
            {
                transform.position = new Vector3(transform.position.x - 1, 0, transform.position.z);
            }
            else down = true;
        }
    }
}

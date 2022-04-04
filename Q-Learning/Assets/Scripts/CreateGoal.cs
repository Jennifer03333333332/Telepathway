using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CreateGoal : MonoBehaviour
{
    public bool isMouseDown = false;
    private Vector3 offset;
    public GameObject PitPrefeb;
    public GameObject PitImagePrefeb;
    public GameObject AIImagePrefeb;
    public GameObject AIPrefeb;
    public GameObject GoalImagePrefeb;
    public GameObject GoalPrefeb;
    public GameObject canvas;
    GameObject MovingObject;
    int current = -1;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {

        
        
        if (Input.GetMouseButtonDown(0) && !isMouseDown)
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;
            GraphicRaycaster gr = canvas.GetComponent<GraphicRaycaster>();
            List<RaycastResult> results = new List<RaycastResult>();
            gr.Raycast(pointerEventData, results);
            if (results.Count != 0)
            {
                if (results[0].gameObject.name == "Image (2)"|| results[0].gameObject.name == "Image (1)"|| results[0].gameObject.name == "Image")
                {
                    isMouseDown = true;
                    if(results[0].gameObject.name == "Image (2)")
                    {
                        MovingObject = Instantiate(PitImagePrefeb, canvas.transform);
                        current = 2;
                    }
                    else if (results[0].gameObject.name == "Image (1)")
                    {
                        MovingObject = Instantiate(GoalImagePrefeb, canvas.transform);
                        current = 1;
                    }
                    else
                    {
                        MovingObject = Instantiate(AIImagePrefeb, canvas.transform);
                        current = 0;
                    }


                }
                
            }


        }
        
        if (Input.GetMouseButtonUp(0))
        {
            if (isMouseDown)
            {
                Destroy(MovingObject);

                Ray ray = Camera.main.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject.tag == "Plane")
                    {
                        Debug.Log(hit.transform.position);
                        offset = hit.point;
                        offset.y = 0;
                        int a = (int)offset.x;
                        int b = (int)offset.z;
                        if (offset.x > 0)
                        {
                            if (offset.x - a < 0.5f)
                            {
                                offset.x = a;
                            }
                            else
                            {
                                offset.x = a + 1;
                            }
                        }
                        else
                        {
                            if (offset.x - a > -0.5f)
                            {
                                offset.x = a;
                            }
                            else
                            {
                                offset.x = a - 1;
                            }
                        }
                        if (offset.z > 0)
                        {
                            if (offset.z - b < 0.5f)
                            {
                                offset.z = b;
                            }
                            else
                            {
                                offset.z = b + 1;
                            }
                        }
                        else
                        {
                            if (offset.z - b > -0.5f)
                            {
                                offset.z = b;
                            }
                            else
                            {
                                offset.z = b - 1;
                            }
                        }
                        if (current == 2)
                        {
                            GameObject pit = Instantiate(PitPrefeb);
                            pit.transform.position = offset;
                        }
                        else if (current == 1)
                        {
                            GameObject goal = Instantiate(GoalPrefeb);
                            goal.transform.position = offset;
                        }
                        else if (current == 0)
                        {
                            GameObject ai = Instantiate(AIPrefeb);
                            ai.transform.position = offset;
                        }
                        
                        
                    }

                }
                /*
                var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, layer1);

                if (hit && hit.transform.tag == "Floor")
                {
                    offset = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    offset.z = 0;
                    int a = (int)offset.x;
                    int b = (int)offset.y;
                    if (offset.x > 0)
                    {
                        if (offset.x - a < 0.5f)
                        {
                            offset.x = a;
                        }
                        else
                        {
                            offset.x = a + 1;
                        }
                    }
                    else
                    {
                        if (offset.x - a > -0.5f)
                        {
                            offset.x = a;
                        }
                        else
                        {
                            offset.x = a - 1;
                        }
                    }
                    if (offset.y > 0)
                    {
                        if (offset.y - b < 0.5f)
                        {
                            offset.y = b;
                        }
                        else
                        {
                            offset.y = b + 1;
                        }
                    }
                    else
                    {
                        if (offset.y - b > -0.5f)
                        {
                            offset.y = b;
                        }
                        else
                        {
                            offset.y = b - 1;
                        }
                    }
                    if (offset.x == player.transform.position.x && offset.y == player.transform.position.y)
                    {
                        transform.position = startPos;
                        GetComponent<BoxCollider2D>().enabled = true;
                        player.GetComponent<Puppet>().enabled = true;
                        isMouseDown = false;
                        return;

                    }
                    else
                    {
                        transform.position = offset;
                        lastMousePosition = transform.position;
                        player.GetComponent<Puppet>().enabled = true;
                        GetComponent<BoxCollider2D>().enabled = true;
                    }



                }
                else
                {
                    Debug.Log("11");
                    transform.position = startPos;
                    GetComponent<BoxCollider2D>().enabled = true;
                    player.GetComponent<Puppet>().enabled = true;
                    isMouseDown = false;
                    return;
                }
                */

            }
            isMouseDown = false;
            current = -1;



        }
        
        if (isMouseDown)
        {
            //player.GetComponent<Puppet>().enabled = false;
            MoveCube();

        }
        
    }

    void MoveCube()
    {
        //RectTransformUtility.WorldToScreenPoint(Camera.main, rectTransform.position)
        //offset = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //offset.z = 0;
        MovingObject.transform.position = Input.mousePosition;
        //lastMousePosition = MovingObject.transform.position;
    }
}

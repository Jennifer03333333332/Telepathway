using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public GameObject FoodImagePrefeb;
    public GameObject FoodPrefeb;
    public GameObject canvas;
    public GameObject AIagent;
    public GameObject AIImage;
    public GameObject startButton;
    public GameObject HighlightPrefeb;
    GameObject highlight;
    GameObject MovingObject;
    Vector3 agentpos;
    int current = -1;
    // Start is called before the first frame update
    void Start()
    {
        AIagent = GameObject.FindGameObjectWithTag("agent");
        if (AIagent != null)
        {
            agentpos = AIagent.transform.position;
        }
        //agentpos = AIagent.transform.position;
        highlight = Instantiate(HighlightPrefeb);
        highlight.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (AIagent == null)
        {
            if (GameObject.FindGameObjectWithTag("agent") != null)
            {
                AIagent = GameObject.FindGameObjectWithTag("agent");
                agentpos = AIagent.transform.position;
            }
        }
        
        
        if (Input.GetMouseButtonDown(0) && !isMouseDown)
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;
            GraphicRaycaster gr = canvas.GetComponent<GraphicRaycaster>();
            List<RaycastResult> results = new List<RaycastResult>();
            gr.Raycast(pointerEventData, results);
            if (results.Count != 0)
            {
                if (results[0].gameObject.name == "Image (2)"|| results[0].gameObject.name == "Image (1)"|| results[0].gameObject.name == "Image" || results[0].gameObject.name == "Image (3)")
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
                    else if (results[0].gameObject.name == "Image")
                    {
                        MovingObject = Instantiate(AIImagePrefeb, canvas.transform);
                        current = 0;
                    }
                    else
                    {
                        MovingObject = Instantiate(FoodImagePrefeb, canvas.transform);
                        current = 3;
                    }


                }
                
            }


        }
        if (isMouseDown)
        {
            Ray ray = Camera.main.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "Plane")
                {
                    highlight.SetActive(true);
                    Debug.Log(hit.transform.position);
                    offset = hit.point;
                    offset.y = -0.3f;
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
                    highlight.transform.position = offset;
                }
                else
                {
                    highlight.SetActive(false);
                }
            }
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            if (isMouseDown)
            {
                Destroy(MovingObject);
                highlight.SetActive(false);
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
                        if (current == 2 )
                        {
                            Debug.Log(offset);
                            if (AIagent==null || (offset.x != agentpos.x||offset.z!= agentpos.z))
                            {
                                
                                GameObject pit = Instantiate(PitPrefeb);
                                pit.transform.position = new Vector3(offset.x,-0.4f,offset.z);
                                if(SceneManager.GetActiveScene().name == "Level 3")
                                {
                                    GameObject.Find("GameManager").GetComponent<ChangeUI>().CountLava();
                                }
                                transform.GetComponent<GrassManager>().HideGrass((int)offset.x, (int)offset.z);
                            }
                            
                        }
                        else if (current == 1)
                        {
                            if (AIagent == null || (offset.x != agentpos.x || offset.z != agentpos.z))
                            {
                                GameObject goal = Instantiate(GoalPrefeb);
                                offset.y = -0.4f;
                                goal.transform.position = offset;
                                if (SceneManager.GetActiveScene().name == "Level 2")
                                {
                                    GameObject.Find("GameManager").GetComponent<ChangeUI>().NextStep();
                                }
                                if (SceneManager.GetActiveScene().name == "Level 5")
                                {
                                    GameObject.Find("GameManager").GetComponent<ChangeUI>().CountLava();
                                }
                                transform.GetComponent<GrassManager>().HideGrass((int)offset.x, (int)offset.z);
                            }
                                

                        }
                        else if (current == 0)
                        {
                            GameObject ai = Instantiate(AIPrefeb);
                            ai.transform.position = offset;
                            AIagent = ai;
                            AIImage.SetActive(false);
                            startButton.SetActive(true);
}
                        else if (current == 3)
                        {
                            GameObject food = Instantiate(FoodPrefeb);
                            food.transform.position = new Vector3(offset.x, -0.45f, offset.z);
                            if (SceneManager.GetActiveScene().name == "Level 4")
                            {
                                GameObject.Find("GameManager").GetComponent<ChangeUI>().CountLava();
                            }
                            transform.GetComponent<GrassManager>().HideGrass((int)offset.x, (int)offset.z);
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

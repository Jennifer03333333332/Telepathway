using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class AIMove : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject AIagent;
    bool isMouseDown = false;
    public Animator AIanimator;
    public float movetime=1f;
    void Start()
    {
        StartCoroutine(RandomWalk());
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = new Vector3(AIagent.transform.position.x, 2.5f, AIagent.transform.position.z);
        /*if (Input.GetMouseButtonDown(0) && !isMouseDown)
        {
            isMouseDown = true;
            Ray ray = Camera.main.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.name == "Up")
                {
                    AIanimator.SetInteger("Move", 1);
                    var rotationVector = AIagent.transform.rotation.eulerAngles;
                    rotationVector.y = 0;
                    
                    Collider[] blockTest = Physics.OverlapBox(new Vector3(AIagent.transform.position.x , 0, AIagent.transform.position.z + 1), new Vector3(0.3f, 0.3f, 0.3f));
                    if (blockTest.Where(col => col.gameObject.tag == "wall").ToArray().Length == 0)
                    {
                        StartCoroutine(MoveTo(AIagent.transform, new Vector3(AIagent.transform.position.x, -0.45f, AIagent.transform.position.z + 1), movetime));
                        
                        //AIagent.transform.position = new Vector3(AIagent.transform.position.x , -0.45f, AIagent.transform.position.z + 1);
                        AIagent.transform.rotation = Quaternion.Euler(rotationVector);
                    }
                }
                else if (hit.transform.gameObject.name == "Down")
                {
                    AIanimator.SetInteger("Move", 1);
                    var rotationVector = AIagent.transform.rotation.eulerAngles;
                    rotationVector.y = 180;
                    Collider[] blockTest = Physics.OverlapBox(new Vector3(AIagent.transform.position.x , 0, AIagent.transform.position.z - 1), new Vector3(0.3f, 0.3f, 0.3f));
                    if (blockTest.Where(col => col.gameObject.tag == "wall").ToArray().Length == 0)
                    {
                        StartCoroutine(MoveTo(AIagent.transform, new Vector3(AIagent.transform.position.x, -0.45f, AIagent.transform.position.z - 1), movetime));
                        
                        //AIagent.transform.position = new Vector3(AIagent.transform.position.x , -0.45f, AIagent.transform.position.z - 1);
                        AIagent.transform.rotation = Quaternion.Euler(rotationVector);
                    }
                }
                else if (hit.transform.gameObject.name == "Left")
                {
                    AIanimator.SetInteger("Move", 1);
                    var rotationVector = AIagent.transform.rotation.eulerAngles;
                    rotationVector.y = -90;
                    Collider[] blockTest = Physics.OverlapBox(new Vector3(AIagent.transform.position.x - 1, 0, AIagent.transform.position.z), new Vector3(0.3f, 0.3f, 0.3f));
                    if (blockTest.Where(col => col.gameObject.tag == "wall").ToArray().Length == 0)
                    {
                        StartCoroutine(MoveTo(AIagent.transform, new Vector3(AIagent.transform.position.x - 1, -0.45f, AIagent.transform.position.z), movetime));
                        
                        //AIagent.transform.position = new Vector3(AIagent.transform.position.x - 1, -0.45f, AIagent.transform.position.z);
                        AIagent.transform.rotation = Quaternion.Euler(rotationVector);
                    }
                }
                else if (hit.transform.gameObject.name == "Right")
                {
                    AIanimator.SetInteger("Move", 1);
                    var rotationVector = AIagent.transform.rotation.eulerAngles;
                    rotationVector.y = 90;
                    Collider[] blockTest = Physics.OverlapBox(new Vector3(AIagent.transform.position.x + 1, 0, AIagent.transform.position.z), new Vector3(0.3f, 0.3f, 0.3f));
                    if (blockTest.Where(col => col.gameObject.tag == "wall").ToArray().Length == 0)
                    {
                        StartCoroutine(MoveTo(AIagent.transform, new Vector3(AIagent.transform.position.x + 1, -0.45f, AIagent.transform.position.z), movetime));
                        
                        //AIagent.transform.position = new Vector3(AIagent.transform.position.x + 1, -0.45f, AIagent.transform.position.z);
                        AIagent.transform.rotation = Quaternion.Euler(rotationVector);
                    }
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
        }

        */
    }

    private IEnumerator MoveTo(Transform tr,Vector3 pos, float time)
    {
        float t = 0;
        Vector3 startPos = tr.position;
        while (true)
        {
            t += Time.deltaTime;
            float a = t / time;
            tr.position = Vector3.Lerp(startPos, pos, a);
            Debug.Log("1");
            if (a >= 1.0f)
            {
                AIanimator.SetInteger("Move", 0);
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(false);
                transform.GetChild(3).gameObject.SetActive(false);
                break;
            }
            yield return null;
        }
    }

    private IEnumerator RandomWalk()
    {
        while (true)
        {
            int move = Random.Range(0, 4);
            if (move == 0)
            {
                AIanimator.SetInteger("Move", 1);
                var rotationVector = AIagent.transform.rotation.eulerAngles;
                rotationVector.y = 0;

                Collider[] blockTest = Physics.OverlapBox(new Vector3(AIagent.transform.position.x, 0, AIagent.transform.position.z + 1), new Vector3(0.3f, 0.3f, 0.3f));
                if (blockTest.Where(col => col.gameObject.tag == "wall").ToArray().Length == 0)
                {
                    StartCoroutine(MoveTo(AIagent.transform, new Vector3(AIagent.transform.position.x, -0.45f, AIagent.transform.position.z + 1), movetime));
                    transform.GetChild(0).gameObject.SetActive(true);
                    //AIagent.transform.position = new Vector3(AIagent.transform.position.x , -0.45f, AIagent.transform.position.z + 1);
                    AIagent.transform.rotation = Quaternion.Euler(rotationVector);
                }
            }
            else if (move == 1)
            {
                AIanimator.SetInteger("Move", 1);
                var rotationVector = AIagent.transform.rotation.eulerAngles;
                rotationVector.y = 180;
                Collider[] blockTest = Physics.OverlapBox(new Vector3(AIagent.transform.position.x, 0, AIagent.transform.position.z - 1), new Vector3(0.3f, 0.3f, 0.3f));
                if (blockTest.Where(col => col.gameObject.tag == "wall").ToArray().Length == 0)
                {
                    StartCoroutine(MoveTo(AIagent.transform, new Vector3(AIagent.transform.position.x, -0.45f, AIagent.transform.position.z - 1), movetime));
                    transform.GetChild(1).gameObject.SetActive(true);
                    //AIagent.transform.position = new Vector3(AIagent.transform.position.x , -0.45f, AIagent.transform.position.z - 1);
                    AIagent.transform.rotation = Quaternion.Euler(rotationVector);
                }
            }
            else if (move == 2)
            {
                AIanimator.SetInteger("Move", 1);
                var rotationVector = AIagent.transform.rotation.eulerAngles;
                rotationVector.y = -90;
                Collider[] blockTest = Physics.OverlapBox(new Vector3(AIagent.transform.position.x - 1, 0, AIagent.transform.position.z), new Vector3(0.3f, 0.3f, 0.3f));
                if (blockTest.Where(col => col.gameObject.tag == "wall").ToArray().Length == 0)
                {
                    StartCoroutine(MoveTo(AIagent.transform, new Vector3(AIagent.transform.position.x - 1, -0.45f, AIagent.transform.position.z), movetime));
                    transform.GetChild(2).gameObject.SetActive(true);
                    //AIagent.transform.position = new Vector3(AIagent.transform.position.x - 1, -0.45f, AIagent.transform.position.z);
                    AIagent.transform.rotation = Quaternion.Euler(rotationVector);
                }
            }
            else if (move == 3)
            {
                AIanimator.SetInteger("Move", 1);
                var rotationVector = AIagent.transform.rotation.eulerAngles;
                rotationVector.y = 90;
                Collider[] blockTest = Physics.OverlapBox(new Vector3(AIagent.transform.position.x + 1, 0, AIagent.transform.position.z), new Vector3(0.3f, 0.3f, 0.3f));
                if (blockTest.Where(col => col.gameObject.tag == "wall").ToArray().Length == 0)
                {
                    StartCoroutine(MoveTo(AIagent.transform, new Vector3(AIagent.transform.position.x + 1, -0.45f, AIagent.transform.position.z), movetime));
                    transform.GetChild(3).gameObject.SetActive(true);
                    //AIagent.transform.position = new Vector3(AIagent.transform.position.x + 1, -0.45f, AIagent.transform.position.z);
                    AIagent.transform.rotation = Quaternion.Euler(rotationVector);
                }
            }
            yield return new WaitForSeconds(movetime);
        }
        
    }
}

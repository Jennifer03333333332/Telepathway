using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using UnityEngine.SceneManagement;

public class GridEnvironment : Environment
{

    public List<GameObject> actorObjs;
    public string[] players;
    public GameObject visualAgent;
    public GameObject foodprefeb;
    int numObstacles;
    int numGoals;
    int gridSize;
    int[] objectPositions;
    float episodeReward;
    Vector3 agentPos;
    Vector3[] foodPos = new Vector3[3];
    public Animator AIanimator;
    public GameObject StartButton;
    public GameObject ResetButton;
    public GameObject QuitButton;
    public GameObject correct;
    public GameObject wrong;
    public int trainingtimes;
    public Text trainingtimeUI;
    bool firsteat = true;
    bool firstkill = true;
    void Start()
    {
        maxSteps = 100;
        waitTime = 0.001f;
        trainingtimes = 0;
        //BeginNewGame();
    }

    /// <summary>
    /// Restarts the learning process with a new Grid.
    /// </summary>
    public void BeginNewGame()
    {
        //int gridSizeSet = (GameObject.Find("Dropdown").GetComponent<Dropdown>().value + 1) * 5;
        //numGoals = 1;
        //numObstacles = Mathf.FloorToInt((gridSizeSet * gridSizeSet) / 10f);
        
        int gridSizeSet = 10;
        gridSize = gridSizeSet;
        //Debug.Log(gridSizeSet);
        actorObjs[0] = GameObject.Find("agent(Clone)");
        if (SceneManager.GetActiveScene().name == "Level 6")
        {
            StartButton.SetActive(false);
            ResetButton.SetActive(true);
            QuitButton.SetActive(true);
            
        }
        agentPos = GameObject.Find("agent(Clone)").transform.position;
        if (GameObject.FindGameObjectsWithTag("food").Length > 0)
        {
            //foodPos[0] = GameObject.FindGameObjectsWithTag("food")[0].transform.position;
            //foodPos[1] = GameObject.FindGameObjectsWithTag("food")[1].transform.position;
            //foodPos[2] = GameObject.FindGameObjectsWithTag("food")[2].transform.position;
        }
        
        foreach (GameObject actor in actorObjs)
        {
            //DestroyImmediate(actor);
        }

        SetUp();
        agent = new InternalAgent();
        agent.SendParameters(envParameters);
        if (AIanimator == null)
        {
            AIanimator = actorObjs[0].GetComponent<Animator>();
        }
        AIanimator.SetInteger("Move", 1);
        Reset();

    }

    /// <summary>
    /// Established the Grid.
    /// </summary>
    public override void SetUp()
    {
        envParameters = new EnvironmentParameters()
        {
            observation_size = 0,
            state_size = gridSize * gridSize,
            action_descriptions = new List<string>() { "Up", "Down", "Left", "Right" },
            action_size = 4,
            env_name = "GridWorld",
            action_space_type = "discrete",
            state_space_type = "discrete",
            num_agents = 1
        };

        //List<string> playersList = new List<string>();
        //actorObjs = new List<GameObject>();
        /*
        for (int i = 0; i < numObstacles; i++)
        {
            playersList.Add("pit");
        }
        playersList.Add("agent");

        for (int i = 0; i < numGoals; i++)
        {
            playersList.Add("goal");
        }
        */
        //players = playersList.ToArray();
       // Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        //cam.transform.position = new Vector3((gridSize - 1), gridSize, -(gridSize - 1) / 2f);
        //cam.orthographicSize = (gridSize + 5f) / 2f;
        SetEnvironment();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Slider").GetComponent<Slider>().value == 0)
        {
            waitTime = 0.3f;
        }
        else if(GameObject.Find("Slider").GetComponent<Slider>().value == 1)
        {
            waitTime = 0.1f;
        }
        else if(GameObject.Find("Slider").GetComponent<Slider>().value == 2)
        {
            waitTime = 0.001f;
        }
        //waitTime = 1.0f - GameObject.Find("Slider").GetComponent<Slider>().value;
        if (run)
        {
            RunMdp();
        }
        
    }

    /// <summary>
    /// Gets the agent's current position and transforms it into a discrete integer state.
    /// </summary>
    /// <returns>The state.</returns>
    public override List<float> collectState()
    {
        List<float> state = new List<float>();
        foreach (GameObject actor in actorObjs)
        {
            if (actor.tag == "agent")
            {
                float point = (gridSize * actor.transform.position.x) + actor.transform.position.z;
                state.Add(point);
            }
        }
        return state;
    }

    /// <summary>
    /// Resizes the grid to the specified size.
    /// </summary>
    public void SetEnvironment()
    {
        /*
        GameObject.Find("Plane").transform.localScale = new Vector3(gridSize / 10.0f, 1f, gridSize / 10.0f);
        GameObject.Find("Plane").transform.position = new Vector3((gridSize - 1) / 2f, -0.5f, (gridSize - 1) / 2f);
        GameObject.Find("sN").transform.localScale = new Vector3(1, 1, gridSize + 2);
        GameObject.Find("sS").transform.localScale = new Vector3(1, 1, gridSize + 2);
        GameObject.Find("sN").transform.position = new Vector3((gridSize - 1) / 2f, 0.0f, gridSize);
        GameObject.Find("sS").transform.position = new Vector3((gridSize - 1) / 2f, 0.0f, -1);
        GameObject.Find("sE").transform.localScale = new Vector3(1, 1, gridSize + 2);
        GameObject.Find("sW").transform.localScale = new Vector3(1, 1, gridSize + 2);
        GameObject.Find("sE").transform.position = new Vector3(gridSize, 0.0f, (gridSize - 1) / 2f);
        GameObject.Find("sW").transform.position = new Vector3(-1, 0.0f, (gridSize - 1) / 2f);
        
        HashSet<int> numbers = new HashSet<int>();
        while (numbers.Count < players.Length)
        {
            numbers.Add(Random.Range(0, gridSize * gridSize));
        }
        objectPositions = numbers.ToArray();
        */
    }

    /// <summary>
    /// Draws the value estimation spheres on the grid.
    /// </summary>
    public void LoadSpheres()
    {
        GameObject[] values = GameObject.FindGameObjectsWithTag("value");
        foreach (GameObject value in values)
        {
            Destroy(value);
        }

        float[] value_estimates = agent.GetValue();
        for (int i = 0; i < gridSize * gridSize; i++)
        {
           // GameObject value = (GameObject)GameObject.Instantiate(Resources.Load("value"));
            int x = i / gridSize;
            int y = i % gridSize;
           // value.transform.position = new Vector3(x, 0.0f, y);
            //value.transform.localScale = new Vector3(value_estimates[i] / 1.25f, value_estimates[i] / 1.25f, value_estimates[i] / 1.25f);
            //Debug.Log(value_estimates[i]);
            if(value_estimates[i] > 0)
            {
                transform.GetComponent<GrassManager>().ChangeColor(x, y, new Color32((byte)((255 - (225 * value_estimates[i]*1.5))), 255,0,255));
                Debug.Log((255 - (225 * value_estimates[i])));
                //transform.GetComponent<GrassManager>().ChangeColor(x, y, Color.green);

            }
            
            if (value_estimates[i] < 0)
            {
               // Material newMat = Resources.Load("negative_mat", typeof(Material)) as Material;
               // value.GetComponent<Renderer>().material = newMat;
                transform.GetComponent<GrassManager>().ChangeColor(x, y, new Color32(255, (byte)((255 + (225 * value_estimates[i]*1.5))),0,255));
            }

            if (value_estimates[i] == 0)
            {
                // Material newMat = Resources.Load("negative_mat", typeof(Material)) as Material;
                // value.GetComponent<Renderer>().material = newMat;
                transform.GetComponent<GrassManager>().ChangeColor(x, y, new Color32(255, 255 , 0, 255));
            }
        }
    }

    /// <summary>
    /// Resets the episode by placing the objects in their original positions.
    /// </summary>
    public override void Reset()
    {
        base.Reset();
        trainingtimes++;
        if (trainingtimeUI != null)
        {
            trainingtimeUI.text = "Iteration: " + trainingtimes;
        }
        
        foreach (GameObject actor in GameObject.FindGameObjectsWithTag("food"))
        {
            //Destroy(actor);
        }
        //actorObjs = new List<GameObject>();

        for (int i = 0; i < players.Length; i++)
        {
            //int x = (objectPositions[i]) / gridSize;
            //int y = (objectPositions[i]) % gridSize;
            //GameObject actorObj = (GameObject)GameObject.Instantiate(Resources.Load(players[i]));
            //actorObj.transform.position = new Vector3(x, 0.0f, y);
            //actorObj.name = players[i];
            //actorObjs.Add(actorObj);
            if (players[i] == "agent")
            {
                //visualAgent = actorObj;
                actorObjs[i].transform.position = agentPos;
                visualAgent = actorObjs[i];
            }
            if (players[i] == "mp")
            {
                actorObjs[i].transform.position = new Vector3(0,0,4);
                
            }
            if (players[i] == "food")
            {
                //GameObject actorObj = Instantiate(foodprefeb);
                //foodprefeb.transform.position = foodPos[i-2];

            }
        }
        episodeReward = 0;
        EndReset();
    }

    private IEnumerator MoveTo(Transform tr, Vector3 pos, float time)
    {
        float t = 0;
        Vector3 startPos = tr.position;
        while (true)
        {
            t += Time.deltaTime;
            float a = t / time;
            tr.position = Vector3.Lerp(startPos, pos, a);
            //Debug.Log("1");
            if (a >= 1.0f)
            {
                //AIanimator.SetInteger("Move", 0);
                break;
            }
            yield return null;
        }
    }

    /// <summary>
    /// Allows the agent to take actions, and set rewards accordingly.
    /// </summary>
    /// <param name="action">Action.</param>
    public override void MiddleStep(int action)
    {
        reward = -0.05f;
        float time = waitTime;
        // 0 - Forward, 1 - Backward, 2 - Left, 3 - Right
        if (action == 3)
        {
            var rotationVector = visualAgent.transform.rotation.eulerAngles;
            rotationVector.y = 90;
            Collider[] blockTest = Physics.OverlapBox(new Vector3(visualAgent.transform.position.x + 1, 0, visualAgent.transform.position.z), new Vector3(0.3f, 0.3f, 0.3f));
            if (blockTest.Where(col => col.gameObject.tag == "wall").ToArray().Length == 0)
            {
                StartCoroutine(MoveTo(visualAgent.transform, new Vector3(visualAgent.transform.position.x+1, -0.45f, visualAgent.transform.position.z), time));
                //visualAgent.transform.position = new Vector3(visualAgent.transform.position.x + 1, 0, visualAgent.transform.position.z);
                visualAgent.transform.rotation = Quaternion.Euler(rotationVector);
            }
        }

        if (action == 2)
        {
            var rotationVector = visualAgent.transform.rotation.eulerAngles;
            rotationVector.y = -90;
            Collider[] blockTest = Physics.OverlapBox(new Vector3(visualAgent.transform.position.x - 1, 0, visualAgent.transform.position.z), new Vector3(0.3f, 0.3f, 0.3f));
            if (blockTest.Where(col => col.gameObject.tag == "wall").ToArray().Length == 0)
            {
                StartCoroutine(MoveTo(visualAgent.transform, new Vector3(visualAgent.transform.position.x - 1, -0.45f, visualAgent.transform.position.z), time));
                //visualAgent.transform.position = new Vector3(visualAgent.transform.position.x - 1, 0, visualAgent.transform.position.z);
                visualAgent.transform.rotation = Quaternion.Euler(rotationVector);
            }
        }

        if (action == 0)
        {
            var rotationVector = visualAgent.transform.rotation.eulerAngles;
            rotationVector.y = 0;
            Collider[] blockTest = Physics.OverlapBox(new Vector3(visualAgent.transform.position.x, 0, visualAgent.transform.position.z + 1), new Vector3(0.3f, 0.3f, 0.3f));
            if (blockTest.Where(col => col.gameObject.tag == "wall").ToArray().Length == 0)
            {
                StartCoroutine(MoveTo(visualAgent.transform, new Vector3(visualAgent.transform.position.x, -0.45f, visualAgent.transform.position.z+1), time));
                //visualAgent.transform.position = new Vector3(visualAgent.transform.position.x, 0, visualAgent.transform.position.z + 1);
                visualAgent.transform.rotation = Quaternion.Euler(rotationVector);
            }
        }

        if (action == 1)
        {
            var rotationVector = visualAgent.transform.rotation.eulerAngles;
            rotationVector.y = 180;
            Collider[] blockTest = Physics.OverlapBox(new Vector3(visualAgent.transform.position.x, 0, visualAgent.transform.position.z - 1), new Vector3(0.3f, 0.3f, 0.3f));
            if (blockTest.Where(col => col.gameObject.tag == "wall").ToArray().Length == 0)
            {
                StartCoroutine(MoveTo(visualAgent.transform, new Vector3(visualAgent.transform.position.x, -0.45f, visualAgent.transform.position.z-1), time));
                //visualAgent.transform.position = new Vector3(visualAgent.transform.position.x, 0, visualAgent.transform.position.z - 1);
                visualAgent.transform.rotation = Quaternion.Euler(rotationVector);
            }
        }
        StartCoroutine(CheckGoal(time,action));
        
        //GameObject.Find("RTxt").GetComponent<Text>().text = "Episode Reward: " + episodeReward.ToString("F2");

    }

    IEnumerator CheckGoal(float time,int action)
    {
        yield return new WaitForSeconds(time);
        Collider[] hitObjects = Physics.OverlapBox(visualAgent.transform.position, new Vector3(0.3f, 0.3f, 0.3f));
        if (hitObjects.Where(col => col.gameObject.tag == "goal").ToArray().Length == 1)
        {
            reward = 1;
            done = true;
            GameObject c = Instantiate(correct);
            c.transform.position = hitObjects.Where(col => col.gameObject.tag == "goal").ToArray()[0].transform.position;
            if(GameObject.Find("Slider").GetComponent<Slider>().value == 0||firsteat){
                if (action == 0)
                {
                    visualAgent.transform.position = new Vector3(visualAgent.transform.position.x, 0, visualAgent.transform.position.z - 1);
                }
                else if (action == 1)
                {
                    visualAgent.transform.position = new Vector3(visualAgent.transform.position.x, 0, visualAgent.transform.position.z + 1);
                }
                else if (action == 2)
                {
                    visualAgent.transform.position = new Vector3(visualAgent.transform.position.x + 1, 0, visualAgent.transform.position.z);
                }
                else if (action == 3)
                {
                    visualAgent.transform.position = new Vector3(visualAgent.transform.position.x - 1, 0, visualAgent.transform.position.z);
                }
                firsteat = false;
                playeatanimation = true;
            }
            
        }
        if (hitObjects.Where(col => col.gameObject.tag == "pit").ToArray().Length == 1)
        {
            reward = -1;
            done = true;
            GameObject c = Instantiate(wrong);
            c.transform.position = hitObjects.Where(col => col.gameObject.tag == "pit").ToArray()[0].transform.position;
            if (GameObject.Find("Slider").GetComponent<Slider>().value == 0 || firstkill)
            {
                firstkill = false;
                playdieanimation = true;
            }
        }
        if (hitObjects.Where(col => col.gameObject.tag == "food").ToArray().Length == 1)
        {
            reward = -0.5f;
            done = false;
            //Destroy((hitObjects.Where(col => col.gameObject.tag == "food").ToArray()[0].gameObject));
            //destory(hitObjects.Where(col => col.gameObject.tag == "food").ToArray()[0].gameObject);
        }
        if (hitObjects.Where(col => col.gameObject.tag == "largegoal").ToArray().Length == 1)
        {
            reward = 10f;
            done = true;
            //Destroy((hitObjects.Where(col => col.gameObject.tag == "food").ToArray()[0].gameObject));
            //destory(hitObjects.Where(col => col.gameObject.tag == "food").ToArray()[0].gameObject);
        }

        LoadSpheres();
        episodeReward += reward;
    }



}

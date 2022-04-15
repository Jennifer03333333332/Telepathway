using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnvironmentParameters {
	public int state_size { get; set; }
	public int action_size { get; set; } 
	public int observation_size { get; set; }
	public List<string> action_descriptions { get; set; }
	public string env_name { get; set; }
	public string action_space_type { get; set; }
	public string state_space_type { get; set; }
	public int num_agents { get; set; }
}

public abstract class Environment : MonoBehaviour {
	public float reward;
	public bool done;
	public int maxSteps;
	public int currentStep;
	public bool begun;
	public bool acceptingSteps;

	public Agent agent;
	public int comPort;
	public int frameToSkip;
	public int framesSinceAction;
	public string currentPythonCommand;
	public bool skippingFrames;
	public float[] actions;
	public float waitTime;
	public int episodeCount;
	public bool humanControl;
	public bool playeatanimation=false;
	public bool playdieanimation = false;
	public bool run = true;
	public int bumper;

	public EnvironmentParameters envParameters;

	public MovingPunishment mp;
    public static readonly string NewLine;

    public virtual void SetUp () {
		envParameters = new EnvironmentParameters()
		{
			observation_size = 0,
			state_size = 0,
			action_descriptions = new List<string>(),
			action_size = 0,
			env_name = "Null",
			action_space_type = "discrete",
			state_space_type = "discrete",
			num_agents = 2
		};
		begun = false;
		acceptingSteps = true;
	}

	// Update is called once per frame
	void Update () {
		
	}

	public virtual List<float> collectState() {
		List<float> state = new List<float> ();
		return state;
	}
		
	public virtual void Step() {
		acceptingSteps = false;
		currentStep += 1;
		if (currentStep >= maxSteps) {
			done = true;
		}

		reward = 0;
		actions = agent.GetAction ();
		framesSinceAction = 0;

		int sendAction = Mathf.FloorToInt(actions [0]);
        if (mp != null)
        {
			mp.MoveStep();
		}
		
		MiddleStep (sendAction);
		

		StartCoroutine (WaitStep ());
	}

	public virtual void MiddleStep(int action) {

	}

	public virtual void MiddleStep(float[] action) {

	}

	public IEnumerator WaitStep() {
		yield return new WaitForSeconds (waitTime);
		EndStep ();
	}

	public virtual void EndStep() {
		agent.SendState (collectState(), reward, done);
		skippingFrames = false;
		acceptingSteps = true;
	}

	public virtual void Reset() {
		reward = 0;
		currentStep = 0;
		episodeCount++;
		done = false;
		acceptingSteps = false;
	}

	public virtual void EndReset() {
		agent.SendState (collectState(), reward, done);
		skippingFrames = false;
		acceptingSteps = true;
		begun = true;
		framesSinceAction = 0;
	}

	public virtual void RunMdp() {
		
		
		if (acceptingSteps == true) {
			if (done == false) {
				Step ();
			} 
			else if(done==true){
                if (!playeatanimation&&!playdieanimation)
                {
					Reset();
				}
                else if(playeatanimation)
                {
					Debug.Log("11");
					run = false;
					StartCoroutine(EatAnimation());
					
                }
				else if (playdieanimation)
                {
					run = false;
					StartCoroutine(DieAnimation());
				}
				
			}
		}
	}

	IEnumerator EatAnimation()
    {

		if (SoundMgr.Instance != null)
		{
			
			SoundMgr.Instance.PlaySound(5);
			SoundMgr.Instance.PlaySound(7);
			
		}
		//GameObject.FindGameObjectWithTag("agent").GetComponent<Animator>().SetInteger("Move", 0);
		//yield return new WaitForSeconds(2f);
		GameObject.FindGameObjectWithTag("agent").GetComponent<Animator>().SetTrigger("Eat");
			GameObject.FindGameObjectWithTag("agent").GetComponent<Animator>().SetInteger("Move", 0);
			//yield return new WaitForSeconds(1f);
			//GameObject.FindGameObjectWithTag("agent").GetComponent<Animator>().SetInteger("Idle", 0);
			yield return new WaitForSeconds(2f);
		if (SoundMgr.Instance != null)
		{

			
			SoundMgr.Instance.PlaySound(7);

		}
		yield return new WaitForSeconds(2f);

		GameObject.FindGameObjectWithTag("agent").GetComponent<Animator>().SetInteger("Move", 1);
		
		playeatanimation = false;
		Reset();
		run = true;
    }

	IEnumerator DieAnimation()
	{
        if (SoundMgr.Instance != null)
        {
			SoundMgr.Instance.PlayDieSound();
			SoundMgr.Instance.PlaySound(6);
		}
		
		//GameObject.FindGameObjectWithTag("agent").GetComponent<Animator>().SetInteger("Move", 0);
		//yield return new WaitForSeconds(2f);
		GameObject.FindGameObjectWithTag("agent").GetComponent<Animator>().SetTrigger("Die");
		GameObject.FindGameObjectWithTag("agent").GetComponent<Animator>().SetInteger("Move", 0);
		//yield return new WaitForSeconds(1f);
		//GameObject.FindGameObjectWithTag("agent").GetComponent<Animator>().SetInteger("Idle", 0);
		yield return new WaitForSeconds(4f);

		GameObject.FindGameObjectWithTag("agent").GetComponent<Animator>().SetInteger("Move", 1);

		playdieanimation = false;
		Reset();
		run = true;
	}
}

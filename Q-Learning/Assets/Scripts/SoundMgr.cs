using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class SoundMgr : MonoBehaviour
{
    [Header("Audio List")]
    [SerializeField] private List<AudioClip> soundList;
    [SerializeField] private List<AudioClip> bgmList;
    [SerializeField] private List<AudioClip> dialoguesList;
    [SerializeField] public static SoundMgr Instance = null;
    [SerializeField] private int dialogueIndex = 0;

    [Header("FadeIn Configs")]
    [SerializeField] private float smoothness = 0.5f;
    [SerializeField] private float increase = 0.2f;
    [SerializeField] private float maxVolume = 1.0f;


    private AudioSource audioSource;

    public class AudioBuffer
    {
        public string name;
        public float time;
    }
    public float bufferTime;
    public List<AudioBuffer> audioBuffers = new List<AudioBuffer>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        //DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {


        if (SceneManager.GetActiveScene().name == "Level 0")
        {
            PlayBGM(0);
            audioSource.loop = true;
        }
       



    }

    void Update()
    {
        if (audioBuffers.Capacity > 0)
        {
            foreach (AudioBuffer b in audioBuffers)
            {
                b.time -= Time.deltaTime;
            }
        }
    }

    public void PlaySound(int clipIndex)
    {
        PlayClip(soundList[clipIndex]);
        //audioSource.PlayOneShot(soundList[clipIndex]);

    }

    public void PlayDieSound()
    {
        int sound = Random.Range(0, 3);
        PlayClip(soundList[sound]);
        //audioSource.PlayOneShot(soundList[clipIndex]);

    }

    public float PlayDialogue(int times)
    {
        if (dialogueIndex == dialoguesList.Count)
        {
            Debug.LogWarning("dialogue index exceeds range!");
            return 0f;
        }
        StartCoroutine(PlayDialogIE(times));
        float waittime = 0;
        int temp = dialogueIndex;
        for (int i = 0; i < times; i++)
        {
            if (temp == dialoguesList.Count)
            {
                return waittime;
            }
            waittime += dialoguesList[temp].length;
            temp++;


        }
        return waittime;
    }

    public void PlayBGM(int clipIndex)
    {
        audioSource.clip = bgmList[clipIndex];
        audioSource.Play();
    }

    public void StopBGM()
    {
        audioSource.Stop();
    }



    IEnumerator PlayDialogIE(int times)
    {
        for (int i = 0; i < times; i++)
        {
            audioSource.PlayOneShot(dialoguesList[dialogueIndex]);
            yield return new WaitForSeconds(dialoguesList[dialogueIndex].length);
            dialogueIndex++;

        }

    }

    public void PlayClip(AudioClip clip, float delay = 0)
    {
        if (clip == null)
        {
            Debug.Log("Null Clip");
            return;
        }

        if (audioBuffers.Capacity > 0)
        {
            foreach (AudioBuffer b in audioBuffers)
            {
                if (clip.name == b.name && b.time > 0)
                {
                    return;
                }
                else if (clip.name == b.name)
                {
                    b.time = bufferTime;
                }
            }
        }

        if (delay > 0)
        {
            audioSource.clip = clip;
            audioSource.PlayDelayed(delay);
        }
        else
        {
            audioSource.PlayOneShot(clip);
        }

        AudioBuffer buffer = new AudioBuffer();
        buffer.name = clip.name;
        buffer.time = bufferTime;
        audioBuffers.Add(buffer);
    }
}
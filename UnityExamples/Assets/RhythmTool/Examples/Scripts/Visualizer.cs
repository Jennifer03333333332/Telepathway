using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UltrahapticsCoreAsset;
using System;
namespace RhythmTool.Examples
{
    public class Visualizer : MonoBehaviour
    {
        public RhythmAnalyzer analyzer;
        public RhythmPlayer player;
        public RhythmEventProvider eventProvider;

        public Text textBPM;
        public Text textNote;

        public Line linePrefab;
        public Line BeatPrefeb;
        public Line AccentPrefeb;
        public GameObject BlockParent;
        public GameObject BeatsParent;
        public GameObject NotesParent;
        public GameObject AccentParent;
        public SensationSource ss;
        bool showbeat=true, shownote=true, showstrength=true;
        public bool showstrengthtimer = true;

        private List<Line> lines;

        private List<Chroma> chromaFeatures;

        private Note lastNote = Note.FSHARP;
        public Leap.Controller _leap;
        GameObject lefthand;
        GameObject righthand;
        GameObject handcontroller;

        void Awake()
        {           
            analyzer.Initialized += OnInitialized;
            player.Reset += OnReset;

            eventProvider.Register<Beat>(OnBeat);
            eventProvider.Register<Onset>(OnOnset);
            eventProvider.Register<Value>(OnSegment, "Segments");

            lines = new List<Line>();
            textNote.text = "()";
            chromaFeatures = new List<Chroma>();
            handcontroller = GameObject.Find("LeapOutlineHandController");
            Debug.Log(handcontroller);
           
            //lefthand = handcontroller.transform.GetChild(0).gameObject;
            //righthand = handcontroller.transform.GetChild(1).gameObject;

        }
        private void Start()
        {
            
        }

        void Update()
        {
            if (!player.isPlaying)
                return;

            if (handcontroller == null)
            {
                handcontroller = GameObject.Find("LeapOutlineHandController");
                lefthand = handcontroller.transform.GetChild(0).gameObject;
                righthand = handcontroller.transform.GetChild(1).gameObject;
            }
            
            UpdateLines();
            
        }
        
        private void UpdateLines()
        {
            float time = player.time;
         
            //Destroy all lines with a timestamp less than the current playback time.
            List<Line> toRemove = new List<Line>();
            foreach (Line line in lines)
            {
                if (line.timestamp < time || line.timestamp > time + eventProvider.offset)
                {
                    Destroy(line.gameObject);
                    
                    if (line.tag == 0 && !(ss.SensationBlock == "Open" && ss.Running == true)&&showbeat)
                    {
                        StartCoroutine(BeatSensation(0.2f));
                        
                    }
                    else if(line.tag == 1)
                    {
                        if (line.strength >= 5 && !(ss.SensationBlock == "Open" && ss.Running == true)&&showstrength&&showstrengthtimer)
                        {
                            showstrengthtimer = false;
                            StartCoroutine(StrengthSensation(0.5f));
                        }
                        else if(!(ss.SensationBlock == "Open" && ss.Running == true)&&shownote)
                        {
                            StartCoroutine(NoteSensation(0.2f, line.note));
                        }
                        
                        textNote.text = "(" + line.note + ")";
                    }
                    
                    toRemove.Add(line);
                }
            }

            foreach (Line line in toRemove)
                lines.Remove(line);

            //Update all Line positions based on their timestamp and current playback time, 
            //so they will move as the song plays.
            foreach (Line line in lines)
            {
                UnityEngine.Vector3 position = line.transform.position;

               
                
                if (line.tag == 0 || (line.tag==1&&line.strength>=5))
                {
                    GameObject Hand = GameObject.Find("HandContainer");
                    if (Hand != null)
                    {
                        if (Hand.activeSelf)
                        {
                            position.x = Hand.transform.position.x;
                            position.z = Hand.transform.position.z;
                            position.y = (-(line.timestamp - time)) + Hand.transform.position.y;
                        }
                    }
                    //position.y = (-(line.timestamp - time))+Hand.transform.position.y;

                }
                else
                {
                    
                    GameObject FirstFinger;
                    GameObject SecondFinger;
                    GameObject ThirdFinger;
                    GameObject FourthFinger;
                    GameObject FifthFinger;
                    Debug.Log(righthand);
                    Debug.Log(lefthand);
                    
                    if (righthand!=null&&righthand.activeSelf)
                    {
                        FirstFinger = GameObject.Find("Bip01 R Finger0Nub");
                        SecondFinger = GameObject.Find("Bip01 R Finger1Nub");
                        ThirdFinger = GameObject.Find("Bip01 R Finger2Nub");
                        FourthFinger = GameObject.Find("Bip01 R Finger3Nub");
                        FifthFinger = GameObject.Find("Bip01 R Finger4Nub");
                        if (line.note == Note.A || line.note == Note.ASharp || line.note == Note.B)
                        {
                            position.x = FirstFinger.transform.position.x;
                            position.z = FirstFinger.transform.position.z;
                            position.y = (-(line.timestamp - time)) + FirstFinger.transform.position.y;
                        }
                        else if (line.note == Note.C || line.note == Note.CSHARP)
                        {
                            position.x = SecondFinger.transform.position.x;
                            position.z = SecondFinger.transform.position.z;
                            position.y = (-(line.timestamp - time)) + SecondFinger.transform.position.y;
                        }
                        else if (line.note == Note.D || line.note == Note.DSHARP)
                        {
                            position.x = ThirdFinger.transform.position.x;
                            position.z = ThirdFinger.transform.position.z;
                            position.y = (-(line.timestamp - time)) + ThirdFinger.transform.position.y;
                        }
                        else if (line.note == Note.E || line.note == Note.F || line.note == Note.FSHARP)
                        {
                            position.x = FourthFinger.transform.position.x;
                            position.z = FourthFinger.transform.position.z;
                            position.y = (-(line.timestamp - time)) + FourthFinger.transform.position.y;

                        }
                        else if (line.note == Note.G || line.note == Note.GSHARP)
                        {
                            position.x = FifthFinger.transform.position.x;
                            position.z = FifthFinger.transform.position.z;
                            position.y = (-(line.timestamp - time)) + FifthFinger.transform.position.y;
                        }

                        
                    }
                    else if (lefthand!=null&&lefthand.activeSelf)
                    {
                        FirstFinger = GameObject.Find("Bip01 R Finger0Nub001");
                        SecondFinger = GameObject.Find("Bip01 R Finger1Nub001");
                        ThirdFinger = GameObject.Find("Bip01 R Finger2Nub001");
                        FourthFinger = GameObject.Find("Bip01 R Finger3Nub001");
                        FifthFinger = GameObject.Find("Bip01 R Finger4Nub001");
                        if (line.note == Note.A || line.note == Note.ASharp || line.note == Note.B)
                        {
                            position.x = FirstFinger.transform.position.x;
                            position.z = FirstFinger.transform.position.z;
                            position.y = (-(line.timestamp - time)) + FirstFinger.transform.position.y;
                        }
                        else if (line.note == Note.C || line.note == Note.CSHARP)
                        {
                            position.x = SecondFinger.transform.position.x;
                            position.z = SecondFinger.transform.position.z;
                            position.y = (-(line.timestamp - time)) + SecondFinger.transform.position.y;
                        }
                        else if (line.note == Note.D || line.note == Note.DSHARP)
                        {
                            position.x = ThirdFinger.transform.position.x;
                            position.z = ThirdFinger.transform.position.z;
                            position.y = (-(line.timestamp - time)) + ThirdFinger.transform.position.y;
                        }
                        else if (line.note == Note.E || line.note == Note.F || line.note == Note.FSHARP)
                        {
                            position.x = FourthFinger.transform.position.x;
                            position.z = FourthFinger.transform.position.z;
                            position.y = (-(line.timestamp - time)) + FourthFinger.transform.position.y;

                        }
                        else if (line.note == Note.G || line.note == Note.GSHARP)
                        {
                            position.x = FifthFinger.transform.position.x;
                            position.z = FifthFinger.transform.position.z;
                            position.y = (-(line.timestamp - time)) + FifthFinger.transform.position.y;
                        }

                        
                    }
                    
                    
                  
                    
                }
                

                line.transform.position = position;
            }
        }
                
        private void OnInitialized(RhythmData rhythmData)
        {
            //Start playing the song.
            player.Play();
        }

        private void OnReset()
        {
            //Destroy all lines when playback is reset.
            foreach (Line line in lines)
                Destroy(line.gameObject);

            lines.Clear();
        }

        private void OnBeat(Beat beat)
        {
            //Instantiate a line to represent the Beat.
            CreateLine(beat.timestamp, 0, 1, Color.black, 1,0,Note.None,-1f);

            //Update BPM text.
            float bpm = Mathf.Round(beat.bpm * 10) / 10;
            textBPM.text = "(" + bpm + " BPM)";
        }

        private void OnOnset(Onset onset)
        {
            //Clear any previous Chroma features.
            chromaFeatures.Clear();

            //Find Chroma features that intersect the Onset's timestamp.
            player.rhythmData.GetIntersectingFeatures(chromaFeatures, onset.timestamp, onset.timestamp);

            //Instantiate a line to represent the Onset and Chroma feature.
            foreach (Chroma chroma in chromaFeatures)
                CreateLine(onset.timestamp, -2 + (float)chroma.note * .1f, .2f, Color.blue, onset.strength / 10,1,chroma.note,onset.strength);
            

            if (chromaFeatures.Count > 0)
                lastNote = chromaFeatures[chromaFeatures.Count - 1].note;

            //If no Chroma Feature was found, use the last known Chroma feature's note.
            if (chromaFeatures.Count == 0)
                CreateLine(onset.timestamp, -2 + (float)lastNote * .1f, .2f, Color.blue, onset.strength / 10,1, lastNote,onset.strength);
        }

        private void OnSegment(Value segment)
        {
            //Instantiate a line to represent the segment.
            CreateLine(segment.timestamp, -3, 1, Color.green, segment.value / 10,2, Note.None,-1f);
        }

        private void CreateLine(float timestamp, float position, float scale, Color color, float opacity,int tag,Note note,float strength)
        {
            Line line;
            if (tag == 0)
            {
                line = Instantiate(BeatPrefeb);
                line.transform.SetParent(BeatsParent.transform);
                line.transform.position = new Vector3(0, 0, 0);
                //line.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            }
            else if(tag==1 &&strength>=5)
            {
               
                line = Instantiate(AccentPrefeb);
                line.transform.SetParent(AccentParent.transform);
                line.transform.position = new Vector3(0, 0, 0);
                line.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);



            }
            else
            {
                line = Instantiate(linePrefab);
                line.transform.SetParent(NotesParent.transform);
                line.transform.position = new Vector3(0, position, 0);
                line.transform.localScale = new Vector3(1, 1, 1);
            }
           
            

            line.Init(color, opacity, timestamp,tag,note,strength);

            lines.Add(line);
        }

        System.Collections.IEnumerator BeatSensation(float waittime)
        {
            if (ss.SensationBlock == "Open" && ss.Running)
            {
               
            }
            else
            {
                ss.SensationBlock = "CircleSensation";
                ss.Running = true;
               
                    yield return new WaitForSeconds(waittime);
                if (!(ss.SensationBlock == "Open" && ss.Running))
                {
                    ss.Running = false;
                }
               
            }
           
        }

        System.Collections.IEnumerator NoteSensation(float waittime,Note note)
        {
            if (ss.SensationBlock == "Open" && ss.Running)
            {
                
            }
            else
            {
                

                if (note == Note.A || note == Note.ASharp || note == Note.B)
                {
                    ss.SensationBlock = "Finger1";
                }
                else if (note == Note.C || note == Note.CSHARP)
                {
                    ss.SensationBlock = "Finger2";
                }
                else if (note == Note.D || note == Note.DSHARP)
                {
                    ss.SensationBlock = "finger3";
                }
                else if (note == Note.E || note == Note.F || note == Note.FSHARP)
                {
                    ss.SensationBlock = "finger4";

                }
                else if (note == Note.G || note == Note.GSHARP)
                {
                    ss.SensationBlock = "finger5";
                }

                ss.Running = true;
               
                    yield return new WaitForSeconds(waittime);
                if (!(ss.SensationBlock == "Open" && ss.Running))
                {
                    ss.Running = false;
                }
                
            }
            
        }
        System.Collections.IEnumerator StrengthSensation(float waittime)
        {


            if (ss.SensationBlock != "Open")
            {
                ss.SensationBlock = "Open";
            }
            else if(ss.SensationBlock=="Open")
            {
                ss.enabled = false;
            }
            ss.enabled = true;
            if (!ss.Running)
            {
                ss.Running = true;
            }
           
            
            yield return new WaitForSeconds(waittime);
            ss.Running = false;
            showstrengthtimer = true;
        }
        public void BeatStatus()
        {
            showbeat = !showbeat;
            if (showbeat)
            {
                BeatsParent.SetActive(true);
            }
            else
            {
                BeatsParent.SetActive(false);
            }
        }

        public void NoteStatus()
        {
            shownote = !shownote;
            if (shownote)
            {
                NotesParent.SetActive(true);
            }
            else
            {
                NotesParent.SetActive(false);
            }
        }

        public void StrengthStatus()
        {
            showstrength = !showstrength;
            if (showstrength)
            {
                AccentParent.SetActive(true);
            }
            else
            {
                AccentParent.SetActive(false);
            }
        }
        public void HideBlock()
        {
            if (BlockParent.activeSelf)
            {
                BlockParent.SetActive(false);
            }
            else
            {
                BlockParent.SetActive(true);
            }
        }
    }

   

}
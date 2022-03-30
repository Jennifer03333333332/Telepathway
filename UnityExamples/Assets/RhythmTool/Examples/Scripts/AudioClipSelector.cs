using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RhythmTool.Examples
{
    /// <summary>
    /// The SongSelector selects a song and starts analyzing and playing it.
    /// </summary>
    public abstract class SongSelector : MonoBehaviour
    {
        public RhythmAnalyzer analyzer;
        public RhythmPlayer player;
        public Text songname;
              
        public virtual void NextSong()
        {
            //Stop playing.
            player.Stop();           
        }
    }

    /// <summary>
    /// The AudioClipSelector is a SongSelector that selects a song from a list of AudioClips.
    /// </summary>
    public class AudioClipSelector : SongSelector
    {
        public List<AudioClip> songs;
        public List<AudioClip> songsforlisten;
        public bool wrongscene = true;
        public AudioSource audioSource;
        public AudioSource originalaudiosource;
        private int currentSong = -1;

        void Start()
        {
            //Immediately go to the next song.
            NextSong();
        }

        public override void NextSong()
        {
            base.NextSong();

            //Clean up old resources.
            Destroy(player.rhythmData);

            //start analyzing the next song.
            currentSong++;

            if (currentSong >= songs.Count)
                currentSong = 0;

            AudioClip audioClip = songs[currentSong];
            RhythmData rhythmData = analyzer.Analyze(audioClip, 6);
            
            if (wrongscene)
            {
                songname.text = songsforlisten[currentSong].name;
                AudioClip audio = songsforlisten[currentSong];
                if (currentSong == 0)
                {
                    audioSource.Stop();
                    originalaudiosource.volume=0.2f;
                    audioSource.volume = 0f;

                }
                else
                {
                    originalaudiosource.volume = 0;
                    audioSource.volume = 0.2f;
                    audioSource.clip = audio;
                    audioSource.Play();
                }
               
            }
            else
            {
                songname.text = songs[currentSong].name;
            }
            player.rhythmData = rhythmData;
            //Give the RhythmData to the RhythmPlayer.

        }
    }
}
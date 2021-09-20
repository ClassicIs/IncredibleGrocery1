using UnityEngine.Audio;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    [SerializeField]
    AudioClipItem[] theClips;

    private void Awake()
    {
        for (int i = 0; i < theClips.Length; i++)
        {
            theClips[i].theSource = gameObject.AddComponent<AudioSource>();
            theClips[i].theSource.clip = theClips[i].theAudio;
            theClips[i].theSource.volume = theClips[i].volume;
            theClips[i].theSource.loop = theClips[i].loopOrNot;
        }
    }

    private void Start()
    {
        playSound("MainTheme");
                
    }


    public void playSound(string soundName)
    {
        for(int i = 0; i < theClips.Length; i++)
        {
            if(theClips[i].name == soundName)
            {
                if (!theClips[i].theSource.isPlaying && theClips[i].theSource.enabled == true)
                {
                    theClips[i].theSource.Play();
                }
            }
        }
    }

    public void stopSound(string soundName)
    {
        for (int i = 0; i < theClips.Length; i++)
        {
            if (theClips[i].name == soundName)
            {
                if (theClips[i].theSource.isPlaying && theClips[i].theSource.enabled == true)
                {
                    theClips[i].theSource.Stop();
                }
            }
        }
    }

    public void turnSFX(bool onOrOff)
    {
        for (int i = 0; i < theClips.Length; i++)
        {
            if (theClips[i].chooseState == AudioClipItem.musicOrSfx.SFX)
            {
                if (!onOrOff)
                {
                    theClips[i].theSource.enabled = false;
                }
                else
                {
                    theClips[i].theSource.enabled = true;
                }                
            }
        }
    }

    public void turnMusic(bool onOrOff)
    {
        for (int i = 0; i < theClips.Length; i++)
        {
            if (theClips[i].chooseState == AudioClipItem.musicOrSfx.Music)
            {
                if (!onOrOff)
                {
                    theClips[i].theSource.enabled = false;
                }
                else
                {
                    theClips[i].theSource.enabled = true;
                }
            }
        }
    }

}

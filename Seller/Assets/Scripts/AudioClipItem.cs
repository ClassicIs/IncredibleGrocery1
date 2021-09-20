using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class AudioClipItem
{
    public enum musicOrSfx
    {
        SFX,
        Music
    };
    public AudioClip theAudio;
    [HideInInspector]
    public AudioSource theSource;
    public string name;

    public bool loopOrNot;
    
    public musicOrSfx chooseState;
    public float volume;
    
}

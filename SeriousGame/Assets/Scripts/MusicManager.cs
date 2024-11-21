using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    [Tooltip("Instance of the MusicManager")]
    private static MusicManager mm { get; set; }
    private static GameObject instance { get; set; }
    private int prevScene = 0;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            GlobalMusicVolume = 1;
            mm = this;
            instance = gameObject;
            DontDestroyOnLoad(instance);
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        CheckAudios();
    }

    private void Update()
    {
        //Check for scene change
        if (SceneManager.GetActiveScene().buildIndex != prevScene)
        {
            CheckAudios();
            prevScene = SceneManager.GetActiveScene().buildIndex;
        }
    }

    //Music Settings
    public static float GlobalMusicVolume { get; set; }

    //Many public static functions for use!!!
    public static void CheckAudios()
    {
        Transform t = instance.transform;
        for (int i = 0; i < t.childCount; i++)
        {
            t.GetChild(i).GetComponent<MusicScript>().checkAudio();
        }
    }
    public static string[] GetSongNames(int? index)
    {
        Transform t = instance.transform;
        string[] ret = new string[t.childCount];
        for (int i = 0; i < t.childCount; i++)
        {
            ret[i] = t.GetChild(i).GetComponent<AudioSource>().clip.name;
        }

        return ret;
    }
    public static MusicScript GetMusicScript(string audioClipName)
    {
        string clip = audioClipName;
        //No extension
        if (audioClipName.Contains(".mp3") || audioClipName.Contains(".wav") || audioClipName.Contains(".ogg"))
        {
            Debug.LogWarning("GetMusicScript() was run with parameter: \"" + clip + "\". Please remove the file extension where you are callingthis function.");
            clip = audioClipName[..clip.IndexOf('.')];
        }

        Transform t = instance.transform;
        string[] ret = new string[t.childCount];
        for (int i = 0; i < t.childCount; i++)
        {
            if (clip == t.GetChild(i).GetComponent<AudioSource>().clip.name) return t.GetChild(i).GetComponent<MusicScript>();
        }

        Debug.Log("GetMusicScript() function returned null, a MusicScript using clip: \"" + clip + "\" was not found.");
        return null;
    }

    public static void StartSong(string audioClipName)
    {
        MusicScript ms = GetMusicScript(audioClipName);
        ms.Enter();
    }
    public static void StopSong(string audioClipName)
    {
        MusicScript ms = GetMusicScript(audioClipName);
        ms.Exit();
    }
    public static void ToggleSong(string audioClipName)
    {
        MusicScript ms = GetMusicScript(audioClipName);
        if (ms.src.volume == 0) ms.Enter();
        else if (ms.src.volume == ms.maxVol) ms.Exit();
        else if (ms.volIncreasing) ms.Exit();
        else ms.Enter();
    }
    public static void ChangeSongVolume(string audioClipName, float newVolume)
    {
        MusicScript ms = GetMusicScript(audioClipName);
        ms.changeVol(newVolume);
    }
    public static void TemporaryVolumeFade(string audioClipName, float volume1, float volume2, float volume1TimeLength)
    {
        MusicScript ms = GetMusicScript(audioClipName);
        ms.tempFade(volume1, volume2, volume1TimeLength);
    }
}

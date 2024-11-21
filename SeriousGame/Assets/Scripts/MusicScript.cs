using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicScript : MonoBehaviour
{
    //Member variables
    public string[] playsOnScenes;
    public string[] startOnScenes;
    public float maxVol = 1;
    public float MaxVolume
    {
        get { return maxVol * MusicManager.GlobalMusicVolume; }
    }
    public float fadeRate = 0.1f;
    float targetVol = 1;
    public float loopStart = 0, loopEnd = 0;
    public float songStart = 0;

    public bool volIncreasing = false;

    public AudioSource src { get => gameObject.GetComponent<AudioSource>(); }

    private void Start()
    {
        src.time = songStart;
    }

    //Public Methods

    public void checkAudio()
    {
        for (int i = 0; i < startOnScenes.Length; i++)
        {
            if (SceneManager.GetActiveScene().name == startOnScenes[i])
            {
                if (!src.loop)
                    src.PlayOneShot(src.clip);
                src.time = songStart;
                break;
            }
        }
        float _vol = 0;
        for (int i = 0; i < playsOnScenes.Length; i++)
        {
            if (SceneManager.GetActiveScene().name == playsOnScenes[i])
            {
                _vol = maxVol;
                break;
            }
        }
        targetVol = _vol;
        StartCoroutine(fadeVol());
    }

    public void changeVol(float _newVol)
    {
        targetVol = _newVol;
        StartCoroutine(fadeVol());
    }

    public void tempFade(float _fadeToVol, float _afterFadeVol, float _waitSeconds)
    {
        StartCoroutine(TempFade(_fadeToVol, _afterFadeVol, _waitSeconds));
    }

    public void Enter()
    {
        changeVol(maxVol);
    }

    public void Exit()
    {
        changeVol(0);
    }

    //Private Stuff

    private void Update()
    {
        if (loopEnd != 0)
        {
            if (src.time >= loopEnd)
                src.time = loopStart;
        }
    }

    IEnumerator fadeVol()
    {
        yield return new WaitForSeconds(fadeRate);
        if (src.volume > targetVol)
        {
            src.volume -= fadeRate;
            volIncreasing = false;
        }
        else if (src.volume < targetVol)
        {
            src.volume += fadeRate;
            volIncreasing = true;
        }

        if (src.volume != targetVol)
            StartCoroutine(fadeVol());

    }

    IEnumerator TempFade(float _before, float _after, float _wait)
    {
        changeVol(_before);
        yield return new WaitForSeconds(_wait);
        changeVol(_after);
    }
}

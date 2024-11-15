using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXScript : MonoBehaviour
{
    public AudioClip[] sfxList;
    public Vector2 randomPitchRange=Vector2.one;

    AudioSource src;
    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<AudioSource>();
    }

    public void PlayRandom()
    {
        Play(sfxList[Random.Range(0, sfxList.Length)]);
    }
    public void Play(AudioClip clip)
    {
        src.pitch = Random.Range(randomPitchRange.x, randomPitchRange.y);
        src.PlayOneShot(clip);
    }
}

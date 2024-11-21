using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicRadio : MonoBehaviour
{
    public AudioClip[] songs;
    AudioSource src;

    int currInd = -1;
    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<AudioSource>();

        List<AudioClip> newSongOrder = new(),_songs = new();
        _songs.AddRange(songs);
        for(int i=0; i<songs.Length; i++)
        {
            int ind = Random.Range(0, _songs.Count);
            newSongOrder.Add(_songs[ind]);
            _songs.RemoveAt(ind);
        }
        songs = newSongOrder.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        if (!src.isPlaying)
        {
            //play next song
            currInd++;
            src.clip = songs[currInd];
            src.PlayDelayed(1.5f);
        }
        else
        {
            Debug.Log("Song Progress: " + src.time + "/" + songs[currInd].length);
        }
    }
}

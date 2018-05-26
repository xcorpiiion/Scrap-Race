using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class Radio : MonoBehaviour {

    private AudioSource audioSource;
    public AudioClip[] sound_Musicas;
    public float tempoMusic;
    private int randomMusica;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        randomMusica = Random.Range(0, sound_Musicas.Length);
        audioSource.PlayOneShot(sound_Musicas[randomMusica]);
        tempoMusic = sound_Musicas[randomMusica].length;
        StartCoroutine("tempoMusica");

    }
	
	IEnumerator tempoMusica() {
        yield return new WaitForSecondsRealtime(tempoMusic);
        randomMusica = Random.Range(0, sound_Musicas.Length);
        audioSource.PlayOneShot(sound_Musicas[randomMusica]);
        tempoMusic = sound_Musicas[randomMusica].length;
    }
}

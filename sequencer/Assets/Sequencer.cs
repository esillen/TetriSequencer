using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sequencer : MonoBehaviour {

	public GameObject highlight;
	public AudioClip [] clips;
	private const int width = 8;
	private const int height = 10;
	private float timePerBeat = 0.2f;
	private float currentTime = 0.0f;
	private GameObject[,] objectMap = new GameObject[height,width];
	private int currentColumn = 0;
	private AudioSource [] audioSources = new AudioSource[height];

	// Use this for initialization
	void Start () {
		for (int i = 0; i < height; i++) {
			audioSources[i] = gameObject.AddComponent<AudioSource> ();
		}
	}
		
	// Update is called once per frame
	void Update () {
		currentTime += Time.deltaTime;
		if (currentTime > timePerBeat) {
			currentTime -= timePerBeat;
			updateInstruments ();
		}
	}

	private void updateInstruments() {

		int [,] gameGrid = GetComponent<GamePlayOverlord> ().getGameGrid();
		
		for (int i = 0; i < height; i++) {
			if (objectMap [i, currentColumn] != null) {
				Destroy (objectMap [i, currentColumn]);
				objectMap [i, currentColumn] = null;
			}
		}

		currentColumn++;
		if (currentColumn >= width) {
			currentColumn = 0;
		}

		for (int i = 0; i < 5; i++) {
			objectMap [i, currentColumn] = Instantiate (highlight, new Vector3 (currentColumn, i, 0f), Quaternion.identity) as GameObject;
			if (gameGrid [i, currentColumn] != (int)SqaureState.Empty) {
				audioSources[i].PlayOneShot (clips[i]);
			}
		}
	}
}

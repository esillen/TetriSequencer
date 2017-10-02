using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sequencer : MonoBehaviour {

	public GameObject highlight;
	public AudioClip [] clips;
	private int width;
	private int height;
	private float timePerBeat = 0.2f;
	private float currentTime = 0.0f;
	private GameObject[,] objectMap;
	private int currentColumn = 0;
	private AudioSource [] audioSources;
	private GamePlayOverlord gamePlayOverlord;

	// Use this for initialization
	void Start () {
		gamePlayOverlord = GetComponent<GamePlayOverlord> ();
	

		int[,] gameGrid = gamePlayOverlord.getGameGrid ();
		height = gameGrid.GetLength (1);
		width = gameGrid.GetLength (0);
		objectMap = new GameObject[height,width];
		audioSources = new AudioSource[height];
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

		int[,] gameGrid = gamePlayOverlord.getGameGrid ();
		
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

		for (int i = height - 1; i >= 0; i--) {
			int posFromGround = height - i - 1; // So that clip 0 plays at the bottom (easier to think that way imo)
			objectMap [i, currentColumn] = Instantiate (highlight, new Vector3 (currentColumn, i, 0f), Quaternion.identity) as GameObject;
			if (gameGrid [currentColumn,  i] != (int)SqaureState.Empty && posFromGround < clips.GetLength(0)) {
				audioSources[i].PlayOneShot (clips[posFromGround]);
			}
		}
	}
}

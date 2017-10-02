using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sequencer : MonoBehaviour {

	public GameObject oneSquare;
	public GameObject highlight;
	public GameObject blockMarker;
	public AudioClip [] clips;
	private const int width = 8;
	private const int height = 10;
	private float timePerBeat = 0.2f;
	private float currentTime = 0.0f;
	private GameObject[,] objectMap = new GameObject[height,width];
	private GameObject[,] markersMap = new GameObject[height,width];
	private int currentColumn = 0;
	private AudioSource [] audioSources = new AudioSource[height];

	// Use this for initialization
	void Start () {
		for (int i = 0; i < height; i++) {
			audioSources[i] = gameObject.AddComponent<AudioSource> ();
		}
		for (int i = 0; i < height; i++) {
			for (int j = 0; j < width; j++) {
				GameObject newsquare = Instantiate (oneSquare, new Vector3 (j, i, 0f), Quaternion.identity) as GameObject;
			}
		}



		addMarker (1, 0);
		addMarker (2, 0);
		addMarker (3, 0);
		addMarker (5, 0);
		addMarker (6, 0);
		addMarker (7, 0);

		addMarker (0, 1);
		addMarker (1, 1);
		addMarker (2, 1);
		addMarker (3, 1);
		addMarker (4, 1);
		addMarker (5, 1);
		addMarker (6, 1);
		addMarker (7, 1);

		addMarker (0, 2);
		addMarker (2, 2);
		addMarker (4, 2);
		addMarker (6, 2);

		addMarker (3, 3);
		addMarker (4, 3);
		addMarker (6, 3);
		addMarker (7, 3);

		addMarker (3, 4);
		addMarker (4, 4);

		addMarker (5, 5);
	}

	void addMarker(int x, int y) {
		markersMap [y, x] = Instantiate (blockMarker, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;
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

		for (int i = 0; i < height; i++) {
			objectMap [i, currentColumn] = Instantiate (highlight, new Vector3 (currentColumn, i, 0f), Quaternion.identity) as GameObject;
			if (markersMap [i, currentColumn] != null) {
				audioSources[i].PlayOneShot (clips[i]);
			}
		}
	}
}

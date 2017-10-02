using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayOverlord : MonoBehaviour {

	public BGCreator bgCreator;

	private float squareDist = 0.5f;
	private int[] proportions = new int[2]{8, 12};

	private int spawnGridHeight = 5;
	private int[,] fullGrid;
	private int[,] movingPiece;
	private bool gameOver = false;



	// Use this for initialization
	void Start () {
		bgCreator.generateGrid (proportions, squareDist);
		fullGrid = new int[proportions [0], proportions [1] + spawnGridHeight];

		for (int y = 0; y < fullGrid.GetLength(1); y++)
			for (int x = 0; x <  fullGrid.GetLength(0); x++) 
				fullGrid[x, y] = (int)SqaureState.Empty;
			
		spawnNewPice ();
		StartCoroutine (gameLoop ());
	}

	void Update(){
		int[,] gameGrid = new int[proportions [0], proportions [1]];
		for (int y = 0; y < proportions [1]; y++)
			for (int x = 0; x < proportions [0]; x++)
				gameGrid [x, y] = fullGrid [x, y+spawnGridHeight];
		bgCreator.render (gameGrid);
	}



	private IEnumerator gameLoop(){
		while (gameOver == false) {
			printGrid (fullGrid);
			yield return new WaitForSeconds (0.5f);
			moveBoardDown ();
		}
	}
		
	private void moveBoardDown(){
		List<int[]> moveIndexes = new List<int[]> ();

		//Find all current moving parts
		for (int y = fullGrid.GetLength (1)-1; y >= 0; y--)
			for (int x = 0; x < fullGrid.GetLength (0); x++) 
				if (fullGrid [x, y] == (int)SqaureState.Moving)
					moveIndexes.Add (new int[2]{x, y});

		//Detect if we have a crash
		bool stop = false;
		foreach (int[] m in moveIndexes) 
			if(m[1]+1 >= fullGrid.GetLength(1) || (fullGrid[m[0], m[1]+1] != (int)SqaureState.Empty) && moveIndexes.Find(x => x[0] == m[0] && x[1] == m[1]+1) == null){
				stop = true;
				break;
			}

		if (stop) {
			stopMoving ();
			spawnNewPice ();
			return;
		}

		for (int y = fullGrid.GetLength (1); y >= 0; y--)
			foreach (int[] m in moveIndexes)
				if (m [1] == y) {
					fullGrid[m[0], m[1]] = (int)SqaureState.Empty;
					fullGrid [m [0], m [1] + 1] = (int)SqaureState.Moving;
				}
	}



	private void stopMoving(){
		for (int y = fullGrid.GetLength (1)-1; y >= 0; y--)
			for (int x = 0; x < fullGrid.GetLength (0); x++)
				if (fullGrid [x, y] == (int)SqaureState.Moving)
					fullGrid [x, y] = (int)SqaureState.Solid;
	}



	private void spawnNewPice(){
		movingPiece = PiceGenerator.getRandomPiece ();
		//Give random rotation to piece
		int startX = Random.Range (0, proportions [0] - movingPiece.GetLength (0));
		for (int x = 0; x < movingPiece.GetLength (0); x++)
			for (int y = 0; y < movingPiece.GetLength (1); y++)
				fullGrid [startX + x, spawnGridHeight - movingPiece.GetLength (1) + y] = movingPiece [x, y];
	}





	private void printGrid(int[,] temp){
		for (int y = 0; y < temp.GetLength (1); y++) {
			string s = "";
			for (int x = 0; x < temp.GetLength (0); x++)
				s += temp [x, y].ToString () + " ";
			print (s);
		}
	}

}


public enum SqaureState {
	Empty = 0,
	Moving = 1,
	Solid = 2,
}
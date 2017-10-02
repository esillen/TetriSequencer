using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGCreator : MonoBehaviour {

	public GameObject bgSquarePrefab;
	private SpriteRenderer[,] bricks;
	private Vector2 topLeftCorner = new Vector2 (-2, 2);

	public void generateGrid(int[] proportions, float squareDist){
		bricks = new SpriteRenderer[proportions [0], proportions [1]];
		GameObject temp = new GameObject ("Background");
		for (int y = 0; y < proportions [1]; y++)
			for (int x = 0; x < proportions [0]; x++)
				bricks[x, y] = Instantiate (bgSquarePrefab,topLeftCorner + new Vector2 (x * squareDist, -y * squareDist), Quaternion.identity, temp.transform).GetComponent<SpriteRenderer>();
	}


	public void render(int[,] gameGrid){
		for (int y = 0; y < gameGrid.GetLength (1); y++)
			for (int x = 0; x < gameGrid.GetLength (0); x++) {
				if (gameGrid [x, y] == 0)
					bricks [x, y].color = Color.white;
				else if (gameGrid [x, y] == 1)
					bricks [x, y].color = Color.red;
				else if (gameGrid [x, y] == 2)
					bricks [x, y].color = Color.grey;
			}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardControlls : MonoBehaviour {

	public static bool canMove(int[,] fullGrid, int xDir, int yDir){
		List<int[]> moveIndexes = getMovingPos (fullGrid);

		//Detect if we have a crash
		foreach (int[] m in moveIndexes) 
			if(m[1]+1 >= fullGrid.GetLength(1) || (fullGrid[m[0]+xDir, m[1]+yDir] != (int)SqaureState.Empty) && moveIndexes.Find(x => x[0] == m[0]+xDir && x[1] == m[1]+yDir) == null)
				return false;
		
		return true;
	}


	public static List<int[]> getMovingPos(int[,] grid){
		List<int[]> moveIndexes = new List<int[]> ();

		//Find all current moving parts
		for (int y = grid.GetLength (1)-1; y >= 0; y--)
			for (int x = 0; x < grid.GetLength (0); x++) 
				if (grid [x, y] == (int)SqaureState.Moving)
					moveIndexes.Add (new int[2]{x, y});

		return moveIndexes;
	}




	public static int[,] moveInXDirection(int[,] grid , int xDir){
		List<int[]> moveIndexes = getMovingPos (grid);
		int startX = xDir < 0 ? 0 : grid.GetLength (0) - 1;

		for (int x = startX; x >= 0 && x < grid.GetLength (0); x -= xDir) {
			foreach (int[] m in moveIndexes)
				if (m [0] == x) {
					grid[m[0]+xDir, m[1]] = (int)SqaureState.Moving;
					grid[m[0], m[1]] = (int)SqaureState.Empty;	
				}
		}

		return grid;
	}
}

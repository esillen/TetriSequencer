using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiceGenerator : MonoBehaviour {

	private readonly static int[,] pieceA = new int[,]{{1,0}, {1,1}, {1,0} };
	private readonly static int[,] pieceB = new int[,]{{1,1}, {1,1}};
	private static List<int[,]> allPieces = new List<int[,]> (){ pieceA, pieceB };



	public static int[,] getRandomPiece(){
		return allPieces [Random.Range (0, allPieces.Count)];
	}
}

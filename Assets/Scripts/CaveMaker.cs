using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveMaker : MonoBehaviour {

	public GameObject cell;
	private int[,] wallC;

	// World space dimensions
	public int mapWidth, mapHeight;

	// Total cells to generate
	public int width, height;
	public int wallChance;

	public int birthLimit, deathLimit, treasureLimit;

	private bool[,] grid;

	void Start () {
		birthLimit = 5;
		deathLimit = 4;
		treasureLimit = 6;

		wallC = new int[width, height];
		grid = new bool[width, height];

		InitGrid ();

		CellularAutomata (5);

		DrawMap ();
	}

	void Update () {

	}

	// Initialize grid with 45% walls
	private void InitGrid(){
		for (int i = 0; i < width; i++) { // Each row
			for (int j = 0; j < height; j++) { // Each column
				if (GetProbability ()) {
					grid [i, j] = true;
				}
			}
		}
	}
	private bool GetProbability(){
		int rand = UnityEngine.Random.Range (0, 100);
		return (rand < wallChance);
	}

	private void CellularAutomata(int iterations){
		for (int iter = 0; iter < iterations; iter++) {
			grid = CaveAutomation ();
		}
	}
	private bool[,] CaveAutomation(){
		bool[,] newGrid = new bool[width, height];

		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {

				int wallCount = GetWallCount (i, j);

				if (wallCount >= birthLimit) {
					newGrid [i, j] = true;
				} else if (wallCount <= deathLimit) {
					newGrid [i, j] = false;
				}
			}
		}
		return newGrid;
	}

	private int GetWallCount(int i, int j){
		int wallCount = grid[i, j] ? 1 : 0;

		Vector2 dir = new Vector2 (0, -1); // Necessary starting point (0, -1)

		float c = (Mathf.PI/4);
		while(c < (2*Mathf.PI)){ // Each direction (8)
			try{
				dir.x = (int) Mathf.Clamp(Mathf.Round(dir.x + Mathf.Cos(c)), -1, 1);
				dir.y = (int) Mathf.Clamp(Mathf.Round(dir.y + Mathf.Sin(c)), -1, 1);

				//print((i+a)+", "+(j+b));
				if(i+dir.x < 0 || i+dir.x >= width || j+dir.y < 0 || j+dir.y >= height){
					wallCount++;
				}else if(grid[(i+(int)dir.x), (j+(int)dir.y)] == true){
					wallCount++;
				}

			} catch(Exception e){
				//print(e.Message);
			}

			c += (Mathf.PI/4);
		}
		return wallCount;
	}

	private void DrawMap(){
		Vector2 mapScale = new Vector2 ((float)width / (float)mapWidth, (float)height / (float)mapHeight);
		cell.transform.localScale = mapScale;
		//print (mapScale.x + ", " + mapScale.y);
		//print (this.GetComponent<Collider2D> ().bounds.size);

		for(int i = 0; i < width; i++){
			for(int j = 0; j < height; j++){
				if (grid [i, j] == true) {
					this.wallC[i, j] = GetWallCount (i, j);

					var wall = Instantiate (cell, new Vector2 (i * this.GetComponent<Collider2D>().bounds.size.x * mapScale.x, j * this.GetComponent<Collider2D>().bounds.size.y * mapScale.y), this.transform.rotation);
					wall.GetComponent<wallcount>().wallCount = wallC[i, j];
				}
			}
		}
	}
}
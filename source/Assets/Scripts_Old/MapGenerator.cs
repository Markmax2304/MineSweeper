using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapGenerator : MonoBehaviour 
{
	public CellLogic cell;
	public int height;
	public int width;
	public int countMine;

	float size = .16f;
	bool isVictory = false;
	bool isEnd;
	float time;

	CellLogic[,] mapCells;
	CellLogic[] mines;

	void Start () 
	{
		isEnd = false;

		mapCells = new CellLogic[height, width];
		mines = new CellLogic[countMine];

		CellLogic.amountFlags = countMine;

		for (int i = 0; i < height; i++) {
			for (int j = 0; j < width; j++) {
				Vector2 currentPosition = new Vector2 ((height / 2f - i - .5f) * size, (width / 2f - j - .5f) * size);
				mapCells [i, j] = Instantiate (cell, currentPosition, Quaternion.identity) as CellLogic;
				mapCells [i, j].x = i;
				mapCells [i, j].y = j;
				mapCells [i, j].transform.parent = this.transform;
			}
		}

		RandomMine ();

		CountingOfMineAround ();
	}

	void Update ()
	{
		if (CellLogic.isDeath || Input.GetMouseButton (2)) {
			if (!isVictory) {
				//Debug.Log ("Defeat!");
				LookCells ();
				CellLogic.isDeath = false;
				isEnd = true;
			}
		}

		if (CellLogic.clickX != -1 && CellLogic.clickY != -1) {
			LookEmptyCells (CellLogic.clickX, CellLogic.clickY);
			CellLogic.clickX = -1;
			CellLogic.clickY = -1;
		}

		if (!isVictory && CellLogic.amountFlags == 0) {
			CheckVictory ();
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			SceneManager.LoadScene (0);
		}

		if (!isEnd && !isVictory) {
			time += Time.fixedDeltaTime;
			//Debug.Log (time);
		}

		if (Input.GetKeyDown (KeyCode.E)) {
			Application.Quit ();
		}
	}

	void RandomMine ()
	{
		for (int i = 0; i < countMine; i++) {
			int h = Random.Range (0, height - 1);
			int w = Random.Range (0, width - 1);

			if (!mapCells [h, w].isMine) {
				mapCells [h, w].isMine = true;
				mines [i] = mapCells [h, w];
			} else {
				i--;
			}
		}
	}

	void CountingOfMineAround ()
	{
		for (int i = 0; i < mines.Length; i++) {
			for (int x = mines [i].x - 1; x <= mines [i].x + 1; x++) {
				for (int y = mines [i].y - 1; y <= mines [i].y + 1; y++) {
					if (x >= 0 && x < height && y >= 0 && y < width) {
						if (!mapCells [x, y].isMine) {
							mapCells [x, y].countAroundMine++;
						}
					}
				}
			}
		}
	}

	void LookEmptyCells (int i, int j)
	{
		if (!mapCells [i, j].isMine) {
			for (int x = i - 1; x <= i + 1; x++) {
				for (int y = j - 1; y <= j + 1; y++) {
					if (x >= 0 && x < height && y >= 0 && y < width) {
						if(mapCells [x, y].isFlag) {
							continue;
						}

						if (!mapCells [x, y].isOpen) {
							mapCells [x, y].LookCell ();
							if (mapCells [x, y].countAroundMine == 0) {
								LookEmptyCells (x, y);
							}
						}
					}
				}
			}
		}
	}

	void CheckVictory ()
	{
		foreach (CellLogic cell in mines) {
			if (cell.isFlag != cell.isMine) {
				return;
			}
		}

		//Debug.Log ("Victory!");
		LookCells ("victory");
		isVictory = true;
	}

	void LookCells ()
	{
		for (int i = 0; i < height; i++) {
			for (int j = 0; j < width; j++) {
				mapCells [i, j].LookCell ();
			}
		}
	}

	void LookCells (string s)
	{
		for (int i = 0; i < height; i++) {
			for (int j = 0; j < width; j++) {
				mapCells [i, j].LookCell (s);
			}
		}
	}
}
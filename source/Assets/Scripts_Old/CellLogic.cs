using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellLogic : MonoBehaviour 
{
	public Sprite closed;
	public Sprite[] numbers;
	public Sprite mine;
	public Sprite explosionMine;
	public Sprite defeatMine;
	public Sprite flag;
	public static int amountFlags;

	public static int clickX = -1;
	public static int clickY = -1;

	public static bool isDeath = false;

	SpriteRenderer SprRend;

	[HideInInspector]
	public bool isMine;
	[HideInInspector]
	public int countAroundMine = 0;
	[HideInInspector]
	public int x;
	[HideInInspector]
	public int y;
	[HideInInspector]
	public bool isOpen = false;
	[HideInInspector]
	public bool isFlag = false;
	bool isInside = false;

	void Start()
	{
		SprRend = GetComponent<SpriteRenderer> ();
		SprRend.sprite = closed;
	}

	void Update ()
	{
		if(isInside && !isFlag && Input.GetMouseButtonDown (0)) {
			LookCell ();

			if (countAroundMine == 0) {
				clickX = x;
				clickY = y;
			}
		}

		if (isInside && !isOpen && Input.GetMouseButtonDown (1)) {
			
			if (!isFlag) {
				if (amountFlags > 0) {
					SprRend.sprite = flag;
					isFlag = true;
					amountFlags--;
					//Debug.Log (amountFlags);
				}
			} else {
				if (amountFlags >= 0) {
					SprRend.sprite = closed;
					isFlag = false;
					amountFlags++;
					//Debug.Log (amountFlags);
				}
			}
		}
	}

	void OnMouseEnter ()
	{
		isInside = true;
	}

	void OnMouseExit ()
	{
		isInside = false;
	}

	public void LookCell ()
	{
		if (!isOpen) {
			if (isMine) {
				SprRend.sprite = mine;
				isDeath = true;
				if (isInside) {
					SprRend.sprite = explosionMine;
				}
			} else {
				SprRend.sprite = numbers [countAroundMine];
			}
			isOpen = true;
		}
	}

	public void LookCell (string s)
	{
		if (s == "victory") {
			if (!isOpen) {
				if (isMine) {
					SprRend.sprite = defeatMine;
				} else {
					SprRend.sprite = numbers [countAroundMine];
				}
				isOpen = true;
			}
		}
	}
}
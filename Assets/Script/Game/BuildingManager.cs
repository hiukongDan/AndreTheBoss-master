﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
	public GameManager gameManager;
	public int buildRange;	
	public bool buildmode;
	
	//public HexCell currentHex;
	
	public void OnEnable()
	{
		buildmode=false;
	}
	
    public void UpdateBuildMode(bool isbuildmode)
	{
		buildmode=isbuildmode;
		gameManager.gameInteraction.Clear();
		ShowBuildableHex();
		if(buildmode)
			gameManager.gameCamera.FocusOnPoint(gameManager.monsterManager.MonsterPawns[0].transform.position);
	}
	
	public void ShowBuildableHex()
	{
		if(gameManager.monsterManager.MonsterPawns.Count!=0)
		gameManager.hexMap.UpdateBuildableCells(gameManager.monsterManager.MonsterPawns[0].currentCell, buildRange ,buildmode);
	}
}
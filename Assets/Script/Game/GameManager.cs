﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Utility
    public CharacterReader characterReader;
    
    // Sub Managers
    public HexMap hexMap;
    public GameCamera gameCamera;
    public GameTurnManager gameTurnManager;
    public EnemyManager enemyManager;
    public MonsterManager monsterManager;
	public BuildingManager buildingManager;
	public HealthBarManager healthbarManager;
	public ItemManager itemManager;
    public GameInteraction gameInteraction;
	
	public MonsterActionManager monsterActionManager;

    // Main Character
    private Boss boss;

    // Misc
    // TODO: Add more

    public void OnEnable()
    {
		
        InitCharacterReader();
        hexMap.GenerateCells();
        hexMap.HideCells();
        gameTurnManager = new GameTurnManager();
		HexCell hexcell=hexMap.GetRandomCellToSpawn();
		monsterManager.InitMonsterManager();
        characterReader.GetEnemyData(4, EnemyType.cultist.ToString());
        InitBoss();
        gameCamera.FocusOnPoint(boss.transform.position);
		monsterActionManager.InitMonsterAcitonManager();
    }

    private void InitBoss()
    {
        boss = (Boss)monsterManager.CreateMonster(MonsterType.boss, hexMap.GetRandomCellToSpawn(), 1);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("my gameturn manager"+gameTurnManager.IsPlayerTurn());
    }

    public void InitCharacterReader()
    {
        characterReader = new CharacterReader();
        characterReader.ReadFile();
    }


    public void QuitGame()
    {
        SaveGame();
        Application.Quit();
    }

    public void SaveGame()
    {
        // stub
    }

    public CharacterReader getCharacterReader()
    {
        return this.characterReader;
    }
}
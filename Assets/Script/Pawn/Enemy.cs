﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Pawn
{
    public EnemyType enemyType;
    public void InitializeEnemy(EnemyType enemyType, string name,
    int attack, int defense, int HP, int dexterity, int attackRange,int magicAttack , int magicDefense,int level)
    {
        this.enemyType = enemyType;
        Name = enemyType.ToString();
        InitializePawn(PawnType.Enemy, name, attack, defense, HP, dexterity, attackRange,magicAttack,magicDefense,level);
    }

    public override string ToString()
    {
        switch(this.enemyType)
        {
            case EnemyType.magicapprentice:
                return "Magic\nApprentice";
            case EnemyType.thief:
                return "Thief";
            case EnemyType.wanderingswordman:
                return "Wandering\nSwordman";
            default:
                return "?Warrior?";
        }
    }
}
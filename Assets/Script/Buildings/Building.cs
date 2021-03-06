﻿using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Building: MonoBehaviour
{
    public static List<Dictionary<int, int>> requireSouls =new List<Dictionary<int, int>>{
		new Dictionary<int, int> { { 1, 1 }, { 2, 10 }, { 3, 30 } },new Dictionary<int, int> { { 1, 1 }, { 2, 10 }, { 3, 30 } },
		new Dictionary<int, int> { { 1, 5 }, { 2, 10 } },new Dictionary<int, int> { { 1, 20 } }	} ;
	public static List<Dictionary<int, int>> produceItems =new List<Dictionary<int, int>>{
		new Dictionary<int, int> { { 1, 1 }, { 2, 2 }, { 3, 3 } },new Dictionary<int, int> { { 1, 1 }, { 2, 2 }, { 3, 3 } },
		new Dictionary<int, int> (),new Dictionary<int, int> ()};
		
	public HexCell currentCell;
	public HealthBar healthbar;
	
	protected BuildingType buildingType;
    protected int currentLevel; // 1,2,3
    protected ItemType itemTypeProduced;

	protected int currentHP;
	protected int maxHP;
	
    public void InitBuilding(BuildingType type, ItemType itemTypeProduced, int initLevel)
    {
		buildingType=type;
        currentLevel = Mathf.Clamp(initLevel, 1,GetMaxLevel(type));
		currentHP = maxHP = GameConfig.BuildingHP[currentLevel];
		if (GetValidProduct(type).Contains(itemTypeProduced))
			this.itemTypeProduced = itemTypeProduced;

		// for catapult
		//buildingPawn = gameObject.AddComponent<Pawn>();
		Debug.Log("initbuilding: "+ gameObject);
		gameObject.AddComponent<Pawn>();
		//buildingPawn.Type = PawnType.Building;
		healthbar.UpdateBuildingLife();
    }

    public int LevelUp() // return souls used
    {
        if (currentLevel == GetMaxLevel(buildingType))
            return 0;
		int before=0;
		int after=0;
		requireSouls[(int)buildingType].TryGetValue(currentLevel + 1, out after);
		requireSouls[(int)buildingType].TryGetValue(currentLevel,out before);
        int required = after-before;

        currentLevel++;
		maxHP = GameConfig.BuildingHP[currentLevel];
		SetAppearance(currentLevel);
		OnLevelUp();
		healthbar.UpdateBuildingLife();
        return required;
    }
	
	

    public int GetCurrentProduceNumber()
    {
		int result=0;
		produceItems[(int)buildingType].TryGetValue(currentLevel,out result);
		return result;
    }
	
	public int GetNextLevelProduceNumber()
    {
		int result=0;
		produceItems[(int)buildingType].TryGetValue(currentLevel+1,out result);
		return result;
    }
	
	public static int GetMaxLevel(BuildingType type)
	{
		return requireSouls[(int)type].Count;
	}
	
	public static int GetRequireSouls(BuildingType type,int level)
	{
		int result=0;
		requireSouls[(int)type].TryGetValue(level,out result);
		return result;
	}
	
	public static int GetProduceNumber(BuildingType type, int level)
	{
		int result=0;
		produceItems[(int)type].TryGetValue(level,out result);
		return result;
	}
	
	public static List<ItemType> GetValidProduct(BuildingType type)
	{
		List<ItemType> list=new List<ItemType>();;
		switch((int)type)
		{
			case 0:
				list=new List<ItemType>(){ItemType.Spiderlily,ItemType.DemonFruit,ItemType.GoldenApple};
				break;
			case 1:
				list=new List<ItemType>(){ItemType.Sulphur,ItemType.Mercury,ItemType.Gold};
				break;
			default:
				break;
		}
		return list;
	}
	
	public static string GetDescription(BuildingType type,ItemType itemType,int level)
	{
		string description="";
		switch((int)type)
		{
			case 0:
				description=Farm.GetDescription(itemType,GetProduceNumber(type, level));
				break;
			case 1:
				description=Mine.GetDescription(itemType,GetProduceNumber(type, level));
				break;
			case 2:
				description = Teleporter.GetDescription(Teleporter.GetMaxDistance(level));
				break;
			case 3:
				description = Altar.GetDescription();
				break;
			default:
				break;
		}
		return description;
	}
	
	public virtual string GetDescription(){return "Description";}
	public virtual string GetUpgradeDescription(){return "Description";}
	public virtual void OnLevelUp(){}
	public virtual void OnPlayerTurnBegin(){}
	
	public virtual void DestroyBuilding()
	{
		GameObject.Destroy(gameObject);
	}
	
	public void SetAppearance(int level)
	{
		for(int i=0;i<this.transform.transform.GetChild(0).childCount;i++)
			this.transform.GetChild(0).GetChild(i).gameObject.SetActive((i+1)==level?true:false);
	}

	public BuildingType GetBuildingType()
	{
		return buildingType;
	}
	
	public int GetCurrentLevel()
	{
		return currentLevel;
	}
	
	public ItemType GetItemType()
	{
		if(GetValidProduct(buildingType).Count>0)
			return itemTypeProduced;
		else
			return ItemType.NUM;
	}
	
	public int GetMaxHP()
	{
		return maxHP;
	}
	
	public  int GetCurrentHP()
	{
		return currentHP;
	}
	
	public bool CanbeSkillTargetOf()
	{
		return GetCurrentProduceNumber()==0?false:true;
	}
	
	public void TakeDamage(int damage)
    {
		currentHP -= damage;
		if (currentHP <= 0)
			GameObject.FindObjectOfType<GameManager>().buildingManager.DestroyBuilding(this);
		healthbar.UpdateBuildingLife();
		healthbar.OnDamageAnim(damage);
    }

	public void Recover()
    {
		if(currentHP<maxHP)
		{
			healthbar.OnRecoverAnim(GameConfig.BuildingRecoverEachTurn[currentLevel]);
		}
		int hp = currentHP + GameConfig.BuildingRecoverEachTurn[currentLevel];
		if (currentHP > maxHP)
			currentHP = maxHP;

		healthbar.UpdateBuildingLife();
		
    }
}


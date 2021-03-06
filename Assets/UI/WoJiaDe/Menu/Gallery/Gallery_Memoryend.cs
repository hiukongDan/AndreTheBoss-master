﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gallery_Memoryend : MonoBehaviour
{
    public int memoryId;
	
	public Text name;
	public Text content;
	
	public MemoryReader reader;
	
	public void UpdateItem()
	{
		name.text="Memory"+memoryId;
		
		if(memoryId>5)
		content.text=reader.GetEventMemoryData(memoryId);
		else
			content.text=reader.GetNormalMemoryData(memoryId);
	}
}

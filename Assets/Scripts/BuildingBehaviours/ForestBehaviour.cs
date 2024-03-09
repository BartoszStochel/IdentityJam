using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestBehaviour : BuildingBehaviour
{
	public int CurrentResources { get; private set; }

	public ForestBehaviour(BuildingData newBuildingData) : base(newBuildingData)
	{
	}
}

using UnityEngine;
using System.Collections.Generic;

public abstract class ActionBuildingData : BuildingData
{
	[field: SerializeField] public int ExtendedRangeInBothWays { get; private set; }
	[field: SerializeField] public float ActionTimeInterval { get; private set; }
	[field: SerializeField] public int TakenResources { get; private set; }

	
}

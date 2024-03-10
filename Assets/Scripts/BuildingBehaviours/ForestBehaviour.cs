using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestBehaviour : BuildingBehaviour
{
	public int CurrentResources { get; private set; }

	public ForestBehaviour(ForestData newBuildingData) : base(newBuildingData)
	{
		CurrentResources = newBuildingData.StartingWood;
	}

	public int DepleteResources(int howMuchToDeplete)
	{
		if (howMuchToDeplete > CurrentResources)
		{
			var howMuchWasReallyDepleted = CurrentResources;
			CurrentResources = 0;

			TriggerBehaviourDeath();

			return howMuchWasReallyDepleted;
		}

		CurrentResources -= howMuchToDeplete;

		if (CurrentResources <= 0)
		{
			TriggerBehaviourDeath();
		}

		return howMuchToDeplete;
	}
}

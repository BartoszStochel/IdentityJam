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
		// todo - podœwietlanie zasiêgu przy budowaniu i póŸniej po najechaniu na budynek

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

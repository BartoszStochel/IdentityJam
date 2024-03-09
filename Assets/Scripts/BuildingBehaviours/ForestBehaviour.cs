using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestBehaviour : BuildingBehaviour
{
	public int CurrentResources { get; private set; }

	public ForestBehaviour(BuildingData newBuildingData) : base(newBuildingData)
	{
	}

	public int DepleteResources(int howMuchToDeplete)
	{
		// TODO - uzupe³nianie vurrentresources na starcie
		// TODO - niszczenie budynku i  odpinanie refek na budynek
		// TODO - stworzenie ForestData

		if (howMuchToDeplete > CurrentResources)
		{
			var howMuchWasReallyDepleted = CurrentResources;
			CurrentResources = 0;
			return howMuchWasReallyDepleted;
		}

		CurrentResources -= howMuchToDeplete;
		return howMuchToDeplete;
	}
}

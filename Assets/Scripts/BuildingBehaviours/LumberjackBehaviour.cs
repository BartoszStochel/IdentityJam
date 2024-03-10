using System.Collections.Generic;
using UnityEngine;

public class LumberjackBehaviour : RangedBuildingBehaviour
{
	public LumberjackBehaviour(BuildingData newBuildingData, List<Field> newFieldsInRange, ResourcesManager newResourcesManager) : base(newBuildingData, newFieldsInRange)
	{
		BuildingData = newBuildingData as LumberjackData;
		resourcesManager = newResourcesManager;

		timer = BuildingData.ActionTimeInterval;
	}

	private ResourcesManager resourcesManager;

	private float timer;

	public override void OnUpdateBehaviour()
	{
		timer -= Time.deltaTime;

		if (timer > 0f)
		{
			return;
		}

		foreach (var field in fieldsInRange)
		{
			if (field.BuildingOnField == null || field.BuildingOnField.Behaviour is not ForestBehaviour forest)
			{
				continue;
			}

			var takenResources = forest.DepleteResources(BuildingData.TakenResources);

			resourcesManager.ModifyWood(takenResources);
			timer = BuildingData.ActionTimeInterval;
			return;
		}
	}
}

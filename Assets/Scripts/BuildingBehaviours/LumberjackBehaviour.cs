using System.Collections.Generic;
using UnityEngine;

public class LumberjackBehaviour : RangedBuildingBehaviour
{
	public LumberjackBehaviour(BuildingData newBuildingData, List<Field> newFieldsInRange) : base(newBuildingData, newFieldsInRange)
	{
	}

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

			var takenResources = forest.DepleteResources(buildingData.TakenResources);

			// TODO - dodawanko do resources
		}
	}
}

using System.Collections.Generic;
using UnityEngine;

public abstract class ActionBuildingBehaviour : BuildingBehaviour
{
	new public ActionBuildingData BuildingData { get; protected set; }
	protected List<Field> fieldsInRange;
	protected float timer;

	public ActionBuildingBehaviour(BuildingData newBuildingData, List<Field> newFieldsInRange) : base(newBuildingData)
	{
		fieldsInRange = newFieldsInRange;
	}

	public override void OnUpdateBehaviour()
	{
		timer -= Time.deltaTime;

		if (timer > 0f)
		{
			return;
		}

		foreach (var field in fieldsInRange)
		{
			if (TryProcessField(field))
			{
				timer = BuildingData.ActionTimeInterval;
				return;
			}
		}
	}

	protected abstract bool TryProcessField(Field field);
}

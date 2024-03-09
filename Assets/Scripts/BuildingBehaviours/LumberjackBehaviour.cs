using System.Collections.Generic;

public class LumberjackBehaviour : RangedBuildingBehaviour
{
	public LumberjackBehaviour(BuildingData newBuildingData, List<Field> newFieldsInRange) : base(newBuildingData, newFieldsInRange)
	{
	}

	private float timer;

	public override void OnUpdateBehaviour()
	{
		
	}
}

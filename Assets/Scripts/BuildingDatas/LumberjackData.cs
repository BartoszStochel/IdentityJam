using UnityEngine;

[CreateAssetMenu(fileName = nameof(LumberjackData), menuName = "BuildingData/" + nameof(LumberjackData))]
public class LumberjackData : ActionBuildingData
{
	public override BuildingBehaviour GetBuildingBehaviour(Field originField, Field[,] fields, ResourcesManager resourcesManager)
	{
		return new LumberjackBehaviour(this, fields.GetFieldsInRange(originField, ExtendedRangeInBothWays), resourcesManager);
	}
}

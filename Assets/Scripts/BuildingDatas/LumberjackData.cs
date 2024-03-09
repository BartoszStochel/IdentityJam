using UnityEngine;

[CreateAssetMenu(fileName = nameof(LumberjackData), menuName = "BuildingData/" + nameof(LumberjackData))]
public class LumberjackData : ActionBuildingData
{
	public override BuildingBehaviour GetBuildingBehaviour(Field originField, Field[,] fields)
	{
		return new LumberjackBehaviour(this, GetFieldsInRange(originField, fields));
	}
}

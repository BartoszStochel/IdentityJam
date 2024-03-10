using UnityEngine;

[CreateAssetMenu(fileName = nameof(GeologistData), menuName = "BuildingData/" + nameof(GeologistData))]
public class GeologistData : ActionBuildingData
{
	public override BuildingBehaviour GetBuildingBehaviour(Field originField, Field[,] fields, ResourcesManager resourcesManager)
	{
		return new GeologistBehaviour(this, fields.GetFieldsInRange(originField, ExtendedRangeInBothWays));
	}
}

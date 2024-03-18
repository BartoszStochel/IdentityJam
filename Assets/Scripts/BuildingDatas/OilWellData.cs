using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(OilWellData), menuName = "BuildingData/" + nameof(OilWellData))]
public class OilWellData : ActionBuildingData
{
	public override BuildingBehaviour GetBuildingBehaviour(Field originField, List<List<Field>> fields, ResourcesManager resourcesManager)
	{
		return new OilWellBehaviour(this, fields.GetFieldsInRange(originField, ExtendedRangeInBothWays), resourcesManager);
	}
}

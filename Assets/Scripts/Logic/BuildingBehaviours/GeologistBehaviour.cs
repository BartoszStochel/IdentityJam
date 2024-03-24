using System.Collections.Generic;

public class GeologistBehaviour : ActionBuildingBehaviour
{
	public GeologistBehaviour(BuildingData newBuildingData, List<Field> newFieldsInRange) : base(newBuildingData, newFieldsInRange)
	{
		BuildingData = newBuildingData as GeologistData;
		timer = BuildingData.ActionTimeInterval;
	}

	protected override bool TryProcessField(Field field)
	{
		if (field.DiscoveredOilSlots == 1)
		{
			return false;
		}

		field.SetDiscoveredOilSlots(1);
		return true;
	}
}

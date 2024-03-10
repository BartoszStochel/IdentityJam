using System.Collections.Generic;

public class GeologistBehaviour : ActionBuildingBehaviour
{
	public GeologistBehaviour(BuildingData newBuildingData, List<Field> newFieldsInRange) : base(newBuildingData, newFieldsInRange)
	{
		BuildingData = newBuildingData as GeologistData;
	}

	protected override bool TryProcessField(Field field)
	{
		if (field.DiscoveredOilSlots == 4)
		{
			return false;
		}

		field.SetDiscoveredOilSlots(4);
		return true;
	}
}

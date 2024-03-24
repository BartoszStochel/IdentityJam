using System.Collections.Generic;

public class OilWellBehaviour : ActionBuildingBehaviour
{
	public OilWellBehaviour(BuildingData newBuildingData, List<Field> newFieldsInRange, ResourcesManager newResourcesManager) : base(newBuildingData, newFieldsInRange)
	{
		BuildingData = newBuildingData as OilWellData;
		resourcesManager = newResourcesManager;
		timer = BuildingData.ActionTimeInterval;
	}

	private ResourcesManager resourcesManager;

	protected override bool TryProcessField(Field field)
	{
		if (field.DiscoveredOilSlots != 1)
		{
			return false;
		}

		var takenResources = field.DepleteOilSlot(0, BuildingData.TakenResources);

		if (takenResources == 0)
		{
			return false;
		}

		resourcesManager.ModifyCrude(takenResources);
		return true;
	}
}

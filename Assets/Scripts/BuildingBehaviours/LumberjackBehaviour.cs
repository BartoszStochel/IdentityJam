using System.Collections.Generic;

public class LumberjackBehaviour : ActionBuildingBehaviour
{
	public LumberjackBehaviour(BuildingData newBuildingData, List<Field> newFieldsInRange, ResourcesManager newResourcesManager) : base(newBuildingData, newFieldsInRange)
	{
		BuildingData = newBuildingData as LumberjackData;
		resourcesManager = newResourcesManager;

		timer = BuildingData.ActionTimeInterval;
	}

	private ResourcesManager resourcesManager;

	protected override bool TryProcessField(Field field)
	{
		if (field.BuildingOnField == null || field.BuildingOnField.Behaviour is not ForestBehaviour forest)
		{
			return false;
		}

		var takenResources = forest.DepleteResources(BuildingData.TakenResources);

		resourcesManager.ModifyWood(takenResources);
		return true;
	}
}

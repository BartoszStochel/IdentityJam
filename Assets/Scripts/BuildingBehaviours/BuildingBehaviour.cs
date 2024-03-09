public abstract class BuildingBehaviour
{
	protected BuildingData buildingData;

	public virtual void OnUpdateBehaviour()
	{
	}

	public BuildingBehaviour(BuildingData newBuildingData)
	{
		buildingData = newBuildingData;
	}
}

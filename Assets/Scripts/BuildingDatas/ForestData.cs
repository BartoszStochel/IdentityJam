using UnityEngine;

[CreateAssetMenu(fileName = nameof(ForestData), menuName = "BuildingData/" + nameof(ForestData))]
public class ForestData : BuildingData
{
	[field: SerializeField] public int StartingWood { get; private set; }
	public override BuildingBehaviour GetBuildingBehaviour(Field originField, Field[,] fields, ResourcesManager resourcesManager)
	{
		return new ForestBehaviour(this);
	}
}

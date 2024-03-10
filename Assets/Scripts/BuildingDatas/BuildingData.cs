using UnityEngine;

[CreateAssetMenu(fileName = nameof(BuildingData), menuName = "BuildingData/Generic" + nameof(BuildingData))]
public class BuildingData : ScriptableObject
{
	[field: SerializeField] public Sprite Sprite { get; private set; }
	[field: SerializeField] public string NameToDisplay { get; private set; }
	[field: SerializeField] public int MoneyCost { get; private set; }
	[field: SerializeField] public int WoodCost { get; private set; }

	public virtual BuildingBehaviour GetBuildingBehaviour(Field originField, Field[,] fields, ResourcesManager newResourceManager)
	{
		return null;
	}
}

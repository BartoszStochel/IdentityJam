using UnityEngine;

[CreateAssetMenu(fileName = nameof(BuildingData), menuName = nameof(BuildingData))]
public class BuildingData : ScriptableObject
{
	[field: SerializeField] public Sprite Sprite { get; private set; }
	[field: SerializeField] public string NameToDisplay { get; private set; }
	[field: SerializeField] public int MoneyCost { get; private set; }
	[field: SerializeField] public int WoodCost { get; private set; }
}

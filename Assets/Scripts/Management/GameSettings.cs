using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(GameSettings), menuName = nameof(GameSettings))]
public class GameSettings : ScriptableObject
{
	// TODO - move more balance fields here

	[field: SerializeField] public List<FieldsLayer> FieldsLayers { get; private set; }
	[field: SerializeField] public List<BuildingData> BuildingDatasToBuildByPlayer { get; private set; }
	[field: SerializeField] public int NumberOfRowsInMap { get; private set; }
	[field: SerializeField] public int SpacingBetweenRows { get; private set; }
	[field: SerializeField] public int BottomRowYPosition { get; private set; }
}

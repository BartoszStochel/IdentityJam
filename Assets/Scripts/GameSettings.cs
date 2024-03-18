using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(GameSettings), menuName = nameof(GameSettings))]
public class GameSettings : ScriptableObject
{
	// TODO - move more balance fields here

	[field: SerializeField] public List<BuildingData> BuildingDatasToBuildByPlayer { get; private set; }
}

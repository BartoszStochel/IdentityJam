using UnityEngine;

[CreateAssetMenu(fileName = nameof(BuildingState), menuName = "GameStates/" + nameof(BuildingState))]
public class BuildingState : GameState
{
	[SerializeField] private Building buildingPrefab;

	private BuildingData currentlySelectedBuildingData;
	private Field[,] fields;

	public void Initialize(Field[,] newFields)
	{
		fields = newFields;
	}

	public void SetCurrentlySelectedBuildingData(BuildingData newBuildingData)
	{
		currentlySelectedBuildingData = newBuildingData;
	}

	public override void OnStateEntered()
	{
		foreach (var field in fields)
		{
			field.TryActivateCanBuildIndicator();
		}
	}

	public override void OnStateExited()
	{
		foreach (var field in fields)
		{
			field.DeactivateCanBuildIndicator();
		}
	}

	public override void OnFieldClicked(Field field)
	{
		var building = Instantiate(buildingPrefab, field.transform);
		building.Initialize(currentlySelectedBuildingData);
		field.SetBuilding(building);

		RequestExitFromThisState?.Invoke(this);
	}
}

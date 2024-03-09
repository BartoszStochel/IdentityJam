using UnityEngine;

[CreateAssetMenu(fileName = nameof(BuildingState), menuName = "GameStates/" + nameof(BuildingState))]
public class BuildingState : GameState
{
	[SerializeField] private Building buildingPrefab;

	private BuildingToBuildButton currentlySelectedBuildingButton;
	private Field[,] fields;

	public void Initialize(Field[,] newFields)
	{
		fields = newFields;
	}

	public void SetCurrentlySelectedBuildingButton(BuildingToBuildButton button)
	{
		currentlySelectedBuildingButton = button;
	}

	public override void OnStateEntered()
	{
		foreach (var field in fields)
		{
			field.TryActivateCanBuildIndicator();
		}

		currentlySelectedBuildingButton.SetCurrentlySelectedIndicatorActivity(true);
	}

	public override void OnStateExited()
	{
		foreach (var field in fields)
		{
			field.DeactivateCanBuildIndicator();
		}

		currentlySelectedBuildingButton.SetCurrentlySelectedIndicatorActivity(false);
	}

	public override void OnFieldClicked(Field field)
	{
		var building = Instantiate(buildingPrefab, field.transform);
		building.Initialize(currentlySelectedBuildingButton.buildingData);
		field.SetBuilding(building);

		RequestExitFromThisState?.Invoke(this);
	}
}

using UnityEngine;

[CreateAssetMenu(fileName = nameof(BuildingState), menuName = "GameStates/" + nameof(BuildingState))]
public class BuildingState : GameState
{
	[SerializeField] private Building buildingPrefab;

	private BuildingToBuildButton currentlySelectedBuildingButton;

	private Field[,] fields;
	private ResourcesManager resourcesManager;

	public void Initialize(Field[,] newFields, ResourcesManager newResourcesManager)
	{
		fields = newFields;
		resourcesManager = newResourcesManager;
	}

	public void SetCurrentlySelectedBuildingButton(BuildingToBuildButton button)
	{
		if (currentlySelectedBuildingButton != null)
		{
			currentlySelectedBuildingButton.SetCurrentlySelectedIndicatorActivity(false);
		}

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
		resourcesManager.ModifyMoney(-currentlySelectedBuildingButton.buildingData.MoneyCost);
		resourcesManager.ModifyWood(-currentlySelectedBuildingButton.buildingData.WoodCost);

		var building = Instantiate(buildingPrefab, field.transform);
		building.Initialize(currentlySelectedBuildingButton.buildingData);
		field.SetBuilding(building);

		RequestExitFromThisState?.Invoke(this);
	}
}

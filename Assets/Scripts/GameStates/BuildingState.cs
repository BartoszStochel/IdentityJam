using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(BuildingState), menuName = "GameStates/" + nameof(BuildingState))]
public class BuildingState : GameState
{
	[SerializeField] private Building buildingPrefab;

	private BuildingToBuildButton currentlySelectedBuildingButton;

	private List<List<Field>> fieldsRows;
	private ResourcesManager resourcesManager;

	public void Initialize(List<List<Field>> newFields, ResourcesManager newResourcesManager, FieldsRangeIndicatorsManager newFieldsRangeIndicatorsManager)
	{
		fieldsRows = newFields;
		resourcesManager = newResourcesManager;
		fieldsRangeIndicatorsManager = newFieldsRangeIndicatorsManager;
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
		foreach (var row in fieldsRows)
		{
			foreach (var field in row)
			{
				field.TryActivateCanBuildIndicator();
			}
		}

		currentlySelectedBuildingButton.SetCurrentlySelectedIndicatorActivity(true);
	}

	public override void OnStateExited()
	{
		foreach (var row in fieldsRows)
		{
			foreach (var field in row)
			{
				field.DeactivateCanBuildIndicator();
			}
		}

		currentlySelectedBuildingButton.SetCurrentlySelectedIndicatorActivity(false);
		fieldsRangeIndicatorsManager.ClearRangeIndicators();
	}

	public override void OnFieldClicked(Field field)
	{
		resourcesManager.ModifyMoney(-currentlySelectedBuildingButton.buildingData.MoneyCost);
		resourcesManager.ModifyWood(-currentlySelectedBuildingButton.buildingData.WoodCost);
		resourcesManager.ModifyCrude(-currentlySelectedBuildingButton.buildingData.CrudeCost);

		PlaceBuildingOnField(currentlySelectedBuildingButton.buildingData, field);

		RequestExitFromThisState?.Invoke(this);
	}

	public void PlaceBuildingOnField(BuildingData buildingData, Field field)
	{
		var building = Instantiate(buildingData.Prefab, field.transform);
		var sortingOrderForBuilding = field.GetComponentInParent<Canvas>().sortingOrder - 1;
		building.Initialize(buildingData.GetBuildingBehaviour(field, fieldsRows, resourcesManager), sortingOrderForBuilding);
		field.SetBuilding(building);
	}

	public override void OnFieldHoverStart(Field hoveredField)
	{
		if (currentlySelectedBuildingButton.buildingData is not ActionBuildingData actionData)
		{
			return;
		}

		fieldsRangeIndicatorsManager.ActivateRangeIndicators(hoveredField, actionData.ExtendedRangeInBothWays);
	}

	public override void OnFieldHoverEnd(Field hoveredField)
	{
		fieldsRangeIndicatorsManager.ClearRangeIndicators();
	}
}

using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = nameof(BuildingState), menuName = "GameStates/" + nameof(BuildingState))]
public class BuildingState : GameState
{
	[SerializeField] private Building buildingPrefab;

	private BuildingToBuildButton currentlySelectedBuildingButton;

	private Field[,] fields;
	private ResourcesManager resourcesManager;

	public void Initialize(Field[,] newFields, ResourcesManager newResourcesManager, FieldsRangeIndicatorsManager newFieldsRangeIndicatorsManager)
	{
		fields = newFields;
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
		var building = Instantiate(buildingPrefab, field.transform);
		building.Initialize(buildingData, buildingData.GetBuildingBehaviour(field, fields, resourcesManager));
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

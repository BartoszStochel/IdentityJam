using UnityEngine;

[CreateAssetMenu(fileName = nameof(DefaultState), menuName = "GameStates/" + nameof(DefaultState))]
public class DefaultState : GameState
{
	public void Initialize(FieldsRangeIndicatorsManager newFieldsRangeIndicatorsManager)
	{
		fieldsRangeIndicatorsManager = newFieldsRangeIndicatorsManager;
	}

	public override void OnStateEntered()
	{

	}

	public override void OnStateExited()
	{
		fieldsRangeIndicatorsManager.ClearRangeIndicators();
	}

	public override void OnFieldClicked(Field field)
	{
		
	}

	public override void OnFieldHoverStart(Field field)
	{

		if (field.BuildingOnField == null || field.BuildingOnField.Behaviour is not RangedBuildingBehaviour rangedBehaviour)
		{
			return;
		}

		fieldsRangeIndicatorsManager.ActivateRangeIndicators(field, rangedBehaviour.BuildingData.ExtendedRangeInBothWays);
	}

	public override void OnFieldHoverEnd(Field field)
	{
		fieldsRangeIndicatorsManager.ClearRangeIndicators();
	}
}

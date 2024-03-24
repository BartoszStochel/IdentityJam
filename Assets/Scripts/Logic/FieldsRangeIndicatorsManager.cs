using System.Collections.Generic;

public class FieldsRangeIndicatorsManager
{
	private List<List<Field>> fields;
	private List<Field> currentlyHighlightedFieldsInRange = new List<Field>();
	private Field currentRangeOriginField;

	public FieldsRangeIndicatorsManager(List<List<Field>> newFields)
	{
		fields = newFields;
	}

	public void ActivateRangeIndicators(Field originField, int extendedRangeInBothWays)
	{
		currentRangeOriginField = originField;
		currentRangeOriginField.BuildingOnThisFieldDestroyed += OnBuildingOnThisFieldDestroyed;
		var fieldsInRange = fields.GetFieldsInRange(currentRangeOriginField, extendedRangeInBothWays);

		foreach (var field in fieldsInRange)
		{
			field.SetRangeIndicatorActivity(true);

			currentlyHighlightedFieldsInRange.Add(field);
		}
	}

	public void ClearRangeIndicators()
	{
		if (currentRangeOriginField is not null)
		{
			currentRangeOriginField.BuildingOnThisFieldDestroyed -= OnBuildingOnThisFieldDestroyed;
		}

		currentRangeOriginField = null;

		foreach (var field in currentlyHighlightedFieldsInRange)
		{
			field.SetRangeIndicatorActivity(false);
		}

		currentlyHighlightedFieldsInRange.Clear();
	}

	private void OnBuildingOnThisFieldDestroyed(Field field)
	{
		ClearRangeIndicators();
	}
}

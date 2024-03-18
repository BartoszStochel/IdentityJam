using System.Collections.Generic;

public class FieldsRangeIndicatorsManager
{
	private List<List<Field>> fields;
	private List<Field> currentlyHighlightedFieldsInRange = new List<Field>();

	public FieldsRangeIndicatorsManager(List<List<Field>> newFields)
	{
		fields = newFields;
	}

	public void ActivateRangeIndicators(Field originField, int extendedRangeInBothWays)
	{
		var fieldsInRange = fields.GetFieldsInRange(originField, extendedRangeInBothWays);

		foreach (var field in fieldsInRange)
		{
			field.SetRangeIndicatorActivity(true);

			currentlyHighlightedFieldsInRange.Add(field);
		}
	}

	public void ClearRangeIndicators()
	{
		foreach (var field in currentlyHighlightedFieldsInRange)
		{
			field.SetRangeIndicatorActivity(false);
		}

		currentlyHighlightedFieldsInRange.Clear();
	}
}

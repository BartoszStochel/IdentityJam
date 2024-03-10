using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldsRangeIndicatorsManager
{
	private Field[,] fields;
	private List<Field> currentlyHighlightedFieldsInRange = new List<Field>();

	public FieldsRangeIndicatorsManager(Field[,] newFields)
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

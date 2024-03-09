using UnityEngine;
using System.Collections.Generic;

public abstract class ActionBuildingData : BuildingData
{
	[field: SerializeField] public int ExtendedRangeInBothWays { get; private set; }
	[field: SerializeField] public float ActionTimeInterval { get; private set; }
	[field: SerializeField] public int TakenResources { get; private set; }

	protected virtual List<Field> GetFieldsInRange(Field originField, Field[,] fields)
	{
		var fieldsInRange = new List<Field>();
		fieldsInRange.Add(originField);
		var mapXSize = fields.GetLength(0);

		for (int i = 1; i <= ExtendedRangeInBothWays; i++)
		{
			var consideredXPosition = originField.XPosition + i;

			if (consideredXPosition >= 0 && consideredXPosition < mapXSize)
			{
				fieldsInRange.Add(fields[consideredXPosition, originField.YPosition]);
			}

			consideredXPosition = originField.XPosition - i;

			if (consideredXPosition >= 0 && consideredXPosition < mapXSize)
			{
				fieldsInRange.Add(fields[consideredXPosition, originField.YPosition]);
			}
		}

		return fieldsInRange;
	}
}

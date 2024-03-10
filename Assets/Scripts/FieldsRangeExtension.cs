using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FieldsRangeExtension
{
	public static List<Field> GetFieldsInRange(this Field[,] fields, Field originField, int extendedRangeInBothWays)
	{
		var fieldsInRange = new List<Field>();
		fieldsInRange.Add(originField);
		var mapXSize = fields.GetLength(0);

		for (int i = 1; i <= extendedRangeInBothWays; i++)
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

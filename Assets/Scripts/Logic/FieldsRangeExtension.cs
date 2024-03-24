using System.Collections.Generic;

public static class FieldsRangeExtension
{
	public static List<Field> GetFieldsInRange(this List<List<Field>> fields, Field originField, int extendedRangeInBothWays)
	{
		var fieldsInRange = new List<Field>();
		fieldsInRange.Add(originField);
		var rowXSize = fields[originField.YPosition].Count;

		for (int i = 1; i <= extendedRangeInBothWays; i++)
		{
			var consideredXPosition = originField.XPosition + i;

			if (consideredXPosition >= 0 && consideredXPosition < rowXSize)
			{
				fieldsInRange.Add(fields[originField.YPosition][consideredXPosition]);
			}

			consideredXPosition = originField.XPosition - i;

			if (consideredXPosition >= 0 && consideredXPosition < rowXSize)
			{
				fieldsInRange.Add(fields[originField.YPosition][consideredXPosition]);
			}
		}

		return fieldsInRange;
	}
}

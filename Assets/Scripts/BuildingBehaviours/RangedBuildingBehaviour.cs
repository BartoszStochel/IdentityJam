using System.Collections.Generic;

public abstract class RangedBuildingBehaviour : BuildingBehaviour
{
	new public ActionBuildingData BuildingData { get; protected set; }
	protected List<Field> fieldsInRange;

	public RangedBuildingBehaviour(BuildingData newBuildingData, List<Field> newFieldsInRange) : base(newBuildingData)
	{
		fieldsInRange = newFieldsInRange;
	}
}

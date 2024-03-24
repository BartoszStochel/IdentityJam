using System;
using UnityEngine;

public abstract class GameState : ScriptableObject
{
	public abstract void OnStateEntered(Field currentlyHoveredField);
	public abstract void OnStateExited();
	public abstract void OnFieldClicked(Field field);
	public abstract void OnFieldHoverStart(Field field);
	public abstract void OnFieldHoverEnd(Field field);

	public Action<GameState> RequestExitFromThisState;

	protected FieldsRangeIndicatorsManager fieldsRangeIndicatorsManager;
}

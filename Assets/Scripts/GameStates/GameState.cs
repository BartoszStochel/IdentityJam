using System;
using UnityEngine;

public abstract class GameState : ScriptableObject
{
	public abstract void OnStateEntered();
	public abstract void OnStateExited();
	public abstract void OnFieldClicked(Field field);

	public Action<GameState> RequestExitFromThisState;
}
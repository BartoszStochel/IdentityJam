using System;

public abstract class BuildingBehaviour
{
	public event Action BehaviourRequestsDeath;

	public BuildingData BuildingData { get; protected set; }

	public virtual void OnUpdateBehaviour()
	{
	}

	public BuildingBehaviour(BuildingData newBuildingData)
	{
		if (newBuildingData == null)
		{
			UnityEngine.Debug.Log("dupa1");
		}

		BuildingData = newBuildingData;

		if (BuildingData == null)
		{
			UnityEngine.Debug.Log("dupa2");
		}
	}

	protected void TriggerBehaviourDeath()
	{
		BehaviourRequestsDeath?.Invoke();
	}
}

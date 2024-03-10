using System;

public abstract class BuildingBehaviour
{
	public event Action BehaviourRequestsDeath;

	protected BuildingData buildingData;

	public virtual void OnUpdateBehaviour()
	{
	}

	public BuildingBehaviour(BuildingData newBuildingData)
	{
		if (newBuildingData == null)
		{
			UnityEngine.Debug.Log("dupa1");
		}

		buildingData = newBuildingData;

		if (buildingData == null)
		{
			UnityEngine.Debug.Log("dupa2");
		}
	}

	protected void TriggerBehaviourDeath()
	{
		BehaviourRequestsDeath?.Invoke();
	}
}

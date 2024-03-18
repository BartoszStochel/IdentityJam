using UnityEngine;
using System;

public class Building : MonoBehaviour
{
	public BuildingBehaviour Behaviour { get; private set; }

	public event Action BuildingDestroyed;

	private BuildingData data;

	private void Update()
	{
		if (Behaviour != null)
		{
			Behaviour.OnUpdateBehaviour();
		}
	}

	public void Initialize(BuildingData newData, BuildingBehaviour newBehaviour, int sortingOrder)
	{
		data = newData;
		Behaviour = newBehaviour;
		Behaviour.BehaviourRequestsDeath += OnBehaviourDeath;

		var sprites = GetComponentsInChildren<SpriteRenderer>();

		for (int i = 0; i < sprites.Length; i++)
		{
			sprites[i].sortingOrder = sprites[i].sortingOrder + sortingOrder;
		}
	}

	private void OnBehaviourDeath()
	{
		BuildingDestroyed?.Invoke();
	}
}

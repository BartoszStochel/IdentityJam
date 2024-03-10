using UnityEngine;
using System;

public class Building : MonoBehaviour
{
	[SerializeField] private SpriteRenderer Sprite;

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

		Sprite.sprite = data.Sprite;
		Sprite.sortingOrder = sortingOrder;
	}

	private void OnBehaviourDeath()
	{
		BuildingDestroyed?.Invoke();
	}
}

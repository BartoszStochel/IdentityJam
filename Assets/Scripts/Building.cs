using UnityEngine;
using UnityEngine.UI;
using System;

public class Building : MonoBehaviour
{
	[SerializeField] private Image Image;

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

	public void Initialize(BuildingData newData, BuildingBehaviour newBehaviour)
	{
		data = newData;
		Behaviour = newBehaviour;
		Behaviour.BehaviourRequestsDeath += OnBehaviourDeath;

		Image.sprite = data.Sprite;
	}

	private void OnBehaviourDeath()
	{
		BuildingDestroyed?.Invoke();
	}
}

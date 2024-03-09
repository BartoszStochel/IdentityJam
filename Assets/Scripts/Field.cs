using System;
using UnityEngine;
using UnityEngine.UI;

public class Field : MonoBehaviour
{
	[SerializeField] private GameObject canBuildIndicator;

	private Building buildingOnField;

	public Action<Field> ButtonClicked;

	private void Start()
	{

		GetComponentInChildren<Button>().onClick.AddListener(OnButtonClicked);
	}

	public void SetBuilding(Building newBuilding)
	{
		buildingOnField = newBuilding;
	}

	private void OnButtonClicked()
	{
		ButtonClicked?.Invoke(this);
	}

	public void TryActivateCanBuildIndicator()
	{
		canBuildIndicator.SetActive(buildingOnField == null);
	}

	public void DeactivateCanBuildIndicator()
	{
		canBuildIndicator.SetActive(false);
	}
}

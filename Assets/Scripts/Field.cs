using System;
using UnityEngine;
using UnityEngine.UI;

public class Field : MonoBehaviour
{
	[SerializeField] private GameObject canBuildIndicator;

	private Building buildingOnField;
	private Button button;

	public Action<Field> ButtonClicked;

	private void Start()
	{
		button = GetComponentInChildren<Button>();
		button.onClick.AddListener(OnButtonClicked);
		button.enabled = false;
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
		var isFieldFree = buildingOnField == null;

		canBuildIndicator.SetActive(isFieldFree);
		button.enabled = isFieldFree;
	}

	public void DeactivateCanBuildIndicator()
	{
		canBuildIndicator.SetActive(false);
		button.enabled = false;
	}
}

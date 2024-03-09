using System;
using UnityEngine;
using UnityEngine.UI;

public class Field : MonoBehaviour
{
	[SerializeField] private GameObject canBuildIndicator;

	public int XPosition { get; private set; }
	public int YPosition { get; private set; }
	public Building BuildingOnField { get; private set; }

	private Button button;

	public Action<Field> ButtonClicked;

	public void Initialize(int xPosition, int yPosition)
	{
		XPosition = xPosition;
		YPosition = yPosition;

		name = $"Field {XPosition}, {YPosition}";

		button = GetComponentInChildren<Button>();
		button.onClick.AddListener(OnButtonClicked);
		button.enabled = false;
	}

	public void SetBuilding(Building newBuilding)
	{
		BuildingOnField = newBuilding;
	}

	private void OnButtonClicked()
	{
		ButtonClicked?.Invoke(this);
	}

	public void TryActivateCanBuildIndicator()
	{
		var isFieldFree = BuildingOnField == null;

		canBuildIndicator.SetActive(isFieldFree);
		button.enabled = isFieldFree;
	}

	public void DeactivateCanBuildIndicator()
	{
		canBuildIndicator.SetActive(false);
		button.enabled = false;
	}
}

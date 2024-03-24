using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Field : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] private GameObject canBuildIndicator;
	[SerializeField] private GameObject inRangeIndicator;
	[SerializeField] private List<TextMeshProUGUI> OilSlots;
	[SerializeField] private Button mainButton;
	[SerializeField] private Button destroyButton;

	public int XPosition { get; private set; }
	public int YPosition { get; private set; }
	public Building BuildingOnField { get; private set; }
	public List<int> Oil { get; private set; }
	public int DiscoveredOilSlots { get; private set; }

	public Action<Field> ButtonClicked;
	public Action<Field> HoverStart;
	public Action<Field> HoverEnd;
	public Action<Field> BuildingOnThisFieldDestroyed;

	public void Initialize(int xPosition, int yPosition, List<int> oil)
	{
		XPosition = xPosition;
		YPosition = yPosition;

		Oil = oil;

		name = $"Field {XPosition}, {YPosition}";

		mainButton.onClick.AddListener(OnButtonClicked);
		mainButton.enabled = false;

		destroyButton.onClick.AddListener(OnBuildingDeath);
		destroyButton.gameObject.SetActive(false);
	}

	public void SetDiscoveredOilSlots(int newDiscoveryLevel)
	{
		DiscoveredOilSlots = newDiscoveryLevel;

		for (int i = 0; i < DiscoveredOilSlots; i++)
		{
			OilSlots[i].text = Oil[i].ToString();
			OilSlots[i].transform.parent.gameObject.SetActive(true);
		}
	}

	public int DepleteOilSlot(int oilSlotIndex, int howMuchToDeplete)
	{
		if (howMuchToDeplete > Oil[oilSlotIndex])
		{
			var howMuchWasReallyDepleted = Oil[oilSlotIndex];
			Oil[oilSlotIndex] = 0;

			return howMuchWasReallyDepleted;
		}

		Oil[oilSlotIndex] -= howMuchToDeplete;
		OilSlots[oilSlotIndex].text = Oil[oilSlotIndex].ToString();
		return howMuchToDeplete;
	}

	public void SetBuilding(Building newBuilding)
	{
		BuildingOnField = newBuilding;
		BuildingOnField.BuildingDestroyed += OnBuildingDeath;
	}

	private void OnBuildingDeath()
	{
		SetOperationButtonsActivity(false);
		BuildingOnField.BuildingDestroyed -= OnBuildingDeath;
		Destroy(BuildingOnField.gameObject);
		BuildingOnField = null;
		BuildingOnThisFieldDestroyed?.Invoke(this);
	}

	private void OnButtonClicked()
	{
		ButtonClicked?.Invoke(this);
	}

	public void TryActivateCanBuildIndicator()
	{
		var isFieldFree = BuildingOnField == null;

		canBuildIndicator.SetActive(isFieldFree);
		mainButton.enabled = isFieldFree;
	}

	public void DeactivateCanBuildIndicator()
	{
		canBuildIndicator.SetActive(false);
		mainButton.enabled = false;
	}

	public void SetRangeIndicatorActivity(bool shouldItBeActive)
	{
		inRangeIndicator.SetActive(shouldItBeActive);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		HoverStart?.Invoke(this);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		HoverEnd?.Invoke(this);
	}

	public void SetOperationButtonsActivity(bool shouldBeActive)
	{
		destroyButton.gameObject.SetActive(shouldBeActive);
		// tutaj jeszcze ten od upgrejdu
	}
}

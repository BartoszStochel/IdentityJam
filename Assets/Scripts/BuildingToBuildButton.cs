using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BuildingToBuildButton : MonoBehaviour
{
	[SerializeField] private Image sprite;
	[SerializeField] private TextMeshProUGUI buildingName;
	[SerializeField] private TextMeshProUGUI moneyCost;
	[SerializeField] private TextMeshProUGUI woodCost;
	[SerializeField] private GameObject currentlySelectedIndicator;
	[SerializeField] private GameObject notEnoughResourcesIndicator;

	private Button button;

	public BuildingData buildingData { get; private set; }

	public Action<BuildingToBuildButton> ButtonClicked;

	public void Initialize(BuildingData data)
	{
		buildingData = data;

		sprite.sprite = buildingData.Sprite;
		buildingName.text = buildingData.NameToDisplay;
		moneyCost.text = buildingData.MoneyCost.ToString();
		woodCost.text = buildingData.WoodCost.ToString();

		moneyCost.gameObject.SetActive(buildingData.MoneyCost > 0);
		woodCost.gameObject.SetActive(buildingData.WoodCost > 0);

		button = GetComponentInChildren<Button>();
		button.onClick.AddListener(OnButtonClicked);
	}

	public void SetCurrentlySelectedIndicatorActivity(bool shouldBeActive)
	{
		currentlySelectedIndicator.SetActive(shouldBeActive);
	}

	public void SetNotEnoughResourcesIndicatorActivity(bool shouldBeActive)
	{
		notEnoughResourcesIndicator.SetActive(shouldBeActive);
		button.enabled = !shouldBeActive;
	}

	private void OnButtonClicked()
	{
		ButtonClicked?.Invoke(this);
	}
}

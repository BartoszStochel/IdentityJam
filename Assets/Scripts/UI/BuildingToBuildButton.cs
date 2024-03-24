using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BuildingToBuildButton : MonoBehaviour
{
	[SerializeField] private Image sprite;
	[SerializeField] private TextMeshProUGUI buildingName;
	[SerializeField] private TextMeshProUGUI firstCost;
	[SerializeField] private TextMeshProUGUI secondCost;
	[SerializeField] private GameObject currentlySelectedIndicator;
	[SerializeField] private GameObject notEnoughResourcesIndicator;

	private Button button;

	public BuildingData buildingData { get; private set; }

	public Action<BuildingToBuildButton> ButtonClicked;

	public void Initialize(BuildingData data)
	{
		buildingData = data;

		buildingName.text = buildingData.NameToDisplay;

		if (buildingData.WoodCost > 0)
		{
			firstCost.text = buildingData.WoodCost.ToString();
		}
		else
		{
			firstCost.text = buildingData.CrudeCost.ToString();
		}

		secondCost.text = buildingData.MoneyCost.ToString();

		//firstCost.gameObject.SetActive(buildingData.MoneyCost > 0);
		//secondCost.gameObject.SetActive(buildingData.WoodCost > 0);

		button = GetComponentInChildren<Button>();
		button.onClick.AddListener(OnButtonClicked);

		sprite.sprite = buildingData.BuildingButton;

		var spriteState = new SpriteState();
		spriteState.highlightedSprite = buildingData.HighlightedButton;
		spriteState.pressedSprite = buildingData.HighlightedButton;
		spriteState.disabledSprite = buildingData.UnavailableButton;
		button.spriteState = spriteState;
	}

	public void SetCurrentlySelectedIndicatorActivity(bool shouldBeActive)
	{
		sprite.sprite = shouldBeActive ? buildingData.HighlightedButton : buildingData.BuildingButton;
	}

	public void SetNotEnoughResourcesIndicatorActivity(bool shouldBeActive)
	{
		button.interactable = !shouldBeActive;
	}

	private void OnButtonClicked()
	{
		ButtonClicked?.Invoke(this);
	}
}

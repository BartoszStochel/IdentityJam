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

	private BuildingData buildingData;

	public Action<BuildingData> ButtonClicked;

	public void Initialize(BuildingData data)
	{
		buildingData = data;

		sprite.sprite = buildingData.Sprite;
		buildingName.text = buildingData.NameToDisplay;
		moneyCost.text = buildingData.MoneyCost.ToString();
		woodCost.text = buildingData.WoodCost.ToString();

		moneyCost.gameObject.SetActive(buildingData.MoneyCost > 0);
		woodCost.gameObject.SetActive(buildingData.WoodCost > 0);

		GetComponentInChildren<Button>().onClick.AddListener(OnButtonClicked);
	}

	private void OnButtonClicked()
	{
		ButtonClicked?.Invoke(buildingData);
	}
}

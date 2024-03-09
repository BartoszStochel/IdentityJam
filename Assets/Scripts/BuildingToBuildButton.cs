using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingToBuildButton : MonoBehaviour
{
	[SerializeField] private Image sprite;
	[SerializeField] private TextMeshProUGUI buildingName;
	[SerializeField] private TextMeshProUGUI moneyCost;
	[SerializeField] private TextMeshProUGUI woodCost;

	public void Initialize(BuildingData data)
	{
		sprite.sprite = data.Sprite;
		buildingName.text = data.NameToDisplay;
		moneyCost.text = data.MoneyCost.ToString();
		woodCost.text = data.WoodCost.ToString();

		moneyCost.gameObject.SetActive(data.MoneyCost > 0);
		woodCost.gameObject.SetActive(data.WoodCost > 0);
	}
}

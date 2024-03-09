using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingToBuildButton : MonoBehaviour
{
	[SerializeField] private Image sprite;
	[SerializeField] private TextMeshProUGUI buildingName;

	public void Initialize(BuildingData data)
	{
		sprite.sprite = data.Sprite;
		buildingName.text = data.NameToDisplay;
	}
}

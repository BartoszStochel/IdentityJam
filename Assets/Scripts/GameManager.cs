using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
	[SerializeField] private Field fieldPrefab;
	[SerializeField] private Transform fieldsContainer;
	[SerializeField] private int mapSizeX;
	[SerializeField] private int mapSizeY;
	[SerializeField] private int spacingX;
	[SerializeField] private int spacingY;

	[SerializeField] private Transform buildingsToBuildContainer;
	[SerializeField] private BuildingToBuildButton buildingToBuildPrefab;
	[SerializeField] private List<BuildingData> buildingDatas;

	[SerializeField] private TextMeshProUGUI cashLabel;
	[SerializeField] private TextMeshProUGUI crudeLabel;
	[SerializeField] private TextMeshProUGUI woodLabel;

	[SerializeField] private int startingCash;

	private int currentCash;
	private int currentCrude;
	private int currentWood;

	private Field[,] fields;

	private void Start()
	{
		GenerateFields();
		GenerateBuildingsToBuild();

		currentCash = startingCash;

		UpdateResourcesLabels();
	}

	private void GenerateFields()
	{
		fields = new Field[mapSizeX, mapSizeY];

		for (int x = 0; x < mapSizeX; x++)
		{
			for (int y = 0; y < mapSizeY; y++)
			{
				var field = Instantiate(fieldPrefab, fieldsContainer);
				var rectTransform = field.GetComponent<RectTransform>();
				rectTransform.anchoredPosition = new Vector2(x * spacingX - (mapSizeX - 1) * spacingX / 2f, y * spacingY - (mapSizeY - 1) * spacingY / 2f);
				field.name = $"Field {x}, {y}";
				fields[x, y] = field;
			}
		}
	}

	private void GenerateBuildingsToBuild()
	{
		foreach (var data in buildingDatas)
		{
			var buildingButton = Instantiate(buildingToBuildPrefab, buildingsToBuildContainer);
			buildingButton.Initialize(data);
			buildingButton.name = data.name + "Button";
		}
	}

	private void UpdateResourcesLabels()
	{
		cashLabel.text = currentCash.ToString();
		crudeLabel.text = currentCrude.ToString();
		woodLabel.text = currentWood.ToString();
	}
}

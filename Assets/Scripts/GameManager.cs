using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
	[SerializeField] private List<BuildingData> allBuildingDatas;
	[SerializeField] private List<BuildingData> buildingDatasToBuildByPlayer;

	[SerializeField] private TextMeshProUGUI cashLabel;
	[SerializeField] private TextMeshProUGUI crudeLabel;
	[SerializeField] private TextMeshProUGUI woodLabel;

	[SerializeField] private int startingMoney;

	[SerializeField] private DefaultState defaultState;
	[SerializeField] private BuildingState buildingState;

	[SerializeField] private Button backgroundButton;

	[SerializeField] private int oilSlotsInOneField;
	[SerializeField] private int totalOilCapacityInOneField;
	[SerializeField] private int maxOilInOneSlot;

	private GameState currentState;

	private Field[,] fields;
	private List<BuildingToBuildButton> buildingButtons;

	private ResourcesManager resourcesManager;
	private FieldsRangeIndicatorsManager fieldsRangeIndicatorsManager;

	private void Start()
	{
		GenerateFields();
		GenerateBuildingsToBuildButtons();
		InitializeResourcesManager();
		UpdateResourcesLabels();
		UpdateBuildingButtonsAvailability();
		fieldsRangeIndicatorsManager = new FieldsRangeIndicatorsManager(fields);
		InitializeStates();
		PlaceRandomForestsOnMap();

		backgroundButton.onClick.AddListener(OnBackgroundButtonClicked);
	}

	private void PlaceRandomForestsOnMap()
	{
		foreach (var field in fields)
		{
			if (Random.Range(0, 2) == 0)
			{
				buildingState.PlaceBuildingOnField(allBuildingDatas.Find(data => data.name == "Forest"), field);
			}
		}
	}

	private void InitializeResourcesManager()
	{
		resourcesManager = new ResourcesManager();
		resourcesManager.ModifyMoney(startingMoney);
		resourcesManager.ResourcesChanged += OnResourcesChanged;
	}

	private void OnResourcesChanged()
	{
		UpdateResourcesLabels();
		UpdateBuildingButtonsAvailability();
	}

	private void UpdateBuildingButtonsAvailability()
	{
		foreach (var buildingToBuildButton in buildingButtons)
		{
			var hasEnoughResourcesForBuilding =
				resourcesManager.CurrentMoney >= buildingToBuildButton.buildingData.MoneyCost &&
				resourcesManager.CurrentWood >= buildingToBuildButton.buildingData.WoodCost;

			buildingToBuildButton.SetNotEnoughResourcesIndicatorActivity(!hasEnoughResourcesForBuilding);
		}
	}

	private void InitializeStates()
	{
		currentState = defaultState;

		defaultState.RequestExitFromThisState += OnRequestExitFromThisState;
		defaultState.Initialize(fieldsRangeIndicatorsManager);

		buildingState.RequestExitFromThisState += OnRequestExitFromThisState;
		buildingState.Initialize(fields, resourcesManager, fieldsRangeIndicatorsManager);
	}

	private void OnRequestExitFromThisState(GameState state)
	{
		ChangeState(defaultState);
	}

	private void OnBackgroundButtonClicked()
	{
		ChangeState(defaultState);
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
				field.Initialize(x, y, GetOilForField());
				field.ButtonClicked += OnFieldClicked;
				field.HoverStart += OnFieldHoverStart;
				field.HoverEnd += OnFieldHoverEnd;

				fields[x, y] = field;
			}
		}
	}

	private List<int> GetOilForField()
	{
		var oil = new List<int>();
		var indicesToConsider = new List<int>();

		for (int i = 0; i < oilSlotsInOneField; i++)
		{
			indicesToConsider.Add(i);
			oil.Add(0);
		}

		for (int i = 0; i < totalOilCapacityInOneField; i++)
		{
			int indexToConsider;
			bool indexIsSettled;

			do
			{
				indexToConsider = indicesToConsider[Random.Range(0, indicesToConsider.Count)];

				if (oil[indexToConsider] >= maxOilInOneSlot)
				{
					indicesToConsider.Remove(indexToConsider);
					indexIsSettled = false;
				}
				else
				{
					indexIsSettled = true;
				}
			}
			while (!indexIsSettled);

			oil[indexToConsider]++;
		}

		return oil;
	}

	private void OnFieldClicked(Field field)
	{
		currentState.OnFieldClicked(field);
	}

	private void OnFieldHoverStart(Field field)
	{
		currentState.OnFieldHoverStart(field);
	}

	private void OnFieldHoverEnd(Field field)
	{
		currentState.OnFieldHoverEnd(field);
	}

	private void GenerateBuildingsToBuildButtons()
	{
		buildingButtons = new List<BuildingToBuildButton>();

		foreach (var data in buildingDatasToBuildByPlayer)
		{
			var buildingButton = Instantiate(buildingToBuildPrefab, buildingsToBuildContainer);
			buildingButton.Initialize(data);
			buildingButton.name = data.name + "Button";
			buildingButton.ButtonClicked += OnBuildingButtonClicked;

			buildingButtons.Add(buildingButton);
		}
	}

	private void OnBuildingButtonClicked(BuildingToBuildButton button)
	{
		buildingState.SetCurrentlySelectedBuildingButton(button);
		ChangeState(buildingState);
	}

	private void ChangeState(GameState newState)
	{
		currentState.OnStateExited();
		currentState = newState;
		currentState.OnStateEntered();
	}

	private void UpdateResourcesLabels()
	{
		cashLabel.text = resourcesManager.CurrentMoney.ToString();
		crudeLabel.text = resourcesManager.CurrentCrude.ToString();
		woodLabel.text = resourcesManager.CurrentWood.ToString();
	}

	private void OnGUI()
	{
		GUILayout.Label($"Current state: {currentState.name}");
	}
}

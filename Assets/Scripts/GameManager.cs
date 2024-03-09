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
	[SerializeField] private List<BuildingData> buildingDatas;

	[SerializeField] private TextMeshProUGUI cashLabel;
	[SerializeField] private TextMeshProUGUI crudeLabel;
	[SerializeField] private TextMeshProUGUI woodLabel;

	[SerializeField] private int startingMoney;

	[SerializeField] private DefaultState defaultState;
	[SerializeField] private BuildingState buildingState;

	[SerializeField] private Button backgroundButton;

	private GameState currentState;

	private Field[,] fields;
	private List<BuildingToBuildButton> buildingButtons;

	private ResourcesManager resourcesManager;

	private void Start()
	{
		GenerateFields();
		GenerateBuildingsToBuildButtons();
		InitializeResourcesManager();
		UpdateResourcesLabels();
		UpdateBuildingButtonsAvailability();
		InitializeStates();

		backgroundButton.onClick.AddListener(OnBackgroundButtonClicked);
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
		buildingState.RequestExitFromThisState += OnRequestExitFromThisState;
		buildingState.Initialize(fields, resourcesManager);
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
				field.name = $"Field {x}, {y}";
				field.ButtonClicked += OnFieldClicked;

				fields[x, y] = field;
			}
		}
	}

	private void OnFieldClicked(Field field)
	{
		currentState.OnFieldClicked(field);
	}

	private void GenerateBuildingsToBuildButtons()
	{
		buildingButtons = new List<BuildingToBuildButton>();

		foreach (var data in buildingDatas)
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

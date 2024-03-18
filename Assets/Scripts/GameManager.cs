using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
	[SerializeField] private Transform fieldsContainer;

	[SerializeField] private Transform buildingsToBuildContainer;
	[SerializeField] private BuildingToBuildButton buildingToBuildPrefab;
	[SerializeField] private BuildingData forestData;

	[SerializeField] private TextMeshProUGUI cashLabel;
	[SerializeField] private TextMeshProUGUI crudeLabel;
	[SerializeField] private TextMeshProUGUI woodLabel;

	[SerializeField] private DefaultState defaultState;
	[SerializeField] private BuildingState buildingState;

	[SerializeField] private Button backgroundButton;

	[SerializeField] private int oilSlotsInOneField;
	[SerializeField] private int minOilInOneField;
	[SerializeField] private int maxOilInOneField;
	[SerializeField] private int maxOilInOneSlot;

	[SerializeField] private int numberOfForestsOnMap;

	[SerializeField] private int startingYear;
	[SerializeField] private int yearLengthInSeconds;
	[SerializeField] private TextMeshProUGUI yearTimerLabel;
	[SerializeField] private int moneyValueOfOneCrude;
	[SerializeField] private List<GameObject> calendarCheckmarks;
	[SerializeField] private BuildingData geologistData;
	[SerializeField] private BuildingData lumberjackData;
	[SerializeField] private BuildingData oilRigData;

	[SerializeField] private GameSettings gameSettings;

	private int currentYear;
	private float timerToNextYear;

	private GameState currentState;

	// So, the first index is Y, second is X.
	private List<List<Field>> fieldsRows;
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
		fieldsRangeIndicatorsManager = new FieldsRangeIndicatorsManager(fieldsRows);
		InitializeStates();
		PlaceRandomForestsOnMap();

		currentYear = startingYear;
		timerToNextYear = yearLengthInSeconds;

		backgroundButton.onClick.AddListener(OnBackgroundButtonClicked);
	}

	private void Update()
	{
		HandleYearsAndCrudeSelling();
	}

	private void HandleYearsAndCrudeSelling()
	{
		timerToNextYear -= Time.deltaTime;

		if (timerToNextYear <= 0f)
		{
			currentYear++;
			timerToNextYear = yearLengthInSeconds;

			var crudeToSell = resourcesManager.CurrentCrude - geologistData.CrudeCost;

			if (crudeToSell > 0)
			{
				resourcesManager.ModifyMoney(crudeToSell * moneyValueOfOneCrude);
				resourcesManager.ModifyCrude(-crudeToSell);
			}
		}

		float timeForOneCheckmark = (float)yearLengthInSeconds / calendarCheckmarks.Count;

		for (int i = 0; i < calendarCheckmarks.Count; i++)
		{
			calendarCheckmarks[i].SetActive(timerToNextYear < timeForOneCheckmark * (calendarCheckmarks.Count -i));
		}

		yearTimerLabel.text = currentYear.ToString();
	}

	private void PlaceRandomForestsOnMap()
	{
		var yIndicesToConsider = new List<int>();

		for (int i = 0; i < gameSettings.NumberOfRowsInMap; i++)
		{
			yIndicesToConsider.Add(i);
		}

		for (int i = 0; i < numberOfForestsOnMap; i++)
		{
			bool indexIsSettled;
			int yIndexToConsider;
			List<Field> freeFieldsInARow;

			do
			{
				yIndexToConsider = yIndicesToConsider[Random.Range(0, yIndicesToConsider.Count)];
				freeFieldsInARow = GetFreeFieldsInOneRow(yIndexToConsider);

				if (freeFieldsInARow.Count <= 1)
				{
					yIndicesToConsider.Remove(yIndexToConsider);
					indexIsSettled = false;
				}
				else
				{
					indexIsSettled = true;
				}
			}
			while (!indexIsSettled);

			var fieldToPlaceForest = freeFieldsInARow[Random.Range(0, freeFieldsInARow.Count)];
			buildingState.PlaceBuildingOnField(forestData, fieldToPlaceForest);
		}
	}

	private List<Field> GetFreeFieldsInOneRow(int rowYIndex)
	{
		var freeFields = new List<Field>();

		for (int i = 0; i < fieldsRows[rowYIndex].Count; i++)
		{
			var fieldToConsider = fieldsRows[rowYIndex][i];
			if (fieldToConsider.BuildingOnField == null)
			{
				freeFields.Add(fieldToConsider);
			}
		}

		return freeFields;
	}

	private void InitializeResourcesManager()
	{
		resourcesManager = new ResourcesManager();
		resourcesManager.ResourcesChanged += OnResourcesChanged;

		resourcesManager.ModifyMoney(geologistData.MoneyCost * 2 + lumberjackData.MoneyCost * 2 + oilRigData.MoneyCost * 2);
		resourcesManager.ModifyCrude(geologistData.CrudeCost * 2);
		resourcesManager.ModifyWood(lumberjackData.WoodCost * 2);
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
			var hasEnoughResourcesForBuilding = resourcesManager.HaveEnoughResourcesForBuilding(buildingToBuildButton.buildingData);

			buildingToBuildButton.SetNotEnoughResourcesIndicatorActivity(!hasEnoughResourcesForBuilding);
		}
	}

	private void InitializeStates()
	{
		currentState = defaultState;

		defaultState.RequestExitFromThisState += OnRequestExitFromThisState;
		defaultState.Initialize(fieldsRangeIndicatorsManager);

		buildingState.RequestExitFromThisState += OnRequestExitFromThisState;
		buildingState.Initialize(fieldsRows, resourcesManager, fieldsRangeIndicatorsManager);
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
		fieldsRows = new List<List<Field>>(gameSettings.NumberOfRowsInMap);
		var availableFieldsLayers = new List<FieldsLayer>(gameSettings.FieldsLayers);

		for (int y = 0; y < gameSettings.NumberOfRowsInMap; y++)
		{
			var chosenPrefab = availableFieldsLayers[Random.Range(0, availableFieldsLayers.Count)];

			if (availableFieldsLayers.Count > 1)
			{
				availableFieldsLayers.Remove(chosenPrefab);
			}

			var instantiatedLayer = Instantiate(chosenPrefab, fieldsContainer);
			instantiatedLayer.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, gameSettings.BottomRowYPosition + gameSettings.SpacingBetweenRows * y);
			var row = new List<Field>(instantiatedLayer.Fields);

			fieldsRows.Add(row);
			instantiatedLayer.GetComponent<Canvas>().sortingOrder = gameSettings.NumberOfRowsInMap * 10 - y * 5;

			for (int x = 0; x < row.Count; x++)
			{
				var field = row[x];

				field.Initialize(x, y, GetOilForField());
				field.ButtonClicked += OnFieldClicked;
				field.HoverStart += OnFieldHoverStart;
				field.HoverEnd += OnFieldHoverEnd;
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

		var oilInOneField = Random.Range(minOilInOneField, maxOilInOneField);

		for (int i = 0; i < oilInOneField; i++)
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

		foreach (var data in gameSettings.BuildingDatasToBuildByPlayer)
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
		//GUILayout.Label($"Current state: {currentState.name}");
	}
}

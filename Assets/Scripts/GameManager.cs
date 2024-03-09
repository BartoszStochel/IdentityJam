using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] private Field fieldPrefab;
	[SerializeField] private GameObject fieldsContainer;
	[SerializeField] private int mapSizeX;
	[SerializeField] private int mapSizeY;
	[SerializeField] private int spacingX;
	[SerializeField] private int spacingY;

	private Field[,] fields;

	private void Start()
	{
		fields = new Field[mapSizeX, mapSizeY];

		for (int x = 0; x < mapSizeX; x++)
		{
			for (int y = 0; y < mapSizeY; y++)
			{
				var field = Instantiate(fieldPrefab, fieldsContainer.transform);
				var rectTransform = field.GetComponent<RectTransform>();
				rectTransform.anchoredPosition = new Vector2(x * spacingX - (mapSizeX - 1) * spacingX / 2f, y * spacingY - (mapSizeY - 1) * spacingY / 2f);
				field.name = $"Field {x}, {y}";
				fields[x, y] = field;
			}
		}
	}
}

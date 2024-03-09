using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
	[SerializeField] private Image Image;
	private BuildingData data;

	public void Initialize(BuildingData newData)
	{
		data = newData;
		Image.sprite = data.Sprite;
	}
}

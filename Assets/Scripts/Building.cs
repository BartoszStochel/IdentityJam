using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
	[SerializeField] private Image Image;

	private BuildingData data;
	private BuildingBehaviour behaviour;

	private void Update()
	{
		if (behaviour != null)
		{
			behaviour.OnUpdateBehaviour();
		}
	}

	public void Initialize(BuildingData newData, BuildingBehaviour newBehaviour)
	{
		data = newData;
		behaviour = newBehaviour;

		Image.sprite = data.Sprite;
	}
}

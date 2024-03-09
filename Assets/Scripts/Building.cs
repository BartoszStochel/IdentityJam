using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
	[SerializeField] private Image Image;

	public BuildingBehaviour Behaviour { get; private set; }

	private BuildingData data;

	private void Update()
	{
		if (Behaviour != null)
		{
			Behaviour.OnUpdateBehaviour();
		}
	}

	public void Initialize(BuildingData newData, BuildingBehaviour newBehaviour)
	{
		data = newData;
		Behaviour = newBehaviour;

		Image.sprite = data.Sprite;
	}
}

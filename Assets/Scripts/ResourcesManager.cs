using System;

public class ResourcesManager
{
	public int CurrentMoney { get; private set; }
	public int CurrentCrude { get; private set; }
	public int CurrentWood { get; private set; }

	public event Action ResourcesChanged;

	public void ModifyMoney(int moneyToAdd)
	{
		CurrentMoney += moneyToAdd;
		ResourcesChanged?.Invoke();
	}

	public void ModifyCrude(int crudeToAdd)
	{
		CurrentCrude += crudeToAdd;
		ResourcesChanged?.Invoke();
	}

	public void ModifyWood(int woodToAdd)
	{
		CurrentWood += woodToAdd;
		ResourcesChanged?.Invoke();
	}
}

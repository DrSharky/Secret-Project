using UnityEngine;

public interface IEquippable
{
	void OnEquip();
	void OnUnequip();
	void UpdateEquipped();
}
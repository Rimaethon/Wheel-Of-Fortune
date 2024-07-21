using Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data.Scriptable_Objects.Abstract
{
	public abstract class AbstractEquipmentDataSO : AbstractItemDataSO
	{
		[VerticalGroup("Item Data/Stats"), LabelWidth(100)]
		public readonly ItemTiers EquipmentTier;
		[VerticalGroup("Item Data/Stats"), LabelWidth(100)]
		public readonly Material Material;
		[VerticalGroup("Item Data/Stats"), LabelWidth(100)]
		public readonly ItemRarity rarity;
	}
}

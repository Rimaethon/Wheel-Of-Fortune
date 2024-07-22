using Data.Scriptable_Objects.Abstract;
using Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data.Scriptable_Objects.Items
{
	[CreateAssetMenu(fileName = "New Upgrade Point Data", menuName = "Scriptable Objects/Upgrade Point Data", order = 1)]
	public class UpgradePointDataSO : AbstractItemDataSO
	{
		[VerticalGroup("Item Data/Stats"), LabelWidth(100)]
		public readonly UpgradeableEquipmentPointTypes upgradeableEquipmentPointType = UpgradeableEquipmentPointTypes.NONE;
	}
}

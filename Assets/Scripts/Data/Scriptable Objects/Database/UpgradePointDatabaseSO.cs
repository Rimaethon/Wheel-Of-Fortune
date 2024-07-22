using System.Collections.Generic;
using Data.Scriptable_Objects.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data.Scriptable_Objects.Database
{
	[CreateAssetMenu(fileName = "New Upgrade Point Database", menuName = "Scriptable Objects/Upgrade Point Database", order = 1)]
	public class UpgradePointDatabaseSO : ItemDatabaseSO
	{
		[OnValueChanged("InitializeDatabase")]
		public List<UpgradePointDataSO> UpgradePointDataList = new List<UpgradePointDataSO>();

		public void InitializeDatabase()
		{
			ItemDatabase.Clear();

			foreach (UpgradePointDataSO data in UpgradePointDataList)
			{
				if (data != null)
				{
					ItemDatabase.Add((int) data.upgradeableEquipmentPointType, data);
					data.ItemID = (int) data.upgradeableEquipmentPointType;
				}
			}
		}
	}
}

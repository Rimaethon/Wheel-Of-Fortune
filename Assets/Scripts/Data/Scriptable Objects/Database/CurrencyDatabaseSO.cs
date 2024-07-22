using System.Collections.Generic;
using Data.Scriptable_Objects.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data.Scriptable_Objects.Database
{
	[CreateAssetMenu(fileName = "New Currency Database", menuName = "Scriptable Objects/Currency Database", order = 1)]
	public class CurrencyDatabaseSO : ItemDatabaseSO
	{
		[OnValueChanged("InitializeDatabase")]
		public List<CurrencyDataSO> CurrencyList = new List<CurrencyDataSO>();

		public void InitializeDatabase()
		{
			ItemDatabase.Clear();

			foreach (CurrencyDataSO data in CurrencyList)
			{
				if (data != null)
				{
					ItemDatabase.Add((int) data.CurrencyType, data);
				}
			}
		}
	}
}

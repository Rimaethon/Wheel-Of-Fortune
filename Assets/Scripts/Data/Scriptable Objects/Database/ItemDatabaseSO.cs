using System.Collections.Generic;
using Data.Scriptable_Objects.Abstract;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data.Scriptable_Objects.Database
{
	[CreateAssetMenu(fileName = "New Item Database", menuName = "Scriptable Objects/Item Database", order = 1)]
	public class ItemDatabaseSO : SerializedScriptableObject
	{
		[OnValueChanged("SetItemID")]
		public readonly Dictionary<int, AbstractItemDataSO> ItemDatabase = new Dictionary<int, AbstractItemDataSO>();

		private void SetItemID()
		{
			foreach (KeyValuePair<int, AbstractItemDataSO> item in ItemDatabase)
			{
				item.Value.ItemID = item.Key;
			}
		}
	}
}

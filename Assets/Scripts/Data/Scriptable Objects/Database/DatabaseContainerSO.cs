using System.Collections.Generic;
using Data.Scriptable_Objects.Abstract;
using Enums;
using UnityEngine;

namespace Data.Scriptable_Objects.Database
{
	[CreateAssetMenu(fileName = "New Database Container", menuName = "Scriptable Objects/Database Container", order = 1)]
	public class DatabaseContainerSO : SingletonSerializedScriptableObject<DatabaseContainerSO>
	{
		public readonly Dictionary<ItemTypes, ItemDatabaseSO> DatabaseDictionary = new Dictionary<ItemTypes, ItemDatabaseSO>();
	}
}

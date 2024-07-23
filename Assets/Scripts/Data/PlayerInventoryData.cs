using System;
using System.Collections.Generic;
using Enums;

namespace Data
{
	[Serializable]
	public class PlayerInventoryData
	{
		public Dictionary<ItemTypes, Dictionary<int, ItemData>> InventoryData = new Dictionary<ItemTypes, Dictionary<int, ItemData>>();
	}
}

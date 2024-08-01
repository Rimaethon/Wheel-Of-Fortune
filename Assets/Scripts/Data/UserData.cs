using System;
using System.Collections.Generic;
using Enums;

namespace Data
{
	[Serializable]
	public class UserData
	{
		public string Name;
		public PlayerInventoryData playerInventoryData;
		public UserData()
		{
			playerInventoryData = new PlayerInventoryData();
			playerInventoryData.InventoryData.Add(ItemTypes.CURRENCY, new Dictionary<int, ItemData>());

			playerInventoryData.InventoryData[ItemTypes.CURRENCY]
							   .Add((int) CurrencyType.Gold, new ItemData
							   {
								   Amount = 6000
							   });

			playerInventoryData.InventoryData[ItemTypes.CURRENCY]
							   .Add((int) CurrencyType.Cash, new ItemData
							   {
								   Amount = 12000
							   });
		}
	}
}

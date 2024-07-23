using System;
using System.Collections.Generic;
using Enums;

namespace Data
{
	[Serializable]
	public class UserData
	{
		public int GoldAmount;
		public int CashAmount;
		public string Name;
		public PlayerInventoryData playerInventoryData;

		public UserData()
		{
			playerInventoryData = new PlayerInventoryData();
			playerInventoryData.InventoryData.Add(ItemTypes.Currency, new Dictionary<int, ItemData>());

			playerInventoryData.InventoryData[ItemTypes.Currency]
							   .Add((int) CurrencyType.Gold, new ItemData
							   {
								   Amount = 6000
							   });

			playerInventoryData.InventoryData[ItemTypes.Currency]
							   .Add((int) CurrencyType.Cash, new ItemData
							   {
								   Amount = 12000
							   });

			CashAmount = 12000;
			GoldAmount = 4000;
		}
	}
}

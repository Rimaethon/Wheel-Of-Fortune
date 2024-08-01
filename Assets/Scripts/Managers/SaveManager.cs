using System.IO;
using Data;
using Enums;
using Sirenix.Serialization;
using UnityEngine;
using Utility;

//Get and Set Methods in this class are just for testing purposes. In a real game, these methods should be server authoritative .
namespace Managers
{
	public class SaveManager : PersistentSingleton<SaveManager>
	{
		private const string extension = ".json";
		private const string user_data_path = "/UserData";
		private UserData userData;

		private void OnEnable()
		{
			InitializeData();
		}

		private void InitializeData()
		{
			if (File.Exists(Application.persistentDataPath + user_data_path))
			{
				userData = LoadFromJson<UserData>(user_data_path);
			}
			else
			{
				userData = new UserData();
				SaveToJson(userData, user_data_path);
			}
		}

		public int GetGoldAmount()
		{
			return userData.playerInventoryData.InventoryData[ItemTypes.CURRENCY][(int) CurrencyType.Gold].Amount;
		}

		public int GetCashAmount()
		{
			return userData.playerInventoryData.InventoryData[ItemTypes.CURRENCY][(int) CurrencyType.Cash].Amount;
		}

		public void ChangeGoldAmount(int amount)
		{
			userData.playerInventoryData.InventoryData[ItemTypes.CURRENCY][(int) CurrencyType.Gold].Amount += amount;
			SaveToJson(userData, user_data_path);
			EventManager.SInstance.Broadcast(GameEvents.OnGoldAmountChanged);
		}

		public void ChangeCashAmount(int amount)
		{
			userData.playerInventoryData.InventoryData[ItemTypes.CURRENCY][(int) CurrencyType.Cash].Amount += amount;
			SaveToJson(userData, user_data_path);
			EventManager.SInstance.Broadcast(GameEvents.ON_CASH_AMOUNT_CHANGED);
		}

		private int GetNumberOfFilesInFolder(string folderPath)
		{
			return Directory.GetFiles(Application.persistentDataPath + folderPath).Length;
		}

		public void SaveToJson<T>(T data, string path)
		{
			byte[] serializedData = SerializationUtility.SerializeValue(data, DataFormat.JSON);
			File.WriteAllBytes(Application.persistentDataPath + path + extension, serializedData);
		}

		public T LoadFromJson<T>(string path)
		{
			byte[] bytes = File.ReadAllBytes(Application.persistentDataPath + path);
			return SerializationUtility.DeserializeValue<T>(bytes, DataFormat.JSON);
		}
	}
}

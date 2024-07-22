using System.Collections.Generic;
using Data.Scriptable_Objects.Abstract;
using UnityEngine;

namespace Data.Scriptable_Objects.Items
{
	[CreateAssetMenu(fileName = "New Chest Data", menuName = "Scriptable Objects/Chest Data", order = 1)]
	public class ChestDataSO : AbstractItemDataSO
	{
		[Tooltip("How many different type of items can be obtained from this chest at once")]
		public int MaxDropAmount;
		public List<ChestRewardData> ChestRewards = new List<ChestRewardData>();
	}
}

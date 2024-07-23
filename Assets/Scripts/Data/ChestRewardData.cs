using System;
using Data.Scriptable_Objects.Abstract;

namespace Data
{
	[Serializable]
	public class ChestRewardData
	{
		public AbstractItemDataSO ItemData;
		public int MinAmount;
		public int MaxAmount;
		public int ChanceToDrop;
	}
}

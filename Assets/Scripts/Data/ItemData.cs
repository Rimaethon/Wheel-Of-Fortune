using System;
using System.Collections.Generic;

namespace Data
{
	[Serializable]
	public class ItemData
	{
		public int Amount;
		public int Level;
		public int currentSkinID = -1;
		public Dictionary<int, SkinData> SkinData;
	}
}

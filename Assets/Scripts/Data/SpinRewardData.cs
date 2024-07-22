using System;
using Data.Scriptable_Objects.Abstract;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data
{
	[Serializable]
	public class SpinRewardData
	{
		[HideLabel, ShowInInspector, PreviewField(Height = 100), HorizontalGroup("Reward Data", 100)]
		public Sprite sprite;

		[VerticalGroup("Reward Data/Fields"), LabelWidth(100), ShowInInspector, ReadOnly]
		public string ItemName;

		[Range(0, 1), VerticalGroup("Reward Data/Fields"), LabelWidth(100)]
		public float BaseChance;

		[VerticalGroup("Reward Data/Fields"), LabelWidth(100)]
		public int BaseAmount;

		[OnValueChanged("SetItemData"), VerticalGroup("Reward Data/Fields"), LabelWidth(100)]
		public AbstractItemDataSO ItemData;

		private void SetItemData()
		{
			if (ItemData == null)
			{
				sprite = null;
				ItemName = "";
				ItemData = null;
				return;
			}

			ItemName = ItemData.Name;
			sprite = ItemData.Icon;
		}
	}
}

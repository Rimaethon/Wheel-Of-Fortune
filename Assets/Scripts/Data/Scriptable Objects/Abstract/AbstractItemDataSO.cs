using System.Collections.Generic;
using Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data.Scriptable_Objects.Abstract
{
	public abstract class AbstractItemDataSO : SerializedScriptableObject
	{
		[VerticalGroup("Item Data/Stats"), LabelWidth(100)]
		public int ItemID;

		[VerticalGroup("Item Data/Stats"), LabelWidth(100)]
		public readonly ItemAcquireTypes AcquireType;

		[HideLabel, PreviewField(Height = 150,Alignment = ObjectFieldAlignment.Left), HorizontalGroup("Item Data", 150)]
		public readonly Sprite Icon;

		[VerticalGroup("Item Data/Stats"), LabelWidth(100)]
		public readonly ItemTypes ItemType;

		[VerticalGroup("Item Data/Stats"), LabelWidth(100)]
		public readonly string Name;

		[VerticalGroup("Item Data/Stats"), LabelWidth(100)]
		public readonly int Price;
		[ShowInInspector]
		protected Dictionary<AttributeTypes, int> Attributes;
	}
}

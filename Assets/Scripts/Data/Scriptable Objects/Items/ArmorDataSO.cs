using System.Collections.Generic;
using Data.Scriptable_Objects.Abstract;
using Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data.Scriptable_Objects.Items
{
	[CreateAssetMenu(fileName = "New Helmet Data", menuName = "Scriptable Objects/HelmetData", order = 1)]
	public class ArmorDataSO : AbstractItemDataSO
	{
		[VerticalGroup("Item Data/Stats"), LabelWidth(100)]
		public ArmorTypes ArmorType;

		private ArmorDataSO()
		{
			Attributes = new Dictionary<AttributeTypes, int>
			{
				{
					AttributeTypes.BaseHealth, 0
				},
				{
					AttributeTypes.MaxHealth, 0
				},

				{
					AttributeTypes.BaseArmor, 0
				},
				{
					AttributeTypes.MaxArmor, 0
				},

				{
					AttributeTypes.BaseMovementSpeed, 0
				},
				{
					AttributeTypes.MaxMovementSpeed, 0
				},

				{
					AttributeTypes.BaseHeadProtection, 0
				},
				{
					AttributeTypes.MaxHeadProtection, 0
				}
			};
		}
	}
}

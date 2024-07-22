using System.Collections.Generic;
using Data.Scriptable_Objects.Abstract;
using Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data.Scriptable_Objects.Items
{
	[CreateAssetMenu(fileName = "New Skin Data", menuName = "Scriptable Objects/Skin Data", order = 1)]
	public class SkinDataSO : AbstractEquipmentDataSO
	{
		[VerticalGroup("Item Data/Stats"), LabelWidth(100)]
		public int weaponID;
		public bool HasFragments;
		public int FragmentNeeded;

		private SkinDataSO()
		{
			Attributes = new Dictionary<AttributeTypes, int>
			{
				{
					AttributeTypes.DamageIncreasePercentage, 0
				},

				{
					AttributeTypes.FireRateIncreasePercentage, 0
				},

				{
					AttributeTypes.MovementSpeedIncreasePercentage, 0
				},

				{
					AttributeTypes.RangeIncreasePercentage, 0
				},

				{
					AttributeTypes.AccuracyIncreasePercentage, 0
				},

				{
					AttributeTypes.ClipSizeIncreasePercentage, 0
				}
			};
		}
	}
}

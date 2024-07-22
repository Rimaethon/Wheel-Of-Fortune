using System.Collections.Generic;
using Data.Scriptable_Objects.Abstract;
using Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data.Scriptable_Objects.Items
{
	[CreateAssetMenu(fileName = "New Weapon Data", menuName = "Scriptable Objects/WeaponData", order = 1)]
	public class WeaponDataSO : AbstractEquipmentDataSO
	{
		[VerticalGroup("Item Data/Stats", 10), LabelWidth(100)]
		public WeaponTypes weaponType;

		[OnValueChanged("SetOwnerID")]
		public Dictionary<int, SkinDataSO> WeaponSkins;

		public WeaponDataSO()
		{
			Attributes = new Dictionary<AttributeTypes, int>
			{
				{
					AttributeTypes.BaseDamage, 0
				},
				{
					AttributeTypes.MaxDamage, 0
				},

				{
					AttributeTypes.BaseFireRate, 0
				},
				{
					AttributeTypes.MaxFireRate, 0
				},

				{
					AttributeTypes.BaseMovementSpeed, 0
				},
				{
					AttributeTypes.MaxMovementSpeed, 0
				},

				{
					AttributeTypes.BaseRange, 0
				},
				{
					AttributeTypes.MaxRange, 0
				},

				{
					AttributeTypes.BaseAccuracy, 0
				},
				{
					AttributeTypes.MaxAccuracy, 0
				},

				{
					AttributeTypes.BaseClipSize, 0
				},
				{
					AttributeTypes.MaxClipSize, 0
				}
			};
		}

		private void SetOwnerID()
		{
			foreach (KeyValuePair<int, SkinDataSO> skin in WeaponSkins)
			{
				skin.Value.weaponID = ItemID;
			}
		}
	}
}

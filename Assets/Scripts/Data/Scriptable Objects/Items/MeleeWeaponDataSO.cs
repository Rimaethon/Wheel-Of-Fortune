using System.Collections.Generic;
using Data.Scriptable_Objects.Abstract;
using Enums;
using UnityEngine;

namespace Data.Scriptable_Objects.Items
{
	[CreateAssetMenu(fileName = "New Melee Weapon Data", menuName = "Scriptable Objects/MeleeWeaponData", order = 1)]
	public class MeleeWeaponDataSO : AbstractEquipmentDataSO
	{
		private MeleeWeaponDataSO()
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
				}
			};
		}
	}
}

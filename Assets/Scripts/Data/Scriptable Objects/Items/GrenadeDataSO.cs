using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Data.Scriptable_Objects.Items
{
	[CreateAssetMenu(fileName = "New Grenade Data", menuName = "Scriptable Objects/GrenadeData", order = 1)]
	public class GrenadeDataSO : WeaponDataSO
	{
		public readonly string Description;

		private GrenadeDataSO()
		{
			Attributes = new Dictionary<AttributeTypes, int>
			{
				{
					AttributeTypes.BaseExplosionDamage, 0
				},
				{
					AttributeTypes.MaxExplosionDamage, 0
				},

				{
					AttributeTypes.BaseExplosionRadius, 0
				},
				{
					AttributeTypes.MaxExplosionRadius, 0
				},

				{
					AttributeTypes.BaseCooldown, 0
				},
				{
					AttributeTypes.MaxCooldown, 0
				}
			};
		}
	}
}

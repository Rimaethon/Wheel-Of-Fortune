using System.Collections.Generic;
using Data.Scriptable_Objects.Abstract;
using Enums;
using UnityEngine;

namespace Data.Scriptable_Objects.Items
{
	[CreateAssetMenu(fileName = "New Med Kit Data", menuName = "Scriptable Objects/MedKitData", order = 1)]
	public class MedkitDataSO : AbstractItemDataSO
	{
		public readonly string Description;

		private MedkitDataSO()
		{
			Attributes = new Dictionary<AttributeTypes, int>
			{
				{
					AttributeTypes.HealthRegenerationRate, 0
				},
				{
					AttributeTypes.ApplyTime, 0
				},
				{
					AttributeTypes.BaseCooldown, 0
				},
				{
					AttributeTypes.DamageIncreasePercentage, 0
				},
				{
					AttributeTypes.DamageReductionPercentage, 0
				},
				{
					AttributeTypes.MovementSpeedIncreasePercentage, 0
				},
				{
					AttributeTypes.EffectDuration, 0
				},
				{
					AttributeTypes.EffectRange, 0
				}
			};
		}
	}
}

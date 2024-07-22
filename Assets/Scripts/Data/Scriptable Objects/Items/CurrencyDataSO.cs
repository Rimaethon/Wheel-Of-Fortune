using Data.Scriptable_Objects.Abstract;
using Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data.Scriptable_Objects.Items
{
	[CreateAssetMenu(fileName = "New Currency", menuName = "Scriptable Objects/Currency", order = 1)]
	public class CurrencyDataSO : AbstractItemDataSO
	{
		[VerticalGroup("Item Data/Stats"), LabelWidth(100)]
		public readonly CurrencyType CurrencyType;
	}
}

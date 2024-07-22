using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data.Scriptable_Objects
{
	[CreateAssetMenu(fileName = "Spin Reward List", menuName = "Scriptable Objects/Spin Reward List", order = 1)]
	public class SpinRewardListSO : SerializedScriptableObject
	{
		public List<SpinRewardData> Database = new List<SpinRewardData>();
	}
}

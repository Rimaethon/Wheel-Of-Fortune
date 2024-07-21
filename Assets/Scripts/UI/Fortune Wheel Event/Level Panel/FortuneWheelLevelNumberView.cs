using TMPro;
using UnityEngine;

namespace UI.Fortune_Wheel_Event.Level_Panel
{
	public class FortuneWheelLevelNumberView : MonoBehaviour
	{
		[HideInInspector]
		public TextMeshProUGUI numberText;

		[HideInInspector]
		public RectTransform rectTransform;

		public int index;

		private void Awake()
		{
			numberText = GetComponent<TextMeshProUGUI>();
			rectTransform = GetComponent<RectTransform>();
		}
	}
}

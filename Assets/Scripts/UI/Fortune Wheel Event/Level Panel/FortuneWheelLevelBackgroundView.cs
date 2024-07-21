using UnityEngine;
using UnityEngine.UI;

namespace UI.Fortune_Wheel_Event.Level_Panel
{
	public class FortuneWheelLevelBackgroundView : MonoBehaviour
	{
		[HideInInspector]
		public Image numberBackground;
		[HideInInspector]
		public RectTransform rectTransform;

		private void Awake()
		{
			numberBackground = GetComponent<Image>();
			rectTransform = GetComponent<RectTransform>();
		}
	}
}

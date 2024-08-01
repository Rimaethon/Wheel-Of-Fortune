using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Fortune_Wheel_Event.Gained_Item_Panel
{
	public class FortuneWheelGainedItemView : MonoBehaviour
	{
		public int itemID;
		public int amount;
		public TextMeshProUGUI amountText;
		public Image itemIcon;
		public ItemTypes itemType;
		public RectTransform rectTransform;

		private void Awake()
		{
			rectTransform = GetComponent<RectTransform>();
			amountText = GetComponentInChildren<TextMeshProUGUI>();
			itemIcon = GetComponentInChildren<Image>();
		}
	}
}

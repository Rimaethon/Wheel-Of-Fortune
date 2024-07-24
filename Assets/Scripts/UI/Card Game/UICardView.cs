using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Card_Game_Event
{
	public class UICardView : MonoBehaviour
	{
		private Image CardImage;
		private TextMeshProUGUI CardText;

		private void Awake()
		{
			CardText = GetComponentInChildren<TextMeshProUGUI>();
			CardImage = GetComponentInChildren<Image>();
		}
	}
}

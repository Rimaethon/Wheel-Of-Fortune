using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Card_Game_Event
{
	public class UICardView : MonoBehaviour
	{
		private Image cardImage;
		private TextMeshProUGUI cardText;

		private void Awake()
		{
			cardText = GetComponentInChildren<TextMeshProUGUI>();
			cardImage = GetComponentInChildren<Image>();
		}
	}
}

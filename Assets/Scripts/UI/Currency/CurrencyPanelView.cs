using Enums;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Currency
{
	public class CurrencyPanelView : MonoBehaviour
	{
		[SerializeField]
		private GameObject goldPanel;
		[SerializeField]
		private GameObject cashPanel;
		private TextMeshProUGUI cashAmountText;
		private TextMeshProUGUI goldAmountText;
		private RectTransform rectTransform;

		protected void Awake()
		{
			rectTransform = GetComponent<RectTransform>();
			goldAmountText = goldPanel.GetComponentInChildren<TextMeshProUGUI>();
			cashAmountText = cashPanel.GetComponentInChildren<TextMeshProUGUI>();

		}

		private void Start()
		{
			HandleGoldChange();
			HandleCashChange();
		}

		private void OnEnable()
		{
			EventManager.Instance.AddHandler(GameEvents.OnGoldAmountChanged, HandleGoldChange);
			EventManager.Instance.AddHandler(GameEvents.OnCashAmountChanged, HandleCashChange);

		}

		private void OnDisable()
		{
			if (EventManager.Instance == null)
			{
				return;
			}

			EventManager.Instance.RemoveHandler(GameEvents.OnGoldAmountChanged, HandleGoldChange);
			EventManager.Instance.RemoveHandler(GameEvents.OnCashAmountChanged, HandleCashChange);
		}

		private void HandleGoldChange()
		{
			HandleCurrencyString(goldAmountText, SaveManager.Instance.GetGoldAmount());
		}

		private void HandleCashChange()
		{
			HandleCurrencyString(cashAmountText, SaveManager.Instance.GetCashAmount());
		}

		private void HandleCurrencyString(TextMeshProUGUI amountText, int amount)
		{
			amountText.text = amount.ToString("N0");
			LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
		}
	}
}

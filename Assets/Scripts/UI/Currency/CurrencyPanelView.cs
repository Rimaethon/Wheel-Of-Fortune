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
			EventManager.SInstance.AddHandler(GameEvents.OnGoldAmountChanged, HandleGoldChange);
			EventManager.SInstance.AddHandler(GameEvents.ON_CASH_AMOUNT_CHANGED, HandleCashChange);

		}

		private void OnDisable()
		{
			if (EventManager.SInstance == null)
			{
				return;
			}

			EventManager.SInstance.RemoveHandler(GameEvents.OnGoldAmountChanged, HandleGoldChange);
			EventManager.SInstance.RemoveHandler(GameEvents.ON_CASH_AMOUNT_CHANGED, HandleCashChange);
		}

		private void HandleGoldChange()
		{
			HandleCurrencyString(goldAmountText, SaveManager.SInstance.GetGoldAmount());
		}

		private void HandleCashChange()
		{
			HandleCurrencyString(cashAmountText, SaveManager.SInstance.GetCashAmount());
		}

		private void HandleCurrencyString(TextMeshProUGUI amountText, int amount)
		{
			amountText.text = amount.ToString("N0");
			LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
		}
	}
}

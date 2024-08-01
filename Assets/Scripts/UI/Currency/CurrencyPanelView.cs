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
			HandleGoldChange( new OnGoldAmountChangedEventBase());
			HandleCashChange(new OnCashAmountChangedEventBase());
		}

		private void OnEnable()
		{
			EventManager.RegisterHandler<OnGoldAmountChangedEventBase>( HandleGoldChange);
			EventManager.RegisterHandler<OnCashAmountChangedEventBase>( HandleCashChange);
		}

		private void OnDisable()
		{
			EventManager.UnregisterHandler<OnGoldAmountChangedEventBase>( HandleGoldChange);
			EventManager.UnregisterHandler<OnCashAmountChangedEventBase>( HandleCashChange);
		}

		private void HandleGoldChange(OnGoldAmountChangedEventBase obj)
		{
			HandleCurrencyString(goldAmountText, SaveManager.SInstance.GetGoldAmount());
		}

		private void HandleCashChange(OnCashAmountChangedEventBase obj)
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

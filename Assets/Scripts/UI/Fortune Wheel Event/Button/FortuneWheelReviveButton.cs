using Enums;
using Managers;
using TMPro;
using UI.Buttons;
using UnityEngine;

namespace UI.Fortune_Wheel_Event.Button
{
	public class FortuneWheelReviveButton : UIButton
	{
		[SerializeField]
		private TextMeshProUGUI goldAmountText;
		[SerializeField]
		private int initialReviveCost = 100;
		private int reviveCount = 1;

		private void Awake()
		{
			goldAmountText.text = initialReviveCost.ToString();
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			EventManager.RegisterHandler<OnFortuneWheelExit>(HandleFortuneWheelExit);
		}

		private void HandleFortuneWheelExit(OnFortuneWheelExit obj)
		{
			reviveCount = 1;
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			EventManager.UnregisterHandler<OnFortuneWheelExit>(HandleFortuneWheelExit);

		}

		protected override void DoOnClick()
		{
			base.DoOnClick();
			OnFortuneWheelReviveButtonClicked onFortuneWheelReviveButtonClicked = new OnFortuneWheelReviveButtonClicked();
			onFortuneWheelReviveButtonClicked.amount = reviveCount * initialReviveCost;
			EventManager.Send(onFortuneWheelReviveButtonClicked);
			reviveCount++;
			goldAmountText.text = (reviveCount * initialReviveCost).ToString();
		}
	}
}

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
			EventManager.SInstance.AddHandler(GameEvents.ON_FORTUNE_WHEEL_EXIT, () => reviveCount=1);
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			EventManager.SInstance.RemoveHandler(GameEvents.ON_FORTUNE_WHEEL_EXIT, () => reviveCount=1);
		}

		protected override void DoOnClick()
		{
			base.DoOnClick();
			EventManager.SInstance.Broadcast(GameEvents.ON_FORTUNE_WHEEL_REVIVE_BUTTON_CLICKED, reviveCount * initialReviveCost);
			reviveCount++;
			goldAmountText.text = (reviveCount * initialReviveCost).ToString();
		}
	}
}

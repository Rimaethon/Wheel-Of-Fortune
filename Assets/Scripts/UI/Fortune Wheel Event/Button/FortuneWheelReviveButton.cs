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
			EventManager.Instance.AddHandler(GameEvents.OnFortuneWheelExit, () => reviveCount=1);
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			EventManager.Instance.RemoveHandler(GameEvents.OnFortuneWheelExit, () => reviveCount=1);
		}

		protected override void DoOnClick()
		{
			base.DoOnClick();
			EventManager.Instance.Broadcast(GameEvents.OnFortuneWheelReviveButtonClicked, reviveCount * initialReviveCost);
			reviveCount++;
			goldAmountText.text = (reviveCount * initialReviveCost).ToString();
		}
	}
}

using Enums;
using Managers;
using TMPro;
using UI.Buttons;
using UnityEngine;

namespace UI.Fortune_Wheel_Event.Button
{
	public class FortuneWheelExitButton : UIButton
	{
		protected override void DoOnClick()
		{
			base.DoOnClick();
			EventManager.Instance.Broadcast(GameEvents.OnFortuneWheelExitButtonClicked);
		}
	}
}

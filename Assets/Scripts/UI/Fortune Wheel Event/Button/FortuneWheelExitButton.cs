using Enums;
using Managers;
using UI.Buttons;

namespace UI.Fortune_Wheel_Event.Button
{
	public class FortuneWheelExitButton : UIButton
	{
		protected override void DoOnClick()
		{
			base.DoOnClick();
			EventManager.SInstance.Broadcast(GameEvents.OnFortuneWheelExitButtonClicked);
		}
	}
}

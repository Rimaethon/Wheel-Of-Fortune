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
			EventManager.Send(new OnFortuneWheelExitButtonClicked());
		}
	}
}

using Event_System;

namespace Enums
{
	public class OnFortuneWheelReviveButtonClicked: EventBase
	{
		public int amount;
	}
	public class OnFortuneWheelExitButtonClicked: EventBase
	{
	}
	public class OnFortuneWheelExit: EventBase
	{
	}
	public class OnFortuneWheelBombExploded: EventBase
	{
	}
	public class OnFortuneWheelLevelChanged: EventBase
	{
	}
	public class FortuneWheelUpdatedEventBase: EventBase
	{
		public int Level;
	}
	public class OnCashAmountChangedEventBase: EventBase
	{

	}
	public class OnGoldAmountChangedEventBase: EventBase
	{
	}
}

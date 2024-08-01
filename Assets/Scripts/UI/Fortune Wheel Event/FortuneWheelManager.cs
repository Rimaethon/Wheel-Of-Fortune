using Enums;
using Managers;
using UnityEngine;

namespace UI.Fortune_Wheel_Event
{
	public class FortuneWheelManager : MonoBehaviour
	{
		[SerializeField]
		private GameObject bombExplodedView;

		[SerializeField]
		private FortuneWheelView fortuneWheelView;

		[SerializeField]
		private GameObject mainMenuView;
		private bool isBombScreenOpen;

		private void OnEnable()
		{
			EventManager.SInstance.AddHandler<int>(GameEvents.ON_FORTUNE_WHEEL_REVIVE_BUTTON_CLICKED, HandleReviveButtonClicked);
			EventManager.SInstance.AddHandler(GameEvents.OnFortuneWheelBombExploded, HandleFortuneWheelBombExploded);
			EventManager.SInstance.AddHandler(GameEvents.OnFortuneWheelExitButtonClicked, HandleExitButtonClicked);
		}

		private void OnDisable()
		{
			if (EventManager.SInstance == null)
				return;

			EventManager.SInstance.RemoveHandler<int>(GameEvents.ON_FORTUNE_WHEEL_REVIVE_BUTTON_CLICKED, HandleReviveButtonClicked);
			EventManager.SInstance.RemoveHandler(GameEvents.OnFortuneWheelBombExploded, HandleFortuneWheelBombExploded);
			EventManager.SInstance.RemoveHandler(GameEvents.OnFortuneWheelExitButtonClicked, HandleExitButtonClicked);
		}

		private void HandleFortuneWheelBombExploded()
		{
			isBombScreenOpen = true;
			bombExplodedView.SetActive(true);
		}

		private void HandleReviveButtonClicked(int goldAmount)
		{
			bombExplodedView.SetActive(false);
			isBombScreenOpen = false;
			SaveManager.SInstance.ChangeGoldAmount(-goldAmount);
		}

		private void HandleExitButtonClicked()
		{
			if(isBombScreenOpen||fortuneWheelView.IsSpinning)
				return;
			EventManager.SInstance.Broadcast(GameEvents.ON_FORTUNE_WHEEL_EXIT);
			bombExplodedView.SetActive(false);
			gameObject.SetActive(false);
			mainMenuView.SetActive(true);
		}
	}
}

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
			EventManager.RegisterHandler<OnFortuneWheelReviveButtonClicked>(HandleReviveButtonClicked);
			EventManager.RegisterHandler<OnFortuneWheelBombExploded>(HandleFortuneWheelBombExploded);
			EventManager.RegisterHandler<OnFortuneWheelExitButtonClicked>(HandleExitButtonClicked);
		}

		private void HandleExitButtonClicked(OnFortuneWheelExitButtonClicked obj)
		{
			Debug.Log("Exit button clicked");
			if(isBombScreenOpen||fortuneWheelView.IsSpinning)
				return;
			EventManager.Send(new OnFortuneWheelBombExploded());
			bombExplodedView.SetActive(false);
			gameObject.SetActive(false);
			mainMenuView.SetActive(true);
		}

		private void OnDisable()
		{
			EventManager.UnregisterHandler<OnFortuneWheelReviveButtonClicked>(HandleReviveButtonClicked);
			EventManager.UnregisterHandler<OnFortuneWheelBombExploded>(HandleFortuneWheelBombExploded);
			EventManager.UnregisterHandler<OnFortuneWheelExitButtonClicked>(HandleExitButtonClicked);
		}

		private void HandleFortuneWheelBombExploded( OnFortuneWheelBombExploded data)
		{
			isBombScreenOpen = true;
			bombExplodedView.SetActive(true);
		}

		private void HandleReviveButtonClicked(OnFortuneWheelReviveButtonClicked data)
		{
			bombExplodedView.SetActive(false);
			isBombScreenOpen = false;
			SaveManager.SInstance.ChangeGoldAmount(-data.amount);
		}

	}
}

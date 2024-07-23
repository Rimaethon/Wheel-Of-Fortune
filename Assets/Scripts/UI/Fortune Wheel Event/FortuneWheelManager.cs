using System;
using Enums;
using Managers;
using UnityEngine;

namespace UI.Fortune_Wheel_Event
{
	public class FortuneWheelManager : MonoBehaviour
	{
		[SerializeField]
		private GameObject BombExplodedView;

		[SerializeField]
		private FortuneWheelView fortuneWheelView;

		[SerializeField]
		private GameObject mainMenuView;
		private bool isBombScreenOpen;

		private void OnEnable()
		{
			EventManager.Instance.AddHandler<int>(GameEvents.OnFortuneWheelReviveButtonClicked, HandleReviveButtonClicked);
			EventManager.Instance.AddHandler(GameEvents.OnFortuneWheelBombExploded, HandleFortuneWheelBombExploded);
			EventManager.Instance.AddHandler(GameEvents.OnFortuneWheelExitButtonClicked, HandleExitButtonClicked);
		}

		private void OnDisable()
		{
			if (EventManager.Instance == null)
				return;

			EventManager.Instance.RemoveHandler<int>(GameEvents.OnFortuneWheelReviveButtonClicked, HandleReviveButtonClicked);
			EventManager.Instance.RemoveHandler(GameEvents.OnFortuneWheelBombExploded, HandleFortuneWheelBombExploded);
			EventManager.Instance.RemoveHandler(GameEvents.OnFortuneWheelExitButtonClicked, HandleExitButtonClicked);
		}

		private void HandleFortuneWheelBombExploded()
		{
			isBombScreenOpen = true;
			BombExplodedView.SetActive(true);
		}

		private void HandleReviveButtonClicked(int goldAmount)
		{
			BombExplodedView.SetActive(false);
			isBombScreenOpen = false;
			SaveManager.Instance.ChangeGoldAmount(-goldAmount);
		}

		private void HandleExitButtonClicked()
		{
			if(isBombScreenOpen||fortuneWheelView.IsSpinning)
				return;
			EventManager.Instance.Broadcast(GameEvents.OnFortuneWheelExit);
			BombExplodedView.SetActive(false);
			gameObject.SetActive(false);
			mainMenuView.SetActive(true);
		}
	}
}

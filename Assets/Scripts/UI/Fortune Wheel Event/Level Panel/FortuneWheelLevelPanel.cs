using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Enums;
using Managers;
using UnityEngine;

namespace UI.Fortune_Wheel_Event.Level_Panel
{
	public class FortuneWheelLevelPanel : MonoBehaviour
	{
		[SerializeField]
		private GameObject numberPrefab;
		[SerializeField]
		private GameObject numberBackgroundPrefab;
		[SerializeField]
		private RectTransform levelNumberHolder;
		[SerializeField]
		private RectTransform backgroundHolder;
		[SerializeField]
		private RectTransform currentNumberHolderRectTransform;

		[SerializeField]
		private int safeLevelCoefficient = 5;
		[SerializeField]
		private int superLevelCoefficient = 30;
		private readonly Queue<FortuneWheelLevelBackgroundView> backgroundUIElements = new Queue<FortuneWheelLevelBackgroundView>();
		private readonly float levelNumberHolderScaleMultiplier = 1.07f;

		private readonly int maxVisibleNumbers = 14;
		private readonly Color normalLevelColor = Color.white;
		private readonly int numberOfPositions = 15;

		private readonly Queue<FortuneWheelLevelNumberView> numberUIElements = new Queue<FortuneWheelLevelNumberView>();
		private readonly Color safeLevelColor = Color.green;

		private readonly Color superLevelColor = new Color(1, 0.5f, 0.1f, 1);
		private Vector3[] backgroundPositions;
		private int currentLevel = 1;

		private float elementWidth;
		private Vector3[] numberPositions;

		private void Awake()
		{
			numberPositions = new Vector3[numberOfPositions];
			backgroundPositions = new Vector3[3];
			InitializePositions();
		}

		private void OnEnable()
		{
			InitializeNumberElements();
			EventManager.Instance.AddHandler(GameEvents.FortuneWheelLevelChanged, HandleLevelChanged);
		}

		private void OnDisable()
		{
			foreach (var element  in numberUIElements)
			{
				Destroy(element.gameObject);
			}
			numberUIElements.Clear();
			currentLevel = 1;
			if (EventManager.Instance == null)
				return;
			EventManager.Instance.RemoveHandler(GameEvents.FortuneWheelLevelChanged, HandleLevelChanged);
		}

		private void HandleLevelChanged()
		{
			HandleLevelChange().Forget();

		}

		private async UniTask HandleLevelChange()
		{
			currentLevel++;

			foreach (FortuneWheelLevelNumberView element in numberUIElements)
			{
				element.rectTransform.DOAnchorPos(numberPositions[element.index - 1], 0.5f).SetUpdate(UpdateType.Fixed);
				element.index--;
			}

			HandleBackgroundImageTransition();
			await UniTask.Delay(550);

			FortuneWheelLevelNumberView fortuneWheelLevelNumberView = numberUIElements.Count != maxVisibleNumbers ?
																		  Instantiate(numberPrefab, levelNumberHolder).GetComponent<FortuneWheelLevelNumberView>() : numberUIElements.Dequeue();

			fortuneWheelLevelNumberView.rectTransform.anchoredPosition = numberPositions[numberOfPositions - 1];
			fortuneWheelLevelNumberView.index = numberOfPositions - 1;
			fortuneWheelLevelNumberView.numberText.color = GetLevelColor(currentLevel + maxVisibleNumbers / 2);
			fortuneWheelLevelNumberView.numberText.text = (currentLevel + maxVisibleNumbers / 2).ToString();
			numberUIElements.Enqueue(fortuneWheelLevelNumberView);
		}

		private void HandleBackgroundImageTransition()
		{

			FortuneWheelLevelBackgroundView fortuneWheelLevelBackground1 = backgroundUIElements.Dequeue();
			FortuneWheelLevelBackgroundView fortuneWheelLevelBackground2 = backgroundUIElements.Dequeue();
			fortuneWheelLevelBackground2.numberBackground.color = GetLevelColor(currentLevel);
			fortuneWheelLevelBackground1.rectTransform.DOAnchorPos(backgroundPositions[0], 0.5f).SetUpdate(UpdateType.Fixed).onComplete += () => { fortuneWheelLevelBackground1.rectTransform.anchoredPosition = backgroundPositions[2]; };
			fortuneWheelLevelBackground2.rectTransform.DOAnchorPos(backgroundPositions[1], 0.5f).SetUpdate(UpdateType.Fixed);
			backgroundUIElements.Enqueue(fortuneWheelLevelBackground2);
			backgroundUIElements.Enqueue(fortuneWheelLevelBackground1);
		}

		private void InitializePositions()
		{
			elementWidth = levelNumberHolder.rect.width * levelNumberHolderScaleMultiplier / maxVisibleNumbers;

			for (int i = 0; i < numberOfPositions; i++)
			{
				numberPositions[i] = new Vector3(elementWidth * i - levelNumberHolder.rect.width * levelNumberHolderScaleMultiplier / 2, 0, 0);
			}

			backgroundPositions[0] = new Vector3(-elementWidth, 0, 0);
			backgroundPositions[1] = new Vector3(0, 0, 0);
			backgroundPositions[2] = new Vector3(elementWidth, 0, 0);
		}

		private void InitializeNumberElements()
		{
			for (int i = 0; i < maxVisibleNumbers / 2 + 1; i++)
			{
				GameObject number = Instantiate(numberPrefab, levelNumberHolder);
				FortuneWheelLevelNumberView fortuneWheelLevelNumberView = number.GetComponent<FortuneWheelLevelNumberView>();
				fortuneWheelLevelNumberView.numberText.color = GetLevelColor(i + 1);
				fortuneWheelLevelNumberView.numberText.text = (i + 1).ToString();
				numberUIElements.Enqueue(fortuneWheelLevelNumberView);

				if (i == maxVisibleNumbers - 1)
				{
					fortuneWheelLevelNumberView.gameObject.SetActive(false);

				}

				fortuneWheelLevelNumberView.rectTransform.anchoredPosition = numberPositions[7 + i];
				fortuneWheelLevelNumberView.index = 7 + i;
			}

			for (int i = 0; i < 2; i++)
			{
				FortuneWheelLevelBackgroundView fortuneWheelLevelBackground = Instantiate(numberBackgroundPrefab, backgroundHolder).GetComponent<FortuneWheelLevelBackgroundView>();
				fortuneWheelLevelBackground.rectTransform.anchoredPosition = backgroundPositions[i + 1];
				fortuneWheelLevelBackground.numberBackground.color = GetLevelColor(1);
				backgroundUIElements.Enqueue(fortuneWheelLevelBackground);
			}
		}

		private Color GetLevelColor(int level)
		{
			if (level % superLevelCoefficient == 0)
			{
				return superLevelColor;
			}

			if (level % safeLevelCoefficient == 0 || level == 1)
			{
				return safeLevelColor;
			}

			return normalLevelColor;
		}
	}
}

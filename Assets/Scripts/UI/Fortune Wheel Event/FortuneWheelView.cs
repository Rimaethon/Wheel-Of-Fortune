using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Data;
using Data.Scriptable_Objects;
using DG.Tweening;
using Enums;
using Managers;
using Sirenix.OdinInspector;
using UI.Fortune_Wheel_Event.Gained_Item_Panel;
using UnityEngine;

namespace UI.Fortune_Wheel_Event
{
	public class FortuneWheelView : MonoBehaviour
	{
		public bool IsSpinning => isSpinning;

		[ShowInInspector]
		private UnityEngine.UI.Button spinButton;

		[SerializeField]
		private RectTransform wheelTransform;

		[SerializeField]
		private FortuneWheelGainedItemPanel fortuneWheelGainedItemPanel;

		[SerializeField]
		private FortuneWheelRewardAnimationHandler fortuneWheelRewardAnimationHandler;

		[SerializeField]
		private SpinRewardListSO lowTierRewardList;

		[SerializeField]
		private SpinRewardListSO midTierRewardList;

		[SerializeField]
		private SpinRewardListSO highTierRewardList;

		private SpinRewardListSO currentRewardList;

		[SerializeField]
		private float rewardMultiplierIncreaseRate = 0.2f;

		[SerializeField]
		private int fullRotationAngle = 360;

		[SerializeField]
		private int LastLevel = 60;

		[SerializeField]
		private Sprite bombSprite;

		[SerializeField]
		private string bombName;

		[Range(0, 1), SerializeField]
		private float bombChance;
		private SpinRewardData bombData;
		private List<FortuneWheelItemView> rewardElements = new List<FortuneWheelItemView>();
		private readonly List<float> cumulativeChances = new List<float>();
		private float cumulativeChance;
		private int numberOfSlots;
		private bool isSpinning;
		private int bombIndex;
		private int currentLevel=1;
		private const float angle_between_prizes = 45f;
		private bool isSafeLevel = true;
		private float currentRewardMultiplier = 1;

		private void Awake()
		{
			CheckComponents();
			numberOfSlots = rewardElements.Count;
			bombData = new SpinRewardData
			{
				sprite = bombSprite,
				ItemName = bombName,
				BaseChance = bombChance,
				BaseAmount = 0
			};
		}

		private void OnValidate()
		{
			CheckComponents();
		}

		private void CheckComponents()
		{
			if (spinButton == null)
			{
				spinButton = GetComponentInChildren<UnityEngine.UI.Button>();
			}
			if (rewardElements.Count == 0)
			{
				rewardElements = wheelTransform.GetComponentsInChildren<FortuneWheelItemView>().ToList();
			}
		}

		private void OnEnable()
		{
			spinButton.onClick.AddListener(SpinWheel);
			cumulativeChances.Clear();
			cumulativeChance = 0;
			currentLevel = 1;
			InitializeEventData(lowTierRewardList,true);
		}

		private void OnDisable()
		{
			spinButton.onClick.RemoveListener(SpinWheel);
		}

		private void InitializeEventData(SpinRewardListSO rewardList,bool isSafeLevel = false)
		{
			currentRewardList = rewardList;
			for (int i = 0; i < numberOfSlots - 1; i++)
			{
				cumulativeChance += rewardList.Database[i].BaseChance;
				cumulativeChances.Add(cumulativeChance);
				rewardElements[i].InitializeReward(rewardList.Database[i], currentRewardMultiplier);
			}
			this.isSafeLevel = isSafeLevel;
			if (isSafeLevel)
			{
				cumulativeChance += rewardList.Database[numberOfSlots - 1].BaseChance;
				cumulativeChances.Add(cumulativeChance);
				rewardElements[numberOfSlots - 1].InitializeReward(rewardList.Database[numberOfSlots - 1], currentRewardMultiplier);
			}
			else
			{
				cumulativeChance += bombChance;
				cumulativeChances.Add(cumulativeChance);
				rewardElements[numberOfSlots - 1].InitializeReward(bombData, currentRewardMultiplier);
			}
		}

		private void SpinWheel()
		{
			if (isSpinning||currentLevel==LastLevel)
				return;

			isSpinning = true;
			float randomChance = Random.Range(0, cumulativeChance);
			int prizeIndex = 0;

			for (int i = 0; i < cumulativeChances.Count; i++)
			{
				if (!(randomChance <= cumulativeChances[i])) continue;
				prizeIndex = i;
				break;
			}

			float angleToPrize = prizeIndex * angle_between_prizes;
			float randomAngle = (fullRotationAngle*prizeIndex) + angleToPrize;

			wheelTransform.DORotate(new Vector3(0, 0, randomAngle), 2f, RotateMode.FastBeyond360)
						  .SetUpdate(UpdateType.Fixed)
						  .SetEase(Ease.OutQuint)
						  .onComplete += () =>
										 {
											 EventManager.Send( new OnFortuneWheelLevelChanged());
											  CalculateReward().Forget();
										 };
		}

		private async UniTask CalculateReward()
		{
			int rotateIndex = (int) Mathf.Abs((wheelTransform.rotation.eulerAngles.z + angle_between_prizes / 2) % 360 / angle_between_prizes);
			currentLevel++;
			if (rotateIndex == 7&&!isSafeLevel)
			{
				EventManager.Send( new OnFortuneWheelBombExploded());
				isSpinning = false;
				return;
			}
			await fortuneWheelRewardAnimationHandler.PlayGainedItemEffect(rewardElements[rotateIndex].transform.position,
																		  fortuneWheelGainedItemPanel.GetGainedItemIconPosition(currentRewardList.Database[rotateIndex], currentRewardMultiplier)
																		, currentRewardList.Database[rotateIndex].sprite);

			await UniTask.Delay(400);
			currentRewardMultiplier += rewardMultiplierIncreaseRate*currentRewardMultiplier;
			HandleTierChange();
			isSpinning = false;
		}

		private void HandleTierChange()
		{
			cumulativeChance = 0;
			cumulativeChances.Clear();
			if(currentLevel%30==0)
			{
				InitializeEventData(highTierRewardList,true);
				return;
			}

			if (currentLevel % 5 == 0)
			{
				InitializeEventData(midTierRewardList,true);
				return;
			}
			InitializeEventData(lowTierRewardList);
		}
	}
}

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
		public bool IsSpinning => _isSpinning;

		[ShowInInspector]
		private UnityEngine.UI.Button spinButton;

		[SerializeField]
		private RectTransform wheelTransform;

		[SerializeField]
		private GameObject GainedPrizesHolder;

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
		private int LastLevel = 60;

		[SerializeField]
		private Sprite bombSprite;
		[SerializeField]
		private string bombName;
		[Range(0, 1), SerializeField]
		private float bombChance;
		private SpinRewardData bombData;

		private List<FortuneWheelItemView> rewardElements = new List<FortuneWheelItemView>();
		private readonly List<float> _cumulativeChances = new List<float>();
		private float cumulativeChance;
		private int numberOfSlots;
		private bool _isSpinning;
		private int BombIndex;
		private int _currentLevel=1;
		private const float angle_between_prizes = 45f;
		private bool _isSafeLevel = true;
		private float _currentRewardMultiplier = 1;

		private void Awake()
		{
			if (rewardElements.Count == 0)
			{
				rewardElements = wheelTransform.GetComponentsInChildren<FortuneWheelItemView>().ToList();
			}
			numberOfSlots = rewardElements.Count;
			bombData = new SpinRewardData
			{
				sprite = bombSprite,
				ItemName = bombName,
				BaseChance = bombChance,
				BaseAmount = 0
			};

		}

		private void OnEnable()
		{
			spinButton.onClick.AddListener(SpinWheel);
			_cumulativeChances.Clear();
			cumulativeChance = 0;
			_currentLevel = 1;
			InitializeEventData(lowTierRewardList,true);
		}

		private void OnDisable()
		{
			spinButton.onClick.RemoveListener(SpinWheel);
		}

		private void OnValidate()
		{
			if (spinButton != null)
			{
				return;
			}
			spinButton = GetComponentInChildren<UnityEngine.UI.Button>();
			if (rewardElements.Count == 0)
			{
				rewardElements = wheelTransform.GetComponentsInChildren<FortuneWheelItemView>().ToList();
			}
		}

		private void InitializeEventData(SpinRewardListSO rewardList,bool isSafeLevel = false)
		{
			currentRewardList = rewardList;
			for (int i = 0; i < numberOfSlots - 1; i++)
			{
				cumulativeChance += rewardList.Database[i].BaseChance;
				_cumulativeChances.Add(cumulativeChance);
				rewardElements[i].InitializeReward(rewardList.Database[i], _currentRewardMultiplier);
			}
			_isSafeLevel = isSafeLevel;
			if (isSafeLevel)
			{
				cumulativeChance += rewardList.Database[numberOfSlots - 1].BaseChance;
				_cumulativeChances.Add(cumulativeChance);
				rewardElements[numberOfSlots - 1].InitializeReward(rewardList.Database[numberOfSlots - 1], _currentRewardMultiplier);
			}
			else
			{
				cumulativeChance += bombChance;
				_cumulativeChances.Add(cumulativeChance);
				rewardElements[numberOfSlots - 1].InitializeReward(bombData, _currentRewardMultiplier);
			}
		}

		private void SpinWheel()
		{
			if (_isSpinning||_currentLevel==LastLevel)
			{
				return;
			}

			_isSpinning = true;
			float randomChance = Random.Range(0, cumulativeChance);
			int prizeIndex = 0;

			for (int i = 0; i < _cumulativeChances.Count; i++)
			{
				if (randomChance <= _cumulativeChances[i])
				{
					prizeIndex = i;
					break;
				}
			}

			float angleToPrize = prizeIndex * angle_between_prizes;
			float randomAngle = 720 + angleToPrize;

			wheelTransform.DORotate(new Vector3(0, 0, randomAngle), 2f, RotateMode.FastBeyond360)
						  .SetUpdate(UpdateType.Fixed)
						  .SetEase(Ease.OutQuint)
						  .onComplete += () =>
										 {
											  EventManager.Instance.Broadcast(GameEvents.FortuneWheelLevelChanged);
											  CalculateReward().Forget();
										 };
		}

		private async UniTask CalculateReward()
		{
			int rotateIndex = (int) Mathf.Abs((wheelTransform.rotation.eulerAngles.z + angle_between_prizes / 2) % 360 / angle_between_prizes);
			_currentLevel++;
			if (rotateIndex == 7)
			{
				EventManager.Instance.Broadcast(GameEvents.OnFortuneWheelBombExploded);
				_isSpinning = false;
				return;
			}
			await fortuneWheelRewardAnimationHandler.PlayGainedItemEffect(rewardElements[rotateIndex].transform.position,
																		  fortuneWheelGainedItemPanel.GetGainedItemIconPosition(currentRewardList.Database[rotateIndex], _currentRewardMultiplier)
																		, currentRewardList.Database[rotateIndex].sprite);

			await UniTask.Delay(400);
			_currentRewardMultiplier += rewardMultiplierIncreaseRate*_currentRewardMultiplier;
			HandleTierChange();
			_isSpinning = false;
		}

		private void HandleTierChange()
		{
			cumulativeChance = 0;
			_cumulativeChances.Clear();
			if(_currentLevel%30==0)
			{
				InitializeEventData(highTierRewardList,true);
				return;
			}

			if (_currentLevel % 5 == 0)
			{
				InitializeEventData(midTierRewardList,true);
				return;
			}
			InitializeEventData(lowTierRewardList);


		}
	}
}

using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Fortune_Wheel_Event
{
	public class FortuneWheelItemView : MonoBehaviour
	{
		private int rewardAmount;
		private TextMeshProUGUI rewardAmountText;
		private float rewardChance;
		private Image rewardImage;

		private void Awake()
		{
			rewardImage = GetComponentInChildren<Image>();
			rewardAmountText = GetComponentInChildren<TextMeshProUGUI>();
		}

		private void OnValidate()
		{
			if (rewardImage == null)
			{
				rewardImage = GetComponentInChildren<Image>();
			}

			if (rewardAmountText == null)
			{
				rewardAmountText = GetComponentInChildren<TextMeshProUGUI>();
			}
		}

		public void InitializeReward(SpinRewardData rewardData, float rewardMultiplier)
		{
			rewardImage.sprite = rewardData.sprite;
			rewardChance = rewardData.BaseChance;
			rewardAmount = (int) (rewardData.BaseAmount * rewardMultiplier);

			if (rewardData.BaseAmount == 0)
			{
				rewardAmountText.gameObject.SetActive(false);
				rewardImage.rectTransform.anchorMin = new Vector2(0, 0);
				return;
			}
			rewardAmountText.text = "X" + rewardAmount;

		}
	}
}

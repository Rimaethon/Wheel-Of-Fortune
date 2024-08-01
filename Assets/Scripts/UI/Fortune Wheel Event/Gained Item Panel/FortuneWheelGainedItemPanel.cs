using System.Collections.Generic;
using Data;
using UnityEngine;

namespace UI.Fortune_Wheel_Event.Gained_Item_Panel
{
	public class FortuneWheelGainedItemPanel : MonoBehaviour
	{
		[SerializeField]
		private GameObject gainedItemPrefab;

		[SerializeField]
		private RectTransform content;

		private readonly List<FortuneWheelGainedItemView> gainedItemViewList = new List<FortuneWheelGainedItemView>();
		private float itemHeight;
		private float lastItemYPos = 280;

		private void Awake()
		{
			itemHeight = gainedItemPrefab.GetComponent<RectTransform>().rect.height;
		}

		private void OnDisable()
		{
			foreach (FortuneWheelGainedItemView item in gainedItemViewList)
			{
				Destroy(item.gameObject);
			}
			gainedItemViewList.Clear();
			lastItemYPos = 280;
		}

		public Vector3 GetGainedItemIconPosition(SpinRewardData data,float rewardMultiplier)
		{
			FortuneWheelGainedItemView fortuneWheelGainedItemView = gainedItemViewList.Find(x => x.itemID == data.ItemData.ItemID && x.itemType == data.ItemData.ItemType);

			if (fortuneWheelGainedItemView == null)
			{
				fortuneWheelGainedItemView = Instantiate(gainedItemPrefab, content).GetComponent<FortuneWheelGainedItemView>();
				gainedItemViewList.Add(fortuneWheelGainedItemView);
				fortuneWheelGainedItemView.rectTransform.anchoredPosition = new Vector2(0, lastItemYPos);
				lastItemYPos -= itemHeight + 10;
				fortuneWheelGainedItemView.itemID = data.ItemData.ItemID;
				fortuneWheelGainedItemView.itemIcon.sprite = data.sprite;
				fortuneWheelGainedItemView.itemType = data.ItemData.ItemType;

				Canvas.ForceUpdateCanvases();
			}

			fortuneWheelGainedItemView.amount += (int)(data.BaseAmount*rewardMultiplier);
			fortuneWheelGainedItemView.amountText.text = fortuneWheelGainedItemView.amount.ToString();
			return fortuneWheelGainedItemView.itemIcon.rectTransform.position;
		}
	}
}

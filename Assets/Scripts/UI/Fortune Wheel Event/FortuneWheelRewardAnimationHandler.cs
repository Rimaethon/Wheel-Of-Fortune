using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace UI.Fortune_Wheel_Event
{
	public class FortuneWheelRewardAnimationHandler : MonoBehaviour
	{
		[SerializeField]
		private float distributionRange = 300f;
		[SerializeField]
		private float distributionDuration = 1f;
		[SerializeField]
		private float movementDuration = 0.8f;
		private readonly List<RectTransform> iconRectTransforms = new List<RectTransform>();
		private readonly List<Image> itemImages = new List<Image>();
		private readonly Random random = new Random();

		private void Awake()
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				iconRectTransforms.Add(transform.GetChild(i).GetComponent<RectTransform>());
				itemImages.Add(transform.GetChild(i).GetComponent<Image>());
				transform.GetChild(i).gameObject.SetActive(false);
			}
		}

		public async UniTask PlayGainedItemEffect(Vector3 startPos, Vector3 endPos, Sprite itemSprite)
		{
			foreach (Image image in itemImages)
			{
				image.sprite = itemSprite;
			}

			foreach (RectTransform rectTransform in iconRectTransforms)
			{
				rectTransform.position = startPos;

				Vector3 randomOffset = new Vector3(
												   (float) (random.NextDouble() * 2 - 1) * distributionRange,
												   (float) (random.NextDouble() * 2 - 1) * distributionRange,
												   0
												  );

				rectTransform.gameObject.SetActive(true);
				rectTransform.DOLocalMove(rectTransform.localPosition + randomOffset, distributionDuration).SetUpdate(UpdateType.Fixed);
			}

			await UniTask.Delay((int) (distributionDuration * 1000));

			foreach (RectTransform rectTransform in iconRectTransforms)
			{
				rectTransform.DOMove(endPos, movementDuration).SetUpdate(UpdateType.Fixed);
			}

			await UniTask.Delay((int) (movementDuration * 1000));

			foreach (RectTransform rectTransform in iconRectTransforms)
			{
				rectTransform.gameObject.SetActive(false);
			}
		}
	}
}

using UnityEngine;

//This doesn't work properly since holes in the spin texture are not evenly distributed
namespace Utility
{
	public class WheelElementFiller : MonoBehaviour
	{
		[SerializeField]
		private int numberOfElements;
		[SerializeField]
		private float WheelRadiusRewardRadiusRatio;
		[SerializeField]
		private float WheelSizeToElementSizeRatio;
		[SerializeField]
		private GameObject wheelElementPrefab;
		private RectTransform rectTransform;

		private void Awake()
		{
			rectTransform = GetComponent<RectTransform>();
			FillWheel();
		}

		private void FillWheel()
		{
			Vector2 wheelElementSize = new Vector2(rectTransform.rect.width * WheelSizeToElementSizeRatio, rectTransform.rect.height * WheelSizeToElementSizeRatio);

			for (int i = 0; i < numberOfElements; i++)
			{
				Vector3 position = new Vector3(rectTransform.rect.width / 2 * WheelRadiusRewardRadiusRatio * Mathf.Sin(i * Mathf.PI * 2 / numberOfElements),
											   rectTransform.rect.height / 2 * WheelRadiusRewardRadiusRatio * Mathf.Cos(i * Mathf.PI * 2 / numberOfElements), 0);

				RectTransform wheelElement = Instantiate(wheelElementPrefab, transform).GetComponent<RectTransform>();
				wheelElement.anchoredPosition = position;

				wheelElement.localRotation = Quaternion.Euler(0, 0, -(float) i * 360 / numberOfElements);
				wheelElement.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, wheelElementSize.y);
				wheelElement.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, wheelElementSize.x);

			}
		}
	}
}

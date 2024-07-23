using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Buttons
{
	public abstract class UIButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
	{
		protected Button Button;
		protected RectTransform RectTransform;
		protected Tween ScaleDownTween;
		protected Tween ScaleUpTween;
		protected Vector3 OriginalScale => Vector3.one;

		protected virtual void OnEnable()
		{
			transform.localScale = OriginalScale;
		}

		protected virtual void OnDisable()
		{
			ScaleDownTween.Kill();
			ScaleUpTween.Kill();
		}

		private void OnValidate()
		{
			if (Button == null)
			{
				Button = GetComponent<Button>();
			}

			if (RectTransform == null)
			{
				RectTransform = GetComponent<RectTransform>();
			}
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (!Button.isActiveAndEnabled)
			{
				return;
			}

			DoOnClick();
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			if (!Button.isActiveAndEnabled)
			{
				return;
			}

			DoOnPointerDown();
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (!Button.isActiveAndEnabled)
			{
				return;
			}

			DoOnPointerUp();
		}

		protected virtual void DoOnClick()
		{
			ScaleDownTween.Kill();
			ScaleUpTween.Kill();
			RectTransform.localScale = OriginalScale;
		}

		protected virtual void DoOnPointerDown()
		{
			ScaleDownTween = RectTransform.DOScale(OriginalScale * 0.95f, 0.075f).SetUpdate(UpdateType.Fixed);
		}

		protected virtual void DoOnPointerUp()
		{
			ScaleUpTween = RectTransform.DOScale(OriginalScale, 0.075f).SetUpdate(UpdateType.Fixed);
		}
	}
}

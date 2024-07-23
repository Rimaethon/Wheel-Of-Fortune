using UnityEngine;

namespace UI.Buttons
{
	public class CanvasChangerButton : UIButton
	{
		[SerializeField]
		private Canvas canvasToOpen;
		[SerializeField]
		private Canvas canvasToClose;

		protected override void DoOnClick()
		{
			base.DoOnClick();
			canvasToOpen.gameObject.SetActive(true);
			canvasToClose.gameObject.SetActive(false);
		}
	}
}

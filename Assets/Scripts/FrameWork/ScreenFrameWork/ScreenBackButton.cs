using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ScreenFrameWork
{
	[RequireComponent(typeof(Button))]

	public class ScreenBackButton : MonoBehaviour
	{
		#region Inspector Variables

		[SerializeField] private float fadeDuration = 0.5f;
		[SerializeField] private string scene;
		[SerializeField] private float alphaStart = 0;
		#endregion

		#region Properties

		private Button Button { get { return gameObject.GetComponent<Button>(); } }
		private CanvasGroup CanvasGroup { get { return gameObject.GetComponent<CanvasGroup>(); } }

		#endregion

		#region Unity Methods

		private void Start()
		{
			Button.onClick.AddListener(OnButtonClicked);

			CanvasGroup.alpha = alphaStart;

			ScreenManager.Instance.OnSwitchingScreens += OnSwitchingScreens;
		}

		#endregion

		#region Private Methods

		private void OnButtonClicked()
		{

			ScreenManager.Instance.Back(scene);
		}

		private void OnSwitchingScreens(string fromScreenId, string toScreenId)
		{
			if (toScreenId == ScreenManager.Instance.HomeScreenId)
			{
				// Fade out the back button
				PlayAnimation(UIAnimation.Alpha(gameObject, 1f, 0f, fadeDuration));
				Button.interactable = false;
			}
			else if (fromScreenId == ScreenManager.Instance.HomeScreenId)
			{
				// Fade in the back button
				PlayAnimation(UIAnimation.Alpha(gameObject, 0f, 1f, fadeDuration));
				Button.interactable = true;
			}
		}

		private void PlayAnimation(UIAnimation anim)
		{
			anim.style = UIAnimation.Style.EaseOut;
			anim.startOnFirstFrame = true;
			anim.Play();
		}

		#endregion
	}
}

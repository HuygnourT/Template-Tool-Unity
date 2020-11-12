using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;
using ScreenFrameWork;
namespace PopUpFrameWork
{
	public class Popup : UIMonoBehaviour
	{
		#region Enums

		protected enum AnimType
		{
			Fade,
			Zoom
		}

		#endregion

		#region Inspector Variables

		[SerializeField] protected bool				canAndroidBackClosePopup;

		[Header("Anim Settings")]
		[SerializeField] protected float			animDuration;
		[SerializeField] protected AnimType			animType;
		[SerializeField] protected AnimationCurve	animCurve;
		[SerializeField] protected RectTransform	animContainer;

		#endregion

		#region Member Variables

		private bool		isInitialized;
		private bool		isShowing;
		private PopupClosed	callback;

		#endregion

		#region Properties

		public bool CanAndroidBackClosePopup { get { return canAndroidBackClosePopup; } }

		#endregion

		#region Delegates

		public delegate void PopupClosed(bool cancelled, object[] outData);

		#endregion

		#region Public Methods

		public virtual void Initialize()
		{

		}

		public void Show()
		{
			Show(null, null);
		}

		public void Show(object[] inData, PopupClosed callback,UnityAction actionCallBack =null)
		{
			this.callback = callback;

			if (isShowing)
			{
				return;
			}

			isShowing = true;

			// Show the popup object
			gameObject.SetActive(true);

			switch (animType)
			{
				case AnimType.Fade:
					DoFadeAnim();
					break;
				case AnimType.Zoom:
					DoZoomAnim();
					break;
			}

			OnShowing(inData);
            OnShowing(actionCallBack);
        }




		public void Hide(bool cancelled)
		{
			Hide(cancelled, null);
		}

		public void Hide(bool cancelled, object[] outData)
		{
			if (!isShowing)
			{
				return;
			}

			isShowing = false;

			if (callback != null)
			{
				callback(cancelled, outData);
			}

			// Start the popup hide animations
			UIAnimation anim = null;

			anim = UIAnimation.Alpha(gameObject, 1f, 0f, animDuration);
			anim.style = UIAnimation.Style.EaseOut;
			anim.startOnFirstFrame = true;

			anim.OnAnimationFinished += (GameObject target) => 
			{
				gameObject.SetActive(false);
			};

			anim.Play();

			OnHiding();
		}

		public void HideWithAction(string action)
		{
			Hide(false, new object[] { action });
		}

		public virtual void OnShowing(object[] inData)
		{

		}

        public virtual void OnShowing(UnityAction unityAction)
        {
            
        }

        public virtual void OnHiding()
		{
			PopupManager.instance.OnPopupHiding(this);
		}

		#endregion

		#region Private Methods

		private void DoFadeAnim()
		{
			// Start the popup show animations
			UIAnimation anim = null;

			anim = UIAnimation.Alpha(gameObject, 0f, 1f, animDuration);
			anim.startOnFirstFrame = true;
			anim.OnAnimationFinished = null;
			anim.Play();
		}

		private void DoZoomAnim()
		{
			// Start the popup show animations
			UIAnimation anim = null;

			anim = UIAnimation.Alpha(gameObject, 0f, 1f, animDuration);
			anim.style = UIAnimation.Style.EaseOut;
			anim.startOnFirstFrame = true;
			anim.OnAnimationFinished = null;
			anim.Play();

			anim					= UIAnimation.ScaleX(animContainer, 0f, 1f, animDuration);
			anim.style				= UIAnimation.Style.Custom;
			anim.animationCurve		= animCurve;
			anim.startOnFirstFrame	= true;
			anim.Play();

			anim					= UIAnimation.ScaleY(animContainer, 0f, 1f, animDuration);
			anim.style				= UIAnimation.Style.Custom;
			anim.animationCurve		= animCurve;
			anim.startOnFirstFrame	= true;
			anim.Play();
		}

		#endregion
	}
}
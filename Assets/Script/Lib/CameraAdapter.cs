using UnityEngine;

namespace MTUnity
{
	/// <summary>
	/// Camera adapter: Make camera fit to screen resolution.
	/// </summary>
	public class CameraAdapter : MonoBehaviour
	{

		/// <value> Unity2D中每个单位对应的像素 </value>
		public float pixelsPerUnit = 100.0f;
		/// <value> 设计宽度 </value>
		public float designWidth;
		/// <value> 设计高度 </value>
		public float designHeight;
		[HideInInspector]
		/// <value> 摄像机区域大小 </value>
		public Rect cameraSize;

		private Camera _mainCamera;

		void Awake ()
		{
			if (designWidth == 0 || designHeight == 0) {//未设置设计尺寸
				Debug.LogWarning ("CameraFitScreenResolution:: The design size not initialized, disable script.");
				this.enabled = false;
				return;
			}
			_mainCamera = GetComponent<Camera> ();
			if (_mainCamera == null) {//挂载此脚本的对象必须是摄像机
				Debug.LogWarning ("CameraFitScreenResolution:: This gameObject isn't a Camera, disable script.");
				this.enabled = false;
			}
		}

		void OnEnable ()
		{
			float w, h;

			float screenWidth = Screen.width * 1.0f;
			float screenHeight = Screen.height * 1.0f;
//			Debug.Log ("CameraFitScreenResolution:: screenWidth=" + screenWidth + ", screenHeight=" + screenHeight);

			float aspectRatio = screenWidth / screenHeight;
			if (screenWidth / designWidth >= screenHeight / designHeight) {
				w = designWidth / pixelsPerUnit;
				h = (designWidth / aspectRatio) / pixelsPerUnit;

			} else {
				w = (designHeight * aspectRatio) / pixelsPerUnit;
				h = designHeight / pixelsPerUnit;
			}
			_mainCamera.orthographicSize = 0.5f * h;
			cameraSize = new Rect (0, 0, w, h);
//			Debug.Log ("CameraFitScreenResolution:: orthographicSize=" + _mainCamera.orthographicSize);
			Debug.Log ("CameraFitScreenResolution:: camera size: w=" + w + ", h=" + h);
		}

	}
}


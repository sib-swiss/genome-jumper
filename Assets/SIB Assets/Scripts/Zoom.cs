using UnityEngine;
using System.Collections;
using MoreMountains.Tools;

namespace MoreMountains.CorgiEngine
{
	public class Zoom : MonoBehaviour {

		public float targetOrtho = 4f;
		public float smoothSpeed = 2f;
		protected CorgiController ctrl;
		protected float orthoOrigin;

		public virtual void Start() {
			orthoOrigin = Camera.main.orthographicSize;
			ctrl = GetComponent<CorgiController>();
		}

		public virtual void Update () {

			if (ctrl.jumpSoon) {
				Camera.main.orthographicSize = Mathf.MoveTowards (Camera.main.orthographicSize, targetOrtho, smoothSpeed * Time.deltaTime);
			}
			if (!ctrl.jumpSoon && ctrl.isGrounded) {
				Camera.main.orthographicSize = Mathf.MoveTowards (Camera.main.orthographicSize, orthoOrigin, smoothSpeed * Time.deltaTime);
			
			}
		}
	}
}
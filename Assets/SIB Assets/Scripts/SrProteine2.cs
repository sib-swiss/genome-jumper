using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.CorgiEngine
{
	public class SrProteine2 : MonoBehaviour {

		public virtual void OnTriggerEnter2D(Collider2D col){
			CorgiController controller = col.GetComponent<CorgiController>();

			if (controller==null)
				return;
			
			controller.jumpSoon = true;
		}

		public virtual void OnTriggerExit2D(Collider2D col){
			CorgiController controller = col.GetComponent<CorgiController>();

			if (controller==null)
				return;

			controller.jumpSoon = false;
		}

	}
}


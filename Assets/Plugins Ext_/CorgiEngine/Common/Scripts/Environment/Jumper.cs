using UnityEngine;
using System.Collections;

namespace MoreMountains.CorgiEngine
{	

	public class Jumper : MonoBehaviour 
	{
		public float JumpPlatformBoost = 1.1f;
        public int damagesToDeal;
        public int invincibilityDuration;

        public AudioClip spark;
        public GameObject sparks;

        public virtual void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.tag == "Player")
            {
                sparks.GetComponent<ParticleSystem>().Play();

                SoundManager.Instance.PlaySound(spark, transform.position);
            }
        }
			
		public virtual void OnTriggerStay2D(Collider2D collider)
		{
			CorgiController controller=collider.GetComponent<CorgiController>();
			if (controller==null)
				return;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().Damage(damagesToDeal, gameObject, 0f, invincibilityDuration);

            if(GameObject.FindGameObjectWithTag("Player").transform.position.x <= GetComponent<Transform>().position.x)
            {
                controller.SetVerticalForce(Mathf.Sqrt(2f * JumpPlatformBoost * -controller.Parameters.Gravity));
                controller.SetHorizontalForce(-Mathf.Sqrt(2f * JumpPlatformBoost * -controller.Parameters.Gravity));
            }

            if (GameObject.FindGameObjectWithTag("Player").transform.position.x > GetComponent<Transform>().position.x)
            {
                controller.SetVerticalForce(Mathf.Sqrt(2f * JumpPlatformBoost * -controller.Parameters.Gravity));
                controller.SetHorizontalForce(Mathf.Sqrt(2f * JumpPlatformBoost * -controller.Parameters.Gravity));
            }

        }
	}
}
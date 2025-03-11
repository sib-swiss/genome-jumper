using UnityEngine;
using System.Collections;

namespace MoreMountains.CorgiEngine
{	

	public class EnemyCollideEffect : MonoBehaviour 
	{
		public float knockBackForce = 1.2f;
        public int damagesToDeal;
        public float invincibilityDuration;
        private bool isInvicible = false;

        public AudioClip sparkSound;
        public GameObject LittleSparksGameObject;

		public bool disableTrigger2D=false;

        public virtual void OnTriggerEnter2D(Collider2D collider)
        {
			if (disableTrigger2D)
				return;
			if (collider.gameObject.tag.Equals("Player") && isInvicible == false)
            {
                if(this.gameObject.name != "horizontalEnemyPrefab") {
                    LittleSparksGameObject.GetComponent<ParticleSystem>().Play();
                    AudioSource.PlayClipAtPoint(sparkSound, transform.position, 0.6f);
                }
            }
        }
			
		public virtual void OnTriggerStay2D(Collider2D collider)
		{
			if (disableTrigger2D)
				return;
			CorgiController controller=collider.GetComponent<CorgiController>();
			if (controller==null)
				return;

            var healthScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
            healthScript.Damage(damagesToDeal, gameObject, 0f, invincibilityDuration, Vector3.zero);
            GameObject.FindGameObjectWithTag("hearts").GetComponent<Life>().UpdateLife(healthScript.CurrentHealth);


            if (!isInvicible)
            {
                if (GameObject.FindGameObjectWithTag("Player").transform.position.x <= GetComponent<Transform>().position.x)
                {
                    controller.SetVerticalForce(Mathf.Sqrt(2f * knockBackForce * -controller.Parameters.Gravity));
                    controller.SetHorizontalForce(-Mathf.Sqrt(2f * knockBackForce * -controller.Parameters.Gravity));
                    isInvicible = true;
                }

                if (GameObject.FindGameObjectWithTag("Player").transform.position.x > GetComponent<Transform>().position.x)
                {
                    controller.SetVerticalForce(Mathf.Sqrt(2f * knockBackForce * -controller.Parameters.Gravity));
                    controller.SetHorizontalForce(Mathf.Sqrt(2f * knockBackForce * -controller.Parameters.Gravity));
                    isInvicible = true;
                }
            }
            else
            {
                if (GameObject.FindGameObjectWithTag("Player").transform.position.x <= GetComponent<Transform>().position.x)
                {
                    controller.SetVerticalForce(Mathf.Sqrt(2f * knockBackForce/5 * -controller.Parameters.Gravity));
                    controller.SetHorizontalForce(-Mathf.Sqrt(2f * knockBackForce/5 * -controller.Parameters.Gravity));
                }

                if (GameObject.FindGameObjectWithTag("Player").transform.position.x > GetComponent<Transform>().position.x)
                {
                    controller.SetVerticalForce(Mathf.Sqrt(2f * knockBackForce/5 * -controller.Parameters.Gravity));
                    controller.SetHorizontalForce(Mathf.Sqrt(2f * knockBackForce/5 * -controller.Parameters.Gravity));
                }
				StartCoroutine(BumpDisabled(invincibilityDuration));
            }


            if (this.gameObject.name == "horizontalEnemyPrefab")
            {
                Destroy(this.gameObject.transform.parent.gameObject);
            }
        }

        public virtual IEnumerator BumpDisabled(float delay)
        {
			if (!disableTrigger2D) {
				yield return new WaitForSeconds(delay);
				isInvicible = false;
			}
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mr1;
using MoreMountains.Tools;

namespace MoreMountains.CorgiEngine
{

    public class SrProteine : MonoBehaviour
    {

        private GameObject Player;
        public bool PlayerAllowedToJump = false;
        public InputManager inputManager;

        public void Start() {
            inputManager = GameObject.Find("UICamera").GetComponent<InputManager>();
        }

		public void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.gameObject.tag == "Player") {
				AudioClip jump = (AudioClip)Resources.Load ("Sounds/srjump");
				AudioSource asrc = gameObject.AddComponent<AudioSource> ();
				asrc.transform.position = transform.position;
				asrc.PlayOneShot (jump);
			}
		}

        public void OnTriggerStay2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "Player")
            {
                Player = collision.gameObject;
                Player.GetComponent<CharacterJump>().AbilityPermitted = false;
                if (inputManager.JumpButton.State.CurrentState == MMInput.ButtonStates.ButtonPressed && PlayerAllowedToJump)
                {
                    Player.transform.FollowPath("SRPath", 0f, Mr1.FollowType.Once);
                    if (Player.transform.position.x < transform.parent.GetChild(0).GetComponent<WaypointManager>().pathList[0].points[1].x && Player.transform.position.x > transform.parent.GetChild(0).GetComponent<WaypointManager>().pathList[0].points[0].x)
                    {
                        Player.transform.Translate(new Vector3(Mathf.Cos(0.785398f) * Time.deltaTime * 4.25f, 0, 0));
                        Player.transform.Translate(new Vector3(0, Mathf.Sin(0.785398f) * Time.deltaTime * 2.25f, 0));
                        Player.GetComponent<CorgiController>().Parameters.Gravity = 0;
                    }
                    else if (Player.transform.position.x > transform.parent.GetChild(0).GetComponent<WaypointManager>().pathList[0].points[1].x && Player.transform.position.x < transform.parent.GetChild(0).GetComponent<WaypointManager>().pathList[0].points[2].x)
                    {
                        Player.transform.Translate(new Vector3(Mathf.Cos(0.785398f) * Time.deltaTime * 4.25f, 0, 0));
                        Player.transform.Translate(new Vector3(0, -Mathf.Sin(0.785398f) * Time.deltaTime * 2.25f, 0));
                        Player.GetComponent<CorgiController>().Parameters.Gravity = 0;
                    }
                    else if ((Player.transform.position.x >= transform.parent.GetChild(0).GetComponent<WaypointManager>().pathList[0].points[2].x - 0.5f) && (Player.transform.position.x <= transform.parent.GetChild(0).GetComponent<WaypointManager>().pathList[0].points[2].x))
                    {
                        Player.GetComponent<CorgiController>().Parameters.Gravity = -30;
                        Player.transform.StopFollowing();
                    }
                }

            }
        }
        public void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "Player")
            {
                Debug.Log("<color=red> SrProteine.cs : GameObject.Find('UICamera').GetComponent<InputManager>().isPlayerJumping</color>");
                //GameObject.Find("UICamera").GetComponent<InputManager>().isPlayerJumping = false;
                PlayerAllowedToJump = false;
                Player.GetComponent<CharacterJump>().AbilityPermitted = true;
                Player.transform.StopFollowing();
                Player.GetComponent<CorgiController>().Parameters.Gravity = -30;
                gameObject.transform.parent.gameObject.SetActive(false);
            }
            
        }
        /*
        public float JumpPlatformBoost;

        public virtual void OnTriggerEnter2D(Collider2D collider)
        {
            CorgiController controller = collider.GetComponent<CorgiController>();
            if(controller == null) { return; }

            controller.SetVerticalForce(Mathf.Sqrt(0.5f * JumpPlatformBoost * -controller.Parameters.Gravity));
            controller.SetHorizontalForce(Mathf.Sqrt(2f * JumpPlatformBoost * -controller.Parameters.Gravity));
        }*/

        //private bool hasLaunched = false;

        /// the force of the jump induced by the platform
        /*private float radius = Mathf.Sqrt(2) / 2;
        public float verticalForce = 5;
        public float horizontalForce = 7;
        public float bumpForce;
        public AudioClip srProteineSfx;

        private int incWaiting = 0;
        private int waitingMax = 4;
        */
        //private CorgiController controller;

        /// <summary>
        /// Triggered when a CorgiController touches the platform, applys a vertical force to it, propulsing it in the air.
        /// </summary>
        /// <param name="controller">The corgi controller that collides with the platform.</param>

        /*public void OnTriggerEnter2D(Collider2D col){

            //if (hasLaunched) {
            //	return;
            //}

            controller=col.GetComponent<CorgiController>();
            if (controller==null)
                return;

            controller.SetVerticalForce(Mathf.Sqrt(2f * verticalForce * - controller.Parameters.Gravity));
            controller.SetHorizontalForce(Mathf.Sqrt(2f * horizontalForce * - controller.Parameters.Gravity));

            //if (!controller.isGrounded) {
            //	return;
            //}

            SoundManager.Instance.PlaySound(srProteineSfx,transform.position);
            //controller.SetHorizontalForce(horizontalForce);
            //controller.SetVerticalForce(Mathf.Sqrt( 2f * verticalForce * -controller.Parameters.Gravity ));
            //controller.SetForce(new Vector2(horizontalForce,verticalForce));

            //controller.AddHorizontalForce(horizontalForce);

            //Rigidbody2D player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Rigidbody2D> ();
            //player.bodyType = RigidbodyType2D.Dynamic;
            //player.AddForce (new Vector2 (horizontalForce, verticalForce),ForceMode2D.Impulse);

            //hasLaunched = true;



        }

        public void OnTriggerExit2D(Collider2D col)
        {

            //if (hasLaunched) {
            //	return;
            //}

            controller = col.GetComponent<CorgiController>();
            if (controller == null)
                return;

            controller.SetVerticalForce(Mathf.Sqrt(2f * verticalForce * -controller.Parameters.Gravity));
            controller.SetHorizontalForce(Mathf.Sqrt(2f * horizontalForce * -controller.Parameters.Gravity));

            //if (!controller.isGrounded) {
            //	return;
            //}

            SoundManager.Instance.PlaySound(srProteineSfx, transform.position);
            //controller.SetHorizontalForce(horizontalForce);
            //controller.SetVerticalForce(Mathf.Sqrt( 2f * verticalForce * -controller.Parameters.Gravity ));
            //controller.SetForce(new Vector2(horizontalForce,verticalForce));

            //controller.AddHorizontalForce(horizontalForce);

            //Rigidbody2D player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Rigidbody2D> ();
            //player.bodyType = RigidbodyType2D.Dynamic;
            //player.AddForce (new Vector2 (horizontalForce, verticalForce),ForceMode2D.Impulse);

            //hasLaunched = true;



        }

        //public void FixedUpdate(){
        //	if (controller != null && incWaiting > -1 && hasLaunched) {
        //		if (incWaiting < waitingMax) {
        //			incWaiting++;
        //		} else if(controller.isGrounded) {
        //			Rigidbody2D player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Rigidbody2D> ();
        //			player.bodyType = RigidbodyType2D.Kinematic;
        //			player.velocity = Vector2.zero;
        //			player.angularVelocity = 0;
        //			//Debug.Log ("Adding horizontal force");
        //			incWaiting = -1;
        //		}
        //	}
        //}*/
    }
}

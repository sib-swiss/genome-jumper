using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using UnityEngine;

public class Coins : MonoBehaviour
{
	private Life _life;
	public GameObject particles;
	public AudioClip takeCoinSound;
    public int CoinsPoints;
	private SpriteRenderer sprite;
    private ScoreDisplay Score;
	private float timeLeft = -1;

	public void Start()
	{
		sprite = GetComponent<SpriteRenderer>();
        Score = GameObject.Find("PointsText").GetComponent<ScoreDisplay>();
	}

	private void OnTriggerEnter2D(Collider2D col){
        if (col.tag.Equals("Player")) {
            _life = GameObject.FindGameObjectWithTag("hearts").GetComponent<Life>();
	        AudioSource.PlayClipAtPoint(takeCoinSound, transform.position, 0.6f);
            //SoundManager.Instance.PlaySound(takeCoinSound, transform.position);

            Instantiate(particles, transform.position, transform.rotation);

            CorgiController controller = col.GetComponent<CorgiController>();
            if (controller == null)
                return;

            Health playerLife = controller.GetComponent<Health>();
            if (playerLife.CurrentHealth != 300) {

                playerLife.CurrentHealth += 25;
                _life.UpdateLife(playerLife.CurrentHealth);
            }
            Score.score += CoinsPoints;
            Destroy(transform.gameObject);

        }
    }
	
}

using MoreMountains.CorgiEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCircleCollider : MonoBehaviour {

    private GameObject player;
    private Health health;
    private bool isKnockbackStateAlreadyTriggered = false;
    private bool velocityApplied = false;
    public bool knockbackStillRunning = false;
    private float knockbackSmoothness = 0;
    Rigidbody2D playerRb;
    protected Vector2 velocity;
    protected Vector2 defaultVelocity = new Vector2(0, 0);
    protected Vector2 lastPosition;
    protected Vector2 knockBackForce;
    public int timer = 0;

    [Header("Knockback force values")]
    public float forceX = 5f;
    public float forceY = 5f;
    public float knockbackLerp = 0;
    public float velocityUpdate = 0f;

    [Header("Damages inflicted by the enemy")]
    public int damagesToDeal;
    public float invincibilityDuration = 0.5f;
    public float disabledMovementDuration = 0f;

    public enum DirectionThatCanBump { All, AllButBotLeft, AllButTopLeft, AllButTopRight }
    public DirectionThatCanBump BumpDirection = DirectionThatCanBump.All;
    private bool playerKnockedBackBotLeft = false;
    private bool playerKnockedBackTopLeft = false;
    private bool playerKnockedBackTopRight = false;
    private bool PlayerKnockBackFromTop = false;

    public bool playerInTheAir = false;


    public AudioClip spark;
    public GameObject sparks;


    void OnTriggerEnter2D(Collider2D collider)
    {
        // We check if we don't collider with anything else than Player (Usefull when multiple objects with this script attached are close, prevent from triggering every frame a useless function)
        if (collider.gameObject.tag == "Player")
        {
            sparks.GetComponent<ParticleSystem>().Play();
		
            SoundManager.Instance.PlaySound(spark,transform.position);

            /*
            // Get the position of the object representating the enemy.
            float enemyPosX = GetComponent<Transform>().position.x;
            float enemyPosY = GetComponent<Transform>().position.y + GetComponent<CircleCollider2D>().offset.y;
            //Debug.Log("Enemy Position : (" + enemyPosX + "," + enemyPosY + ")");

            // Get the radius of the collider, in order to get the cardinal edges of the circle collider
            float colliderRadius = GetComponent<CircleCollider2D>().radius;
            var colliderBounds = GetComponent<CircleCollider2D>().bounds;
            //Debug.Log("Circle Collider Radius : " + colliderRadius);
            //Debug.Log(colliderBounds);

            // Get the minX, maxX, minY, maxY of the circle (aka the cardinal edges of the circle collider)
            //OLD CARDS POINTS
            float minX = (enemyPosX - colliderRadius);
            float maxX = (enemyPosX + colliderRadius);
            float minY = (enemyPosY - colliderRadius)/2;
            float maxY = (enemyPosY + colliderRadius)/2;
            float centerX = enemyPosX;
            float centerY = enemyPosY/2;

            float fixedPlayerColliderX = collider.transform.position.x;
            float fixedPlayerColliderY = (collider.transform.position.y - collider.transform.localScale.y);

            //Debug.Log("(" + fixedPlayerColliderX + "," + fixedPlayerColliderY + ")");

            // If we hit the LowerLeft of the circle
            if ((fixedPlayerColliderX >= minX && fixedPlayerColliderX < centerX) && (fixedPlayerColliderY < centerY))
            {

                if(isKnockbackStateAlreadyTriggered == false && velocityApplied == false &&(BumpDirection == DirectionThatCanBump.All || BumpDirection == DirectionThatCanBump.AllButTopLeft || BumpDirection == DirectionThatCanBump.AllButTopRight))
                {
                    //Debug.Log("Lowerleft");
                    isKnockbackStateAlreadyTriggered = true;
                    knockbackStillRunning = true;
                    knockBackForce = new Vector2(-2 * forceX, 0 * forceY);
                    playerRb.bodyType = RigidbodyType2D.Dynamic;
                    playerRb.AddForce(knockBackForce, ForceMode2D.Impulse);
                    playerKnockedBackBotLeft = true;
                    velocityApplied = true;
                    player.GetComponent<Health>().Damage(damagesToDeal, gameObject, 0f, invincibilityDuration);
                    // ResetCharacterState();
                }
            }

            if ((fixedPlayerColliderX >= minX && fixedPlayerColliderX < centerX) && (centerY < fixedPlayerColliderY && fixedPlayerColliderY <= maxY))
            {
                if(isKnockbackStateAlreadyTriggered == false && velocityApplied == false && (BumpDirection == DirectionThatCanBump.All || BumpDirection == DirectionThatCanBump.AllButBotLeft || BumpDirection == DirectionThatCanBump.AllButTopRight))
                {
                    // Debug.Log("TopLeft");
                    isKnockbackStateAlreadyTriggered = true;
                    knockbackStillRunning = true;
                    knockBackForce = new Vector2(-2 * forceX, 2 * forceY);
                    playerRb.bodyType = RigidbodyType2D.Dynamic;
                    playerRb.AddForce(knockBackForce, ForceMode2D.Impulse);
                    playerKnockedBackTopLeft = true;
                    //player.GetComponent<Rigidbody2D>().velocity = knockBackForce;
                    velocityApplied = true;
                    player.GetComponent<Health>().Damage(damagesToDeal, gameObject, 0f, invincibilityDuration);
                    //ResetCharacterState();
                }
            }

            if ((fixedPlayerColliderX > centerX && fixedPlayerColliderX <= maxX) && (centerY < fixedPlayerColliderY && fixedPlayerColliderY <= maxY))
            {
                if (isKnockbackStateAlreadyTriggered == false && velocityApplied == false && (BumpDirection == DirectionThatCanBump.All || BumpDirection == DirectionThatCanBump.AllButTopLeft || BumpDirection == DirectionThatCanBump.AllButTopLeft))
                {
                    // Debug.Log("TopRight");
                    isKnockbackStateAlreadyTriggered = true;
                    knockbackStillRunning = true;
                    knockBackForce = new Vector2(2 * forceX, 2 * forceY);
                    playerRb.bodyType = RigidbodyType2D.Dynamic;
                    playerRb.AddForce(knockBackForce, ForceMode2D.Impulse);
                    playerKnockedBackTopRight = true;
                    //player.GetComponent<Rigidbody2D>().velocity = knockBackForce;
                    velocityApplied = true;
                    player.GetComponent<Health>().Damage(damagesToDeal, gameObject, 0f, invincibilityDuration);
                    //ResetCharacterState();
                }
            }

            if((fixedPlayerColliderX == minX || fixedPlayerColliderX == maxX) && (fixedPlayerColliderY == centerY)) {
                if(isKnockbackStateAlreadyTriggered == false && velocityApplied == false)
                {
                    isKnockbackStateAlreadyTriggered = true;
                    knockbackStillRunning = true;
                    knockBackForce = new Vector2(2 * forceX, -1 * forceY);
                    playerRb.bodyType = RigidbodyType2D.Dynamic;
                    playerRb.AddForce(knockBackForce, ForceMode2D.Impulse);
                    //player.GetComponent<Rigidbody2D>().velocity = knockBackForce;
                    velocityApplied = true;
                    player.GetComponent<Health>().Damage(damagesToDeal, gameObject, 0f, invincibilityDuration);
                    //ResetCharacterState();
                }
            }

            if ((fixedPlayerColliderY >= maxY))
            {
                if (isKnockbackStateAlreadyTriggered == false && velocityApplied == false)
                {
                    Debug.Log("salut");
                    isKnockbackStateAlreadyTriggered = true;
                    knockbackStillRunning = true;
                    PlayerKnockBackFromTop = true;
                    knockBackForce = Vector3.Normalize(new Vector2(2 * forceX, 3 * forceY)) * 10f;
                    Debug.Log(playerRb.velocity);
                    playerRb.bodyType = RigidbodyType2D.Dynamic;
                    playerRb.AddForce(knockBackForce,ForceMode2D.Impulse);
                    //.GetComponent<Rigidbody2D>().velocity = knockBackForce;
                    velocityApplied = true;
                    player.GetComponent<Health>().Damage(damagesToDeal, gameObject, 0f, invincibilityDuration);
                    //ResetCharacterState();
                }
            }*/

            //playerRb.bodyType = RigidbodyType2D.Dynamic;
            //playerRb.AddForce(Vector2.left * 2f, ForceMode2D.Impulse);
            //playerRb.AddForce(Vector2.up * 2f, ForceMode2D.Impulse);

            playerInTheAir = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //playerRb.bodyType = RigidbodyType2D.Kinematic;
        //playerRb.velocity = Vector3.zero;
        playerInTheAir = false;
    }


    public virtual void CalculateVelocity()
    {
        velocity = lastPosition - (Vector2)transform.position / Time.deltaTime;
        lastPosition = player.transform.position;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    { 
        Vector3 testRay = new Vector3(player.transform.position.x + 0.01f, (player.transform.position.y - 0.1770639f) - 0.1f, player.transform.position.z);
        Debug.DrawRay(testRay, Vector3.down * 5f, Color.green);
        RaycastHit2D ray = Physics2D.Raycast(testRay, Vector3.down * 5f);

        if (playerInTheAir)
        {
            Debug.Log("air!");
            playerRb.velocity = new Vector2(-forceX, forceY);
            if (timer < 10)
            {
                timer = timer + 1;
            }
            else
            {
                playerRb.velocity = new Vector2(0, 0);
                timer = 0;
            }
        }
        else
        {
            if (timer < 10)
            {
                timer = timer + 1;
            }
            else
            {
                playerRb.velocity = new Vector2(0, 0);
                timer = 0;
            }
        }
        //else if (playerInTheAir && !player.GetComponent<CorgiController>().isGrounded)
        //{
        //    Debug.Log("air2");
        //    playerRb.velocity = new Vector2(-forceX, forceY);
        //    if (timer < 10)
        //    {
        //        Debug.Log(timer);
        //        timer = timer + 1;
        //    }
        //    else
        //    {
        //        playerRb.velocity = new Vector2(0, 0);
        //        timer = 0;
        //    }
        //}
        
    }

    /*public virtual void ResetKnockbackState() {
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    public virtual void RestoreAbilityToWalk()
    {
        player.GetComponent<CharacterHorizontalMovement>().AbilityPermitted = true;
    }
    public virtual void SetVelocityToDefault()
    {
        velocityApplied = false;
        isKnockbackStateAlreadyTriggered = false;
        
    }
    public void ResetCharacterState()
    {
        //Invoke("ResetKnockbackState", knockbackLerp);
        Invoke("RestoreAbilityToWalk", disabledMovementDuration);
        Invoke("SetVelocityToDefault", velocityUpdate);
    }

    Vector3 SmoothApproach(Vector3 pastPosition, Vector3 pastTargetPosition, Vector3 targetPosition, float speed)
    {
        float t = Time.deltaTime * speed;
        Vector3 v = (targetPosition - pastTargetPosition) / t;
        Vector3 f = pastPosition - pastTargetPosition + v;
        return targetPosition - v + f * Mathf.Exp(-t);
    }*/

}

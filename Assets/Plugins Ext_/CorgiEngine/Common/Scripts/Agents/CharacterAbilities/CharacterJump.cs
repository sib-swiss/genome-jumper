﻿using UnityEngine;
using System.Collections;
using MoreMountains.Tools;

namespace MoreMountains.CorgiEngine
{	
	/// <summary>
	/// Add this class to a character and it'll be able to jump
	/// Animator parameters : Jumping (bool), DoubleJumping (bool), HitTheGround (bool)
	/// </summary>
	[AddComponentMenu("Corgi Engine/Character/Abilities/Character Jump")] 
	public class CharacterJump : CharacterAbility
	{	
		/// This method is only used to display a helpbox text at the beginning of the ability's inspector
		public override string HelpBoxText() { return "This component handles jumps. Here you can define the jump height, whether the jump is proportional to the press length or not, the minimum air time (how long a character should stay in the air before being able to go down if the player has released the jump button), a jump window duration (the time during which, after falling off a cliff, a jump is still possible), jump restrictions, how many jumps the character can perform without touching the ground again, and how long collisions should be disabled when exiting 1-way platforms or moving platforms."; }

		/// the possible jump restrictions
		public enum JumpBehavior
		{
			CanJumpOnGround,
			CanJumpAnywhere,
			CantJump,
			CanJumpAnywhereAnyNumberOfTimes
		}

		[Header("Jump Behaviour")]
		/// defines how high the character can jump
		public float JumpHeight = 3.025f;
		/// the maximum number of jumps allowed (0 : no jump, 1 : normal jump, 2 : double jump, etc...)
		public int NumberOfJumps=3;
		/// basic rules for jumps : where can the player jump ?
		public JumpBehavior JumpRestrictions = JumpBehavior.CanJumpAnywhere;
		/// a timeframe during which, after leaving the ground, the character can still trigger a jump
		public float JumpTimeWindow = 0f;

		[Header("Proportional jumps")]
		/// if true, the jump duration/height will be proportional to the duration of the button's press
		public bool JumpIsProportionalToThePressTime=true;
		/// the minimum time in the air allowed when jumping - this is used for pressure controlled jumps
		public float JumpMinimumAirTime = 0.1f;

		[Header("Collisions")]
		// duration (in seconds) we need to disable collisions when jumping down a 1 way platform
		public float OneWayPlatformsJumpCollisionOffDuration=0.3f;
		// duration (in seconds) we need to disable collisions when jumping off a moving platform
		public float MovingPlatformsJumpCollisionOffDuration=0.05f;

		/// the number of jumps left to the character
		public int NumberOfJumpsLeft { get; protected set; }

	    protected float _jumpButtonPressTime = 0;
	    protected bool _jumpButtonPressed = false;
	    protected bool _jumpButtonReleased = false;
	    protected bool _doubleJumping = false;
		protected CharacterHorizontalMovement _characterHorizontalMovement = null;
	    protected CharacterWalljump _characterWallJump = null;
		protected CharacterCrouch _characterCrouch = null;
		protected CharacterButtonActivation _characterButtonActivation = null;

		protected float _lastTimeGrounded = 0f;

		/// Evaluates the jump restrictions
		public bool JumpAuthorized 
		{ 
			get 
			{ 
				if (EvaluateJumpTimeWindow())
				{
					return true;
				}

				if ( (JumpRestrictions == JumpBehavior.CanJumpAnywhere) ||  (JumpRestrictions == JumpBehavior.CanJumpAnywhereAnyNumberOfTimes) )
				{
					return true;
				}					
				
				if (JumpRestrictions == JumpBehavior.CanJumpOnGround)
				{
					if (_controller.State.IsGrounded)
					{
						return true;
					}
					else
					{
						if (NumberOfJumpsLeft < NumberOfJumps)
						{
							return true;
						}
					}
				}					
				
				return false; 
			}
		}

		/// <summary>
		/// On Start() we reset our number of jumps
		/// </summary>
		protected override void Initialization()
	    {
			base.Initialization();
			ResetNumberOfJumps();
			_characterWallJump = GetComponent<CharacterWalljump>();
			_characterCrouch = GetComponent<CharacterCrouch>();
			_characterButtonActivation = GetComponent<CharacterButtonActivation>();
			_characterHorizontalMovement = GetComponent<CharacterHorizontalMovement> ();
		}	

		/// <summary>
		/// At the beginning of each cycle we check if we've just pressed or released the jump button
		/// </summary>
		protected override void HandleInput()
		{
			if (_inputManager.JumpButton.State.CurrentState == MMInput.ButtonStates.ButtonDown)
			{
				JumpStart();
			}
			if (_inputManager.JumpButton.State.CurrentState == MMInput.ButtonStates.ButtonUp)
			{
				JumpStop();
			}
		}	

		/// <summary>
		/// Every frame we perform a number of checks related to jump
		/// </summary>
		public override void ProcessAbility()
	    {
			base.ProcessAbility();
			if (!AbilityPermitted) { return; }

			// if we just got grounded, we reset our number of jumps
			if (_controller.State.JustGotGrounded)
			{
				NumberOfJumpsLeft=NumberOfJumps;	
				_doubleJumping=false;
			}

			// we store the last timestamp at which the character was grounded
			if (_controller.State.IsGrounded)
			{
				_lastTimeGrounded = Time.time;
			}

            // If the user releases the jump button and the character is jumping up and enough time since the initial jump has passed, then we make it stop jumping by applying a force down.
            if ( (_jumpButtonPressTime!=0) 
			    && (Time.time - _jumpButtonPressTime >= JumpMinimumAirTime) 
			    && (_controller.Speed.y > Mathf.Sqrt(Mathf.Abs(_controller.Parameters.Gravity))) 
			    && (_jumpButtonReleased)
				&& ( !_jumpButtonPressed
					|| (_movement.CurrentState == CharacterStates.MovementStates.Jetpacking)))
			{
				_jumpButtonReleased=false;	
				if (JumpIsProportionalToThePressTime)	
				{	
					_jumpButtonPressTime=0;
					_controller.SetVerticalForce(0);
				}
			}
	    }

		/// <summary>
		/// Determines if whether or not a Character is still in its Jump Window (the delay during which, after falling off a cliff, a jump is still possible without requiring multiple jumps)
		/// </summary>
		/// <returns><c>true</c>, if jump time window was evaluated, <c>false</c> otherwise.</returns>
		protected virtual bool EvaluateJumpTimeWindow()
		{
			if (_movement.CurrentState == CharacterStates.MovementStates.Jumping 
				|| _movement.CurrentState == CharacterStates.MovementStates.DoubleJumping
				|| _movement.CurrentState == CharacterStates.MovementStates.WallJumping)
			{
				return false;
			}

			if (Time.time - _lastTimeGrounded <= JumpTimeWindow)
			{
				return true;
			}
			else 
			{
				return false;
			}
		}

		/// <summary>
		/// Evaluates the jump conditions to determine whether or not a jump can occur
		/// </summary>
		/// <returns><c>true</c>, if jump conditions was evaluated, <c>false</c> otherwise.</returns>
		protected virtual bool EvaluateJumpConditions()
		{
			if ( !AbilityPermitted  // if the ability is not permitted
				|| !JumpAuthorized // if jumps are not authorized right now
				|| ((_condition.CurrentState != CharacterStates.CharacterConditions.Normal) // or if we're not in the normal stance
					&& (_condition.CurrentState != CharacterStates.CharacterConditions.ControlledMovement))
				|| (_movement.CurrentState == CharacterStates.MovementStates.Jetpacking) // or if we're jetpacking
				|| (_movement.CurrentState == CharacterStates.MovementStates.Dashing) // or if we're dashing
				|| ((_movement.CurrentState == CharacterStates.MovementStates.WallClinging) && (_characterWallJump != null)) // or if we're wallclinging and can walljump
				|| _controller.State.IsCollidingAbove) // or if we're colliding with the ceiling
			{
				return false;
			}

			// if we're in a button activated zone and can interact with it
			if (_characterButtonActivation != null)
			{
				if (_characterButtonActivation.AbilityPermitted
					&& _characterButtonActivation.InButtonActivatedZone)
				{
					return false;
				}
			}

			// if we're crouching and don't have enough space to stand we do nothing and exit
			if ((_movement.CurrentState == CharacterStates.MovementStates.Crouching) || (_movement.CurrentState == CharacterStates.MovementStates.Crawling))
			{				
				if (_characterCrouch != null)
				{
					if (_characterCrouch.InATunnel)
					{
						return false;
					}
				}
			}

			// if we're not grounded, not on a ladder, and don't have any jumps left, we do nothing and exit
			if ((!_controller.State.IsGrounded)
				&& !EvaluateJumpTimeWindow()
				&& (_movement.CurrentState != CharacterStates.MovementStates.LadderClimbing)
				&& (JumpRestrictions != JumpBehavior.CanJumpAnywhereAnyNumberOfTimes)
				&& (NumberOfJumpsLeft <= 0))			
			{
				return false;
			}

			if (_inputManager != null)
			{
				// if the character is standing on a one way platform and is also pressing the down button,
				if (_verticalInput < -_inputManager.Threshold.y && _controller.State.IsGrounded)
				{
					if (JumpDownFromOneWayPlatform())
					{
						return false;
					}
				}

				// if the character is standing on a moving platform and not pressing the down button,
				if (_verticalInput >= -_inputManager.Threshold.y && _controller.State.IsGrounded)
				{
					JumpFromMovingPlatform();
				}
			}	

			return true;
		}

		/// <summary>
		/// Causes the character to start jumping.
		/// </summary>
		public virtual void JumpStart()
		{			
			if (!EvaluateJumpConditions())
			{
				return;
			}
			// we reset our walking speed
			if ((_movement.CurrentState == CharacterStates.MovementStates.Crawling)
				|| (_movement.CurrentState == CharacterStates.MovementStates.Crouching))
			{
				_characterHorizontalMovement.ResetHorizontalSpeed();
			}				

			// if we're still here, the jump will happen
			// we set our current state to Jumping
			_movement.ChangeState(CharacterStates.MovementStates.Jumping);

			// we trigger a character event
			MMEventManager.TriggerEvent(new MMCharacterEvent(_character, MMCharacterEventTypes.Jump));

			// we start our sounds
			PlayAbilityStartSfx();

			if (NumberOfJumpsLeft!=NumberOfJumps)
			{
				_doubleJumping=true;
			}

			// we decrease the number of jumps left
			NumberOfJumpsLeft=NumberOfJumpsLeft-1;

			// we reset our current condition and gravity
			_condition.ChangeState(CharacterStates.CharacterConditions.Normal);
			_controller.GravityActive(true);
			_controller.CollisionsOn ();

			// we set our various jump flags and counters
			SetJumpFlags();


			// we make the character jump
			_controller.SetVerticalForce(Mathf.Sqrt( 2f * JumpHeight * Mathf.Abs(_controller.Parameters.Gravity) ));	
		}

		/// <summary>
		/// Handles jumping down from a one way platform.
		/// </summary>
		protected virtual bool JumpDownFromOneWayPlatform()
		{
			if (_controller.OneWayPlatformMask.Contains(_controller.StandingOn.layer)
				|| _controller.MovingOneWayPlatformMask.Contains(_controller.StandingOn.layer))
			{
				// we make it fall down below the platform by moving it just below the platform
				_controller.transform.position=new Vector2(transform.position.x,transform.position.y-0.1f);
				// we turn the boxcollider off for a few milliseconds, so the character doesn't get stuck mid platform
				StartCoroutine(_controller.DisableCollisionsWithOneWayPlatforms(OneWayPlatformsJumpCollisionOffDuration));
				_controller.DetachFromMovingPlatform();
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Handles jumping from a moving platform.
		/// </summary>
		protected virtual void JumpFromMovingPlatform()
		{
			if ( _controller.MovingPlatformMask.Contains(_controller.StandingOn.layer)
				|| _controller.MovingOneWayPlatformMask.Contains(_controller.StandingOn.layer) )
			{
				// we turn the boxcollider off for a few milliseconds, so the character doesn't get stuck mid air
				StartCoroutine(_controller.DisableCollisionsWithMovingPlatforms(MovingPlatformsJumpCollisionOffDuration));
				_controller.DetachFromMovingPlatform();
			}
		}
		
		/// <summary>
		/// Causes the character to stop jumping.
		/// </summary>
		public virtual void JumpStop()
		{
			_jumpButtonPressed=false;
			_jumpButtonReleased=true;
		}

		/// <summary>
		/// Resets the number of jumps.
		/// </summary>
		public virtual void ResetNumberOfJumps()
		{
			NumberOfJumpsLeft = NumberOfJumps;
		}

		/// <summary>
		/// Resets jump flags
		/// </summary>
		public virtual void SetJumpFlags()
		{
			_jumpButtonPressTime=Time.time;
			_jumpButtonPressed=true;
			_jumpButtonReleased=false;
		}

		/// <summary>
		/// Sets the number of jumps left.
		/// </summary>
		/// <param name="newNumberOfJumps">New number of jumps.</param>
		public virtual void SetNumberOfJumpsLeft(int newNumberOfJumps)
		{
			NumberOfJumpsLeft = newNumberOfJumps;
		}

		/// <summary>
		/// Resets the jump button released flag.
		/// </summary>
		public virtual void ResetJumpButtonReleased()
		{
			_jumpButtonReleased=false;	
		}

		/// <summary>
		/// Adds required animator parameters to the animator parameters list if they exist
		/// </summary>
		protected override void InitializeAnimatorParameters()
		{
			RegisterAnimatorParameter ("Jumping", AnimatorControllerParameterType.Bool);
			RegisterAnimatorParameter ("DoubleJumping", AnimatorControllerParameterType.Bool);
			RegisterAnimatorParameter ("HitTheGround", AnimatorControllerParameterType.Bool);
		}

		/// <summary>
		/// At the end of each cycle, sends Jumping states to the Character's animator
		/// </summary>
		public override void UpdateAnimator()
		{
			MMAnimator.UpdateAnimatorBool(_animator,"Jumping",(_movement.CurrentState == CharacterStates.MovementStates.Jumping),_character._animatorParameters);
			MMAnimator.UpdateAnimatorBool(_animator,"DoubleJumping",_doubleJumping,_character._animatorParameters);
			MMAnimator.UpdateAnimatorBool (_animator, "HitTheGround", _controller.State.JustGotGrounded, _character._animatorParameters);
		}

		/// <summary>
		/// Resets parameters in anticipation for the Character's respawn.
		/// </summary>
		public override void Reset()
		{
			base.Reset ();
			NumberOfJumpsLeft = NumberOfJumps;
		}
	}
}

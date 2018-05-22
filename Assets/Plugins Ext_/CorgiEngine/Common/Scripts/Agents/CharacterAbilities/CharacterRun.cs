﻿using UnityEngine;
using System.Collections;
using MoreMountains.Tools;

namespace MoreMountains.CorgiEngine
{	
	/// <summary>
	/// Add this component to a character and it'll be able to run
	/// Animator parameters : Running
	/// </summary>
	[AddComponentMenu("Corgi Engine/Character/Abilities/Character Run")] 
	public class CharacterRun : CharacterAbility
	{	
		/// This method is only used to display a helpbox text at the beginning of the ability's inspector
		public override string HelpBoxText() { return "This component allows your character to change speed (defined here) when pressing the run button."; }

		[Header("Speed")]
		/// the speed of the character when it's running
		public float RunSpeed = 16f;

		/// <summary>
		/// At the beginning of each cycle, we check if we've pressed or released the run button
		/// </summary>
		protected override void HandleInput()
		{
			if (_inputManager.RunButton.State.CurrentState == MMInput.ButtonStates.ButtonDown || _inputManager.RunButton.State.CurrentState == MMInput.ButtonStates.ButtonPressed)
			{
				RunStart();
			}				
			if (_inputManager.RunButton.State.CurrentState == MMInput.ButtonStates.ButtonUp)
			{
				RunStop();
			}
		}

		public override void ProcessAbility()
		{
			base.ProcessAbility();
			HandleRunningExit();
		}

		protected virtual void HandleRunningExit()
		{
			// if we're running and not grounded, we change our state to Falling
			if (!_controller.State.IsGrounded && (_movement.CurrentState == CharacterStates.MovementStates.Running))
			{
				_movement.ChangeState(CharacterStates.MovementStates.Falling);
				StopSfx ();
			}
			// if we're not moving fast enough, we go back to idle
			if ((Mathf.Abs(_controller.Speed.x) < RunSpeed / 10) && (_movement.CurrentState == CharacterStates.MovementStates.Running))
			{
				_movement.ChangeState (CharacterStates.MovementStates.Idle);
				StopSfx ();
			}
			if (!_controller.State.IsGrounded && _abilityInProgressSfx != null)
			{
				StopSfx ();
			}
		}

		/// <summary>
		/// Causes the character to start running.
		/// </summary>
		public virtual void RunStart()
		{		
			if ( !AbilityPermitted // if the ability is not permitted
				|| (!_controller.State.IsGrounded) // or if we're not grounded
				|| (_condition.CurrentState != CharacterStates.CharacterConditions.Normal) // or if we're not in normal conditions
				|| (_movement.CurrentState != CharacterStates.MovementStates.Walking) ) // or if we're not walking
			{
				// we do nothing and exit
				return;
			}
			
			// if the player presses the run button and if we're on the ground and not crouching and we can move freely, 
			// then we change the movement speed in the controller's parameters.
			if (_characterBasicMovement != null)
			{
				_characterBasicMovement.MovementSpeed = RunSpeed;
			}

			// if we're not already running, we trigger our sounds
			if (_movement.CurrentState != CharacterStates.MovementStates.Running)
			{
				PlayAbilityStartSfx();
				PlayAbilityUsedSfx();
			}

			_movement.ChangeState(CharacterStates.MovementStates.Running);
		}
		
		/// <summary>
		/// Causes the character to stop running.
		/// </summary>
		public virtual void RunStop()
		{
			// if the run button is released, we revert back to the walking speed.
			if (_characterBasicMovement != null)
			{
				_characterBasicMovement.ResetHorizontalSpeed ();
			}
			if (_movement.CurrentState == CharacterStates.MovementStates.Running)
			{
				_movement.ChangeState(CharacterStates.MovementStates.Idle);
			}
			StopSfx ();
		}

		/// <summary>
		/// Stops all run sounds
		/// </summary>
		protected virtual void StopSfx()
		{
			StopAbilityUsedSfx();
			PlayAbilityStopSfx();
		}

		/// <summary>
		/// Adds required animator parameters to the animator parameters list if they exist
		/// </summary>
		protected override void InitializeAnimatorParameters()
		{
			RegisterAnimatorParameter ("Running", AnimatorControllerParameterType.Bool);
		}

		/// <summary>
		/// At the end of each cycle, we send our Running status to the character's animator
		/// </summary>
		public override void UpdateAnimator()
		{
			MMAnimator.UpdateAnimatorBool(_animator,"Running",(_movement.CurrentState == CharacterStates.MovementStates.Running),_character._animatorParameters);
		}
	}
}

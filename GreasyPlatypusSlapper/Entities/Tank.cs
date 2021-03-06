using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;
using FlatRedBall.Math.Geometry;
using GreasyPlatypusSlapper.DataTypes;
using Microsoft.Xna.Framework;
using GreasyPlatypusSlapper.Factories;
using Microsoft.Xna.Framework.Input;
using GreasyPlatypusSlapper.InputManagement;

namespace GreasyPlatypusSlapper.Entities
{
	public partial class Tank
	{
		I2DInput movementInput;
		I2DInput aimingInput;
		IPressableInput shootingInput;
		double lastTreadTime;
		float currentHealth;
		IPressableInput boostInput;
		double lastBoostTime;
		float activeBoostModifier;
		PlayerInput playerInput;

		public int TeamIndex { get; set; }
		public float CurrentSpeed
		{
			get
			{
				return Velocity.Length();
			}
		}
		public float CurrentHealthPercent
		{
			get
			{
				return currentHealth / MaxHealth;
			}
		}

		/// <summary>
		/// Initialization logic which is execute only one time for this Entity (unless the Entity is pooled).
		/// This method is called when the Entity is added to managers. Entities which are instantiated but not
		/// added to managers will not have this method called.
		/// </summary>
		private void CustomInitialize()
		{
			this.TurretInstance.ParentRotationChangesRotation = false;
			lastBoostTime = 0 - BoostDurationInSeconds - BoostTimeoutInSeconds; // so we don't start boosted
			currentHealth = MaxHealth;
			this.TurretInstance.ParentRotationChangesRotation = false;
		}

		public void ApplyDamage(float amount)
		{
			// only apply damage if we are healthy
			if (currentHealth > 0)
			{
				currentHealth -= amount;
				if (currentHealth <= 0)
				{
					Die();
				}
			}
		}

		public void Die()
		{
			// TODO: play effects

			Destroy();
		}

		public void LoadInput(PlayerInput playerInput)
		{
			if (playerInput == null) throw new ArgumentNullException("playerInput", "playerInput cannot be a null refernece.");
			this.playerInput = playerInput;
			this.movementInput = playerInput.MovementInput;
			this.aimingInput = playerInput.AimingInput;
			this.shootingInput = playerInput.ShootingInput;

			if (GlobalContent.FeatureFlags[FeatureFlags.EnableBoost].IsEnabled)
			{
				this.boostInput = playerInput.BoostInput;
			}
		}

		private void CustomActivity()
		{
			CalculateBoostModifier();

			ApplyMovement();
			ApplyTurretAiming();
			ShootingActivity();

			if (GlobalContent.FeatureFlags[FeatureFlags.EnableTankTreads].IsEnabled)
			{
				TankTreadActivity();
			}

			if (GlobalContent.FeatureFlags[FeatureFlags.EnableSmokeOnLowHealth].IsEnabled)
			{
				SmokeInstance.Emitting = CurrentHealthPercent < LowHealthThreshold;
			}

		}

		private void ApplyMovement()
		{
			if (GlobalContent.FeatureFlags[FeatureFlags.EnableTurnBasedMovement].IsEnabled)
			{
				ApplyTurnBasedMovement();
			}
			else
			{
				if (movementInput?.Magnitude > .2f)
				{
					var desiredDirection = movementInput.GetAngle().Value;

					var direction = Math.Sign(FlatRedBall.Math.MathFunctions.AngleToAngle(RotationZ, desiredDirection));

					var rotationSpeed = 3;
					var forwardSpeed = 100;

					this.RotationZVelocity = direction * rotationSpeed;

					this.Velocity = this.RotationMatrix.Right * forwardSpeed;
				}
				else
				{
					this.Velocity = Vector3.Zero;
					this.RotationZVelocity = 0;
				}
			}

			if (activeBoostModifier > 0)
			{
				Velocity *= activeBoostModifier;
			}
		}

		private void ApplyTurretAiming()
		{
			if (GlobalContent.FeatureFlags[FeatureFlags.EnableTurnBasedMovement].IsEnabled)
			{
				ApplyTurnBasedTurretAiming();
			}
			else
			{
				ApplyAbsoluteBasedTurretAiming();
			}
		}

		private void ApplyAbsoluteBasedTurretAiming()
		{
			const int rotationSpeed = 3;

			if (aimingInput?.Magnitude > .2f)
			{
				var desiredDirection = aimingInput.GetAngle().Value;

				var direction = Math.Sign(FlatRedBall.Math.MathFunctions.AngleToAngle(TurretInstance.RotationZ, desiredDirection));

				TurretInstance.RelativeRotationZVelocity = direction * rotationSpeed;
			}
			else
			{
				TurretInstance.RelativeRotationZVelocity = 0;
			}
		}

		private void ApplyTurnBasedTurretAiming()
		{
			System.Diagnostics.Debug.WriteLine("Turn!");

			var rotationVelocity = 0f;
			if (aimingInput?.Magnitude > .2f)
			{
				rotationVelocity = -1 * aimingInput?.X ?? rotationVelocity;
			}
			TurretInstance.RelativeRotationZVelocity = rotationVelocity;
		}

		private void ShootingActivity()
		{
			if (shootingInput?.WasJustPressed == true)
			{
				var x = TurretInstance.X;
				var y = TurretInstance.Y;
				var bullet = Factories.BulletFactory.CreateNew(x, y);
				bullet.TeamIndex = this.TeamIndex;
				bullet.RotationZ = TurretInstance.RotationZ;

				var BulletSpeed = 500;
				bullet.Launch(TurretInstance.RotationMatrix.Right * BulletSpeed);
			}
		}

		private void TankTreadActivity()
		{
			var timeSinceLastTread = TimeManager.CurrentTime - lastTreadTime;
			var distSinceLastTread = CurrentSpeed * timeSinceLastTread;

			if (distSinceLastTread >= TreadSpacing)
			{
				var tread = TreadEffectFactory.CreateNew(Position.X, Position.Y);
				tread.RotationZ = RotationZ;
				lastTreadTime = TimeManager.CurrentTime;
			}

		}

		private void ApplyTurnBasedMovement()
		{
			// copied from previous movement
			const int rotationSpeed = -3;
			const int forwardSpeed = 100;

			var forwardVelocity = 0f;
			var rotationVelocity = 0f;

			if (movementInput?.Magnitude > .2f)
			{
				forwardVelocity = forwardSpeed * movementInput.Y;
				rotationVelocity = rotationSpeed * movementInput.X;
			}

			RotationZVelocity = rotationVelocity;
			Velocity = RotationMatrix.Right * forwardVelocity;
		}

		private void CalculateBoostModifier()
		{
			if (!GlobalContent.FeatureFlags[FeatureFlags.EnableBoost].IsEnabled)
			{
				return;
			}

			var timeUntilNextBoost = lastBoostTime + BoostDurationInSeconds + BoostTimeoutInSeconds;

			// Allow a fresh boost if timeout is over
			if (boostInput?.WasJustPressed == true && timeUntilNextBoost < TimeManager.CurrentTime)
			{
				lastBoostTime = TimeManager.CurrentTime;
			}

			var timeSinceBoost = TimeManager.CurrentTime - lastBoostTime;
			var isInBoost = timeSinceBoost < BoostDurationInSeconds;
			var isInPenalty = !isInBoost &&
							   timeSinceBoost < BoostDurationInSeconds + BoostPenaltyDurationInSeconds;

			if (isInBoost)
			{
				activeBoostModifier = BoostSpeedMultiplier;
			}
			else if (isInPenalty)
			{
				activeBoostModifier = BoostPenaltySpeedMultiplier;
			}
			else
			{
				activeBoostModifier = 0;
			}
		}

		private void CustomDestroy()
		{


		}

		private static void CustomLoadStaticContent(string contentManagerName)
		{


		}
	}
}

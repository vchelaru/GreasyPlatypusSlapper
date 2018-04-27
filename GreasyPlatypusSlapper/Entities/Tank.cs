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
using Microsoft.Xna.Framework;

namespace GreasyPlatypusSlapper.Entities
{
	public partial class Tank
	{


        I2DInput movementInput;
        I2DInput aimingInput;
        IPressableInput shootingInput;

        public int TeamIndex { get; set; }

        /// <summary>
        /// Initialization logic which is execute only one time for this Entity (unless the Entity is pooled).
        /// This method is called when the Entity is added to managers. Entities which are instantiated but not
        /// added to managers will not have this method called.
        /// </summary>
		private void CustomInitialize()
		{
            this.TurretInstance.ParentRotationChangesRotation = false;
		}

        public void AssignDefaultInput()
        {
            var keyboard = InputManager.Keyboard;
            movementInput = keyboard.Get2DInput(Microsoft.Xna.Framework.Input.Keys.A,
                Microsoft.Xna.Framework.Input.Keys.D,
                Microsoft.Xna.Framework.Input.Keys.W,
                Microsoft.Xna.Framework.Input.Keys.S);

            aimingInput = keyboard.Get2DInput(Microsoft.Xna.Framework.Input.Keys.Left,
                Microsoft.Xna.Framework.Input.Keys.Right,
                Microsoft.Xna.Framework.Input.Keys.Up,
                Microsoft.Xna.Framework.Input.Keys.Down);

            shootingInput = keyboard.GetKey(Microsoft.Xna.Framework.Input.Keys.Space);
        }

        private void CustomActivity()
		{
            ApplyMovement();

            ApplyTurretAiming();

            ShootingActivity();

		}

        private void ApplyMovement()
        {
            if(movementInput?.Magnitude > .2f)
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

        private void ApplyTurretAiming()
        {
            if(aimingInput?.Magnitude > .2f)
            {
                var desiredDirection = aimingInput.GetAngle().Value;

                var direction = Math.Sign(FlatRedBall.Math.MathFunctions.AngleToAngle(TurretInstance.RotationZ, desiredDirection));

                var rotationSpeed = 3;
                TurretInstance.RelativeRotationZVelocity = direction * rotationSpeed;

            }
            else
            {
                TurretInstance.RelativeRotationZVelocity = 0;
            }
        }

        private void ShootingActivity()
        {
            if(shootingInput?.WasJustPressed == true)
            {
                var x = TurretInstance.X;
                var y = TurretInstance.Y;
                var bullet = Factories.BulletFactory.CreateNew(x, y);
                bullet.TeamIndex = this.TeamIndex;
                bullet.RotationZ = TurretInstance.RotationZ;

                var BulletSpeed = 500;
                bullet.Velocity = TurretInstance.RotationMatrix.Right * BulletSpeed;
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

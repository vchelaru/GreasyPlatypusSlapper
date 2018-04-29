using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;
using FlatRedBall.Math.Geometry;
using FlatRedBall.Localization;
using FlatRedBall.Math.Collision;
using GreasyPlatypusSlapper.Entities;
using FlatRedBall.TileCollisions;
using RedGrin;
using Microsoft.Xna.Framework.Input;

namespace GreasyPlatypusSlapper.Screens
{
	public partial class GameScreen : INetworkArena
	{
		TileShapeCollection solidCollision;
		TileShapeCollection roadCollision;
		CollisionRelationship roadVsTankRelationship;

		void CustomInitialize()
		{
			InitializeActivity();

			//this.Tank1Test.AssignDefaultInput();
			this.Tank2Test.TeamIndex = 1;
			CreateTanksAndAssignInput();

			this.CameraEntityInstance.ObjectsWatching.AddRange(this.TankList);
            this.CameraEntityInstance.CurrentLevel = TestLevel;
			
		}

		private void InitializeActivity()
		{
			InitializeCollision();

		}

		private void InitializeCollision()
		{
			solidCollision = TestLevel.Collisions.FirstOrDefault(item => item.Name == "Tiles");
			roadCollision = TestLevel.Collisions.FirstOrDefault(item => item.Name == "RoadTiles");
			foreach (var collision in TestLevel.Collisions)
			{
				collision.Visible = true;
			}
			var relationship = CollisionManager.Self.CreateTileRelationship(TankList, solidCollision);
			relationship.SetMoveCollision(0, 1);

			var bulletVsTank = CollisionManager.Self.CreateRelationship(TankList, BulletList);
			bulletVsTank.CollisionOccurred = HandleBulletVsTankCollision;

			roadVsTankRelationship = CollisionManager.Self.CreateTileRelationship(TankList, roadCollision);
			roadVsTankRelationship.IsActive = false;
		}

		private void HandleBulletVsTankCollision(Tank tank, Bullet bullet)
		{
			if (bullet.TeamIndex != tank.TeamIndex)
			{
				tank.ApplyDamage(bullet.Damage);
				bullet.Destroy();
			}
		}

		void CustomActivity(bool firstTimeCalled)
		{
#if DEBUG
			RestartActivity();

#endif

			CustomCollisionActivity();

		}

		private void CustomCollisionActivity()
		{
			var isBoosting = roadVsTankRelationship.DoCollisions();

			FlatRedBall.Debugging.Debugger.Write(isBoosting);
		}

		private void RestartActivity()
		{
			if (InputManager.Keyboard.KeyPushed(Microsoft.Xna.Framework.Input.Keys.R))
			{
				this.RestartScreen(true);
			}
		}

		void CustomDestroy()
		{


		}

		void CreateTanksAndAssignInput()
		{
			//Test code for a single input. 
			I2DInput movementInput;
			I2DInput aimingInput;
			IPressableInput shootingInput;
			IPressableInput boostInput; 

			if (InputManager.NumberOfConnectedGamePads > 0)
			{
				var gamePad = InputManager.Xbox360GamePads[0];//InputManager.Xbox360GamePads[i];
				movementInput = gamePad.LeftStick;
				aimingInput = gamePad.RightStick;
				shootingInput = gamePad.RightTrigger;
				boostInput = gamePad.LeftTrigger; 
			}
			else
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
				boostInput = keyboard.GetKey(Keys.Q);
			}
			Tank1Test.LoadInput(movementInput, aimingInput, shootingInput, boostInput);
		}

		static void CustomLoadStaticContent(string contentManagerName)
		{


		}

		// TODO: Create a NetworkController entity
		public INetworkEntity RequestCreateEntity(long ownerId, long entityId, object entityData)
		{
			throw new NotImplementedException();
		}

		// TODO: Destroy a NetworkController entity by id
		public void RequestDestroyEntity(INetworkEntity entity)
		{
			throw new NotImplementedException();
		}
	}
}

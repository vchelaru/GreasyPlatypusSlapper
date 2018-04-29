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
using GreasyPlatypusSlapper.InputManagement;
using GreasyPlatypusSlapper.Factories;
using Microsoft.Xna.Framework;

namespace GreasyPlatypusSlapper.Screens
{
	public partial class GameScreen : INetworkArena, IManagesUserInteraction
	{
		TileShapeCollection solidCollision;
		TileShapeCollection roadCollision;
		CollisionRelationship roadVsTankRelationship;
		IUserInteractionState currentUserInteractionState;
		Vector2 lastStartPosition = new Vector2(200, -200);
		int lastTeamIndex = 0; 

		void CustomInitialize()
		{
			InitializeActivity();
			LoadUserInteractionState(new UIS_PlayerSelect(this, PlayerSelectionUIInstance)); 
		}

		private void InitializeActivity()
		{
			InitializeCollision();

            this.CameraEntityInstance.CurrentLevel = TestLevel;

		}

		void CustomActivity(bool firstTimeCalled)
		{
#if DEBUG
			RestartActivity();

#endif
		
			CustomCollisionActivity();
			currentUserInteractionState.Update(); 
		}

		void CustomDestroy()
		{


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

		/// <summary>
		/// Transitions from the PlayerSelectionUI to the GameScreen proper UI and starts the fight. 
		/// </summary>
		/// <param name="playerDataList">A list of the different playerInput's representing the different players partipating in the game.</param>
		public void StartGame(List<PlayerData> playerDataList)
		{
			CreateTanksAndAssignInput(playerDataList);
			PlayerSelectionUIInstance.HideAnimation.Play();
			PlayerSelectionUIInstance.HideAnimation.EndReached += () => { LoadUserInteractionState(new UIS_Playing()); };
		}

		private void HandleBulletVsTankCollision(Tank tank, Bullet bullet)
		{
			if (bullet.TeamIndex != tank.TeamIndex)
			{
				tank.ApplyDamage(bullet.Damage);
				bullet.Destroy();
			}
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

		/// <summary>
		/// Takes a list of PlayerInput objects, creates a tank for each one, and assigns its input profile to it. 
		/// </summary>
		/// <param name="playerDataList"></param>
		void CreateTanksAndAssignInput(List<PlayerData> playerDataList)
		{
			foreach(var playerData in playerDataList)
			{
				var startPosition = GetUnusedStartPosition(); 
				var newTank = TankFactory.CreateNew(startPosition.X, startPosition.Y);
				newTank.Z = 1;
				newTank.TeamIndex = lastTeamIndex++;
				newTank.CurrentTankColorState = playerData.TankColor; 
				newTank.LoadInput(playerData.PlayerInput); 
			}

			this.CameraEntityInstance.ObjectsWatching.AddRange(this.TankList);
		}

		/// <summary>
		/// Gets a new, vacant position for a tank to spawn in. 
		/// </summary>
		/// <returns></returns>
		private Vector2 GetUnusedStartPosition()
		{
			var toReturn = lastStartPosition;
			lastStartPosition.X += 200;
			return toReturn; 
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

		/// <summary>
		/// Loads a IUserInteractionState, which will manage what input to accept at any particular time. 
		/// </summary>
		/// <param name="state"></param>
		public void LoadUserInteractionState(IUserInteractionState state)
		{
			currentUserInteractionState?.Teardown();
			currentUserInteractionState = state ?? throw new ArgumentNullException("Must pass in a non-null IUserInteractionState implementation.");
			currentUserInteractionState?.Setup(); 
		}
	}
}

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

namespace GreasyPlatypusSlapper.Screens
{
	public partial class GameScreen
	{
        TileShapeCollection solidCollision;
        TileShapeCollection roadCollision;

        CollisionRelationship roadVsTankRelationship;

		void CustomInitialize()
		{
            InitializeActivity();

            this.Tank1Test.AssignDefaultInput();
            this.Tank2Test.TeamIndex = 1;

            this.CameraEntityInstance.ObjectsWatching.AddRange(this.TankList);
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
            if(bullet.TeamIndex != tank.TeamIndex)
            {
                bullet.Destroy();
                tank.Destroy();
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

        }

        private void RestartActivity()
        {
            if(InputManager.Keyboard.KeyPushed(Microsoft.Xna.Framework.Input.Keys.R))
            {
                this.RestartScreen(true);
            }
        }

        void CustomDestroy()
		{


		}

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

	}
}

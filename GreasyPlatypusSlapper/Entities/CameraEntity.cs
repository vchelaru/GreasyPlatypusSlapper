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
using FlatRedBall.Math;
using FlatRedBall.TileGraphics;

namespace GreasyPlatypusSlapper.Entities
{
	public partial class CameraEntity
	{
        public LayeredTileMap CurrentLevel { get; set; }

        public PositionedObjectList<PositionedObject> ObjectsWatching { get; private set; } = new PositionedObjectList<PositionedObject>();

        /// <summary>
        /// Initialization logic which is execute only one time for this Entity (unless the Entity is pooled).
        /// This method is called when the Entity is added to managers. Entities which are instantiated but not
        /// added to managers will not have this method called.
        /// </summary>
		private void CustomInitialize()
		{

            Camera.Main.X = 400;
            Camera.Main.Y = -300;


        }

		private void CustomActivity()
		{
            CameraActivity();
		}

        private void CameraActivity()
        {
			if (ObjectsWatching.Count > 0)
			{
				float minX = float.PositiveInfinity;
				float maxX = float.NegativeInfinity;

				float minY = float.PositiveInfinity;
				float maxY = float.NegativeInfinity;

				foreach (var item in ObjectsWatching)
				{
					minX = System.Math.Min(minX, item.X);
					minY = System.Math.Min(minY, item.Y);

					maxX = System.Math.Max(maxX, item.X);
					maxY = System.Math.Max(maxY, item.Y);
				}

				Camera.Main.X = (minX + maxX) / 2.0f;
				Camera.Main.Y = (minY + maxY) / 2.0f;
			}
            KeepCameraInBounds();
        }

        private void KeepCameraInBounds()
        {
            var minCameraX = 0 + Camera.Main.OrthogonalWidth / 2.0f;
            var maxCameraX = CurrentLevel.Width - Camera.Main.OrthogonalWidth / 2.0f;

            Camera.Main.X = Math.Max(Camera.Main.X, minCameraX);
            Camera.Main.X = Math.Min(Camera.Main.X, maxCameraX);

            var maxCameraY = 0 - Camera.Main.OrthogonalHeight / 2.0f;
            var minCameraY = -CurrentLevel.Height + Camera.Main.OrthogonalHeight / 2.0f;
            Camera.Main.Y = Math.Min(Camera.Main.Y, maxCameraY);
            Camera.Main.Y = Math.Max(Camera.Main.Y, minCameraY);
        }

        private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }
	}
}

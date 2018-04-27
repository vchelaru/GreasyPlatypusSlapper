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

namespace GreasyPlatypusSlapper.Entities
{
	public partial class CameraEntity
	{

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
            float minX = float.PositiveInfinity;
            float maxX = float.NegativeInfinity;

            float minY = float.PositiveInfinity;
            float maxY = float.NegativeInfinity;

            foreach(var item in ObjectsWatching)
            {
                minX = System.Math.Min(minX, item.X);
                minY = System.Math.Min(minY, item.Y);

                maxX = System.Math.Max(maxX, item.X);
                maxY = System.Math.Max(maxY, item.Y);
            }

            Camera.Main.X = (minX + maxX) / 2.0f;
            Camera.Main.Y = (minY + maxY) / 2.0f;
        }

        private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }
	}
}

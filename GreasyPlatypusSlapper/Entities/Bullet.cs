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

namespace GreasyPlatypusSlapper.Entities
{
	public partial class Bullet
	{

        public int TeamIndex { get; set; }
		private Vector3 startingLocation; 

        /// <summary>
        /// Initialization logic which is execute only one time for this Entity (unless the Entity is pooled).
        /// This method is called when the Entity is added to managers. Entities which are instantiated but not
        /// added to managers will not have this method called.
        /// </summary>
		private void CustomInitialize()
		{
            if(GlobalContent.FeatureFlags[FeatureFlags.EnableRocketTrails].IsEnabled)
            {
                MissileTrailInstance.Emitting = true;
            }

		}

		private void CustomActivity()
		{
			CheckMaxDistance(); 

		}

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

		private void CheckMaxDistance()
		{
			if (Vector3.Distance(startingLocation, this.Position) > MaxTravelDistance)
			{
				Destroy(); 
			}
		}

		internal void Launch(Vector3 vector3)
		{
			startingLocation = this.Position; 
			this.Velocity = vector3; 
		}
	}
}

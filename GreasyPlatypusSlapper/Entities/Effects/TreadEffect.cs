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

namespace GreasyPlatypusSlapper.Entities.Effects
{
	public partial class TreadEffect
	{
		private void CustomInitialize()
		{
            Tread1.AlphaRate = AlphaRate;
            Tread2.AlphaRate = AlphaRate;
		}

		private void CustomActivity()
		{
            if (Tread1.Alpha <= 0 || Tread2.Alpha <= 0)
            {
                Destroy();
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

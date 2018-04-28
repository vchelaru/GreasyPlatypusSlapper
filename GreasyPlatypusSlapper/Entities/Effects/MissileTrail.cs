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
using GreasyPlatypusSlapper.Utilities;
using FlatRedBall.Math;

namespace GreasyPlatypusSlapper.Entities.Effects
{
	public partial class MissileTrail
	{
        private SpriteList trails = new SpriteList();

		private void CustomInitialize()
		{
            if (DebugFeatureSettings.EnableRocketTrails)
            {
                Setup();
            }
		}

		private void CustomActivity()
		{
            if(DebugFeatureSettings.EnableRocketTrails)
            {
                EmitTrails();
            }
		}

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        private void EmitTrails()
        {
            int count = trails.Count;

            TrailEmitter.TimedEmit(trails);

            // we have a new sprite, randomize the sprite
            // TODO: this doesn't actually work because vic lies
            if(trails.Count > count)
            {
                trails.Last.CurrentFrameIndex = FlatRedBallServices.Random.Next(0, Particles["RocketTrails"].Count);
            }
        }

        private void Setup()
        {
            TrailEmitter.TimedEmission = true;
            TrailEmitter.SecondFrequency = 0.02f;
            TrailEmitter.RemovalEvent = Emitter.RemovalEventType.Alpha0;

            TrailEmitter.EmissionSettings = new EmissionSettings()
            {
                AlphaRate = -2,
                Animate = false,
                AnimationChains = Particles,
                CurrentChainName = "RocketTrails",
                RotationZ = -3.14f,
                RotationZRange = 6.28f,
                TextureScale = 1f,
                MatchScaleXToY = true,
                ScaleYVelocity = 20f
            };
        }
	}
}

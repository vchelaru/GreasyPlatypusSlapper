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
	public partial class Smoke
	{
        private SpriteList trails = new SpriteList();

        public bool Emitting
        {
            get
            {
                return EffectEmitter.TimedEmission;
            }
            set
            {
                EffectEmitter.TimedEmission = value;
            }
        }

        private void CustomInitialize()
		{
            Setup();
        }

		private void CustomActivity()
		{
            EmitTrails();
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

            EffectEmitter.TimedEmit(trails);

            // we have a new sprite, randomize the sprite
            // TODO: this doesn't actually work because vic lies
            if (trails.Count > count)
            {
                trails.Last.CurrentFrameIndex = FlatRedBallServices.Random.Next(0, Particles["RocketTrails"].Count);
            }
        }

        private void Setup()
        {
            EffectEmitter.TimedEmission = true;
            EffectEmitter.SecondFrequency = 0.02f;
            EffectEmitter.RemovalEvent = Emitter.RemovalEventType.Alpha0;
            EffectEmitter.EmissionSettings = new EmissionSettings()
            {
                AlphaRate = -2,
                Animate = false,
                AnimationChains = Particles,
                CurrentChainName = "Smoke",
                RotationZ = -3.14f,
                RotationZRange = 6.28f,
                TextureScale = 1f,
                MatchScaleXToY = true,
                ScaleYVelocity = 20f
            };
        }
    }
}

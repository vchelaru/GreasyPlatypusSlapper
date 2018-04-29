using FlatRedBall.Input;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreasyPlatypusSlapper.InputManagement
{
	/// <summary>
	/// A class which contains references to the input objects which will control a users tank. 
	/// </summary>
	public class PlayerInput
	{
		public I2DInput MovementInput { get; private set; }
		public I2DInput AimingInput { get; private set; }
		public IPressableInput ShootingInput { get; private set; }
		public IPressableInput BoostInput { get; private set; }
		public IPressableInput Start { get; private set; }

		public PlayerInput(Xbox360GamePad gamePad)
		{
			MovementInput = gamePad.LeftStick;
			AimingInput = gamePad.RightStick;
			ShootingInput = gamePad.RightTrigger;
			BoostInput = gamePad.LeftTrigger;
			Start = gamePad.GetButton(Xbox360GamePad.Button.Start); 
		}

		public PlayerInput(FlatRedBall.Input.Keyboard keyboard)
		{
			MovementInput = keyboard.Get2DInput(Microsoft.Xna.Framework.Input.Keys.A,
				Microsoft.Xna.Framework.Input.Keys.D,
				Microsoft.Xna.Framework.Input.Keys.W,
				Microsoft.Xna.Framework.Input.Keys.S);

			AimingInput = keyboard.Get2DInput(Microsoft.Xna.Framework.Input.Keys.Left,
				Microsoft.Xna.Framework.Input.Keys.Right,
				Microsoft.Xna.Framework.Input.Keys.Up,
				Microsoft.Xna.Framework.Input.Keys.Down);

			ShootingInput = keyboard.GetKey(Microsoft.Xna.Framework.Input.Keys.Space);
			BoostInput = keyboard.GetKey(Keys.Q);
			Start = keyboard.GetKey(Keys.Enter); 
		}
	}
}

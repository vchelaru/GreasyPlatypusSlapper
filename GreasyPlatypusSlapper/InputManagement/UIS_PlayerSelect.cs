using GreasyPlatypusSlapper.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlatRedBall.Gum;
using GreasyPlatypusSlapper.GumRuntimes;
using FlatRedBall.Input;

namespace GreasyPlatypusSlapper.InputManagement
{
	/// <summary>
	/// User Interaction State for controlling Player Addition, Subtraction, and Battle Start. 
	/// </summary>
	public class UIS_PlayerSelect : IUserInteractionState
	{
		private PlayerSelectionUIRuntime playerSelectionUIInstance;
		private GameScreen gameScreen;
		private Dictionary<object, PlayerInput> playerInputList = new Dictionary<object, PlayerInput>();
		private Dictionary<object, PlayerSelectionBoxRuntime> playerBoxList = new Dictionary<object, PlayerSelectionBoxRuntime>();
		private bool justAdded = false;

		public UIS_PlayerSelect(GameScreen gameScreen, PlayerSelectionUIRuntime playerSelectionUIInstance)
		{
			this.playerSelectionUIInstance = playerSelectionUIInstance;
			this.gameScreen = gameScreen;
		}

		public void Setup()
		{
			//No Operation.
		}

		public void Teardown()
		{
			//No Operation. 
		}

		public void Update()
		{
			ProcessAdditionsAndRemovals();
			if (!justAdded)
			{
				ProcessStartRequests();
			}
			justAdded = false; 
		}

		private void ProcessAdditionsAndRemovals()
		{
			ProcessXBoxControllers();
			ProcessKeyboardControllers();
		}

		private void ProcessXBoxControllers()
		{
			//For XBox controllers. 
			for (int i = 0; i < InputManager.NumberOfConnectedGamePads; i++)
			{
				var gamePad = InputManager.Xbox360GamePads[i];
				if (!playerInputList.ContainsKey(gamePad) && !playerBoxList.ContainsKey(gamePad))
				{
					if (gamePad.ButtonPushed(Xbox360GamePad.Button.A))
					{
						var playerInput = new PlayerInput(gamePad);
						AddPlayer(gamePad, playerInput);
					}
				}
				else
				{
					if (gamePad.ButtonPushed(Xbox360GamePad.Button.B))
					{
						RemovePlayer(gamePad);
					}
				}
			}
		}

		private void ProcessKeyboardControllers()
		{
			if (!playerInputList.ContainsKey(InputManager.Keyboard) && !playerBoxList.ContainsKey(InputManager.Keyboard))
			{
				if (InputManager.Keyboard.AnyKeyPushed())
				{
					var playerInput = new PlayerInput(InputManager.Keyboard);
					AddPlayer(InputManager.Keyboard, playerInput);
				}
				else
				{
					if (InputManager.Keyboard.KeyPushed(Microsoft.Xna.Framework.Input.Keys.Enter))
					{
						gameScreen.StartGame(playerInputList.Values.ToList<PlayerInput>());
					}
				}
			}
			else
			{
				if (InputManager.Keyboard.KeyPushed(Microsoft.Xna.Framework.Input.Keys.Escape))
				{
					RemovePlayer(InputManager.Keyboard); 
				}
			}
		}

		private void ProcessStartRequests()
		{
			foreach (var input in playerInputList.Values)
			{
				if (input.Start.WasJustPressed)
				{
					gameScreen.StartGame(playerInputList.Values.ToList<PlayerInput>());
				}
			}

		}

		private void AddPlayer(object inputObj, PlayerInput playerInput)
		{
			playerInputList.Add(inputObj, playerInput);
			var playerBox = new PlayerSelectionBoxRuntime();
			playerBox.Parent = playerSelectionUIInstance.GetPlayerList();
			playerBoxList.Add(inputObj, playerBox);
			justAdded = true; 
		}

		private void RemovePlayer(object inputObj)
		{
			var playerBox = playerBoxList[inputObj];
			playerBox.Parent = null;
			playerBox.Destroy();
			playerBoxList.Remove(inputObj);

			playerInputList.Remove(inputObj);
		}
	}
}

using GreasyPlatypusSlapper.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlatRedBall.Gum;
using GreasyPlatypusSlapper.GumRuntimes;
using FlatRedBall.Input;
using Microsoft.Xna.Framework;
using static GreasyPlatypusSlapper.Entities.Tank;

namespace GreasyPlatypusSlapper.InputManagement
{
	public struct PlayerData
	{
		public PlayerInput PlayerInput { get; set; }
		public PlayerSelectionBoxRuntime SelectionBox { get; set; }
		public TankColor TankColor { get; set; }
	}

	/// <summary>
	/// User Interaction State for controlling Player Addition, Subtraction, and Battle Start. 
	/// </summary>
	public class UIS_PlayerSelect : IUserInteractionState
	{
		private PlayerSelectionUIRuntime playerSelectionUIInstance;
		private GameScreen gameScreen;
		private int maxPlayers = Enum.GetNames(typeof(TankColor)).Count() - 2; 

		private Dictionary<object, PlayerData> playerDataList = new Dictionary<object, PlayerData>(); 

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
				if (!playerDataList.ContainsKey(gamePad))
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
			if (!playerDataList.ContainsKey(InputManager.Keyboard))
			{
				if (InputManager.Keyboard.AnyKeyPushed())
				{
					var playerInput = new PlayerInput(InputManager.Keyboard);
					AddPlayer(InputManager.Keyboard, playerInput);
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
			foreach (var data in playerDataList.Values)
			{
				if (data.PlayerInput.Start.WasJustPressed)
				{
					gameScreen.StartGame(playerDataList.Values.ToList<PlayerData>());
				}
			}
		}

		private void AddPlayer(object inputObj, PlayerInput playerInput)
		{	
			if (playerDataList.Count < maxPlayers)
			{
				var playerBox = new PlayerSelectionBoxRuntime();
				playerBox.Parent = playerSelectionUIInstance.GetPlayerList();
				var color = GetTankColor(); 
				var playerData = new PlayerData() { PlayerInput = playerInput, SelectionBox = playerBox, TankColor = color };
				playerDataList.Add(inputObj, playerData);
				justAdded = true;
				TryToggleStartText();
			}
		}

		private TankColor GetTankColor()
		{
			var colorIndex = playerDataList.Count + 2;
			return (TankColor)Enum.Parse(typeof(TankColor), colorIndex.ToString());
		}
		private void RemovePlayer(object inputObj)
		{
			var data = playerDataList[inputObj];
			data.SelectionBox.Parent = null;
			data.SelectionBox.Destroy();

			playerDataList.Remove(inputObj);

			TryToggleStartText();
		}

		private void TryToggleStartText()
		{
			if (playerDataList.Values.Count > 0 && playerSelectionUIInstance.CurrentReadyToStartState == PlayerSelectionUIRuntime.ReadyToStart.No)
			{
				playerSelectionUIInstance.CurrentReadyToStartState = PlayerSelectionUIRuntime.ReadyToStart.Yes;
			}
			else if (playerDataList.Values.Count == 0 && playerSelectionUIInstance.CurrentReadyToStartState == PlayerSelectionUIRuntime.ReadyToStart.Yes)
			{
				playerSelectionUIInstance.CurrentReadyToStartState = PlayerSelectionUIRuntime.ReadyToStart.No;
			}
		}
	}
}

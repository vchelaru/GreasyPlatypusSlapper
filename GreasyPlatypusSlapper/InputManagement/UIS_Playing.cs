using FlatRedBall.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreasyPlatypusSlapper.InputManagement
{
	/// <summary>
	/// Interaction State created when the Battle Starts and responsible allowing tank control and pausing. 
	/// </summary>
	public class UIS_Playing : IUserInteractionState
	{
		public void Setup()
		{
			//TODO: Turn on ability for players to control tanks. 
		}

		public void Teardown()
		{
			//TODO: Turn off ability for players to control tanks. 
		}

		public void Update()
		{
			//TODO: Detect input for pause or quit or whatever. 
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreasyPlatypusSlapper.InputManagement
{
	public interface IManagesInputStates
	{
		void LoadUserInputState(IUserInputState state);
	}
}

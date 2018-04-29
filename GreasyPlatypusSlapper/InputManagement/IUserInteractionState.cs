using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreasyPlatypusSlapper.InputManagement
{
	public interface IUserInteractionState
	{
		void Setup();
		void Teardown();
		void Update(); 
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEventBus
{
	public interface IEventArgument<TEvent>
	{
		TEvent EventType { get; }
	}
}

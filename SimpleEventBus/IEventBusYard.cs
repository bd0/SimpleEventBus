using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEventBus
{
	public interface IEventBusYard<TEvent>
	{

		IEventBus<TEvent, TEventArg> GetEventBus<TEventArg>(TEvent eventType) where TEventArg : IEventArgument<TEvent>;

	}
}

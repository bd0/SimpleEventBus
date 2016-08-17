using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEventBus
{
	/// <summary>
	/// Provides a simple implementation for storing event buses.
	/// </summary>
	/// <typeparam name="TEvent"></typeparam>
	public class SimpleEventBusYard<TEvent> : IEventBusYard<TEvent>
	{

		protected IDictionary<TEvent, object> eventBusDictionary;


		public SimpleEventBusYard()
		{
			eventBusDictionary = new Dictionary<TEvent, object>();
		}


		public virtual IEventBus<TEvent, TEventArg> GetEventBus<TEventArg>(TEvent eventType) where TEventArg : IEventArgument<TEvent>
		{
			object eventBus;

			if (!eventBusDictionary.TryGetValue(eventType, out eventBus))
			{
				// Create a new event bus and cast it to the interface type.
				eventBus = CreateEventBus<TEventArg>();

				// Store the bus to use later.
				eventBusDictionary.Add(eventType, eventBus);
			}

			// Cast the event bus to the provided argument type and return it.
			// If the event type is incorrect an exception will be thrown.
			// This probably means different type arguments were provided to
			// GetEventBus when requesting the same event.
			try
			{
				return (IEventBus<TEvent, TEventArg>)eventBus;
			}
			catch (InvalidCastException ice)
			{
				string message = $@"Failed to get event bus. Expected event bus of type ""{typeof(IEventBus<TEvent, TEventArg>).FullName}"" but it was of type ""{eventBus.GetType().FullName}"". Make sure GetEventBus is always called with consistent type arguments for events of the same type.";

				throw new InvalidCastException(message, ice);
			}
		}


		protected virtual IEventBus<TEvent, TEventArg> CreateEventBus<TEventArg>() where TEventArg : IEventArgument<TEvent>
		{
			// Return a new SimpleEventBus with the correct IEventArgument type.
			IEventBus<TEvent, TEventArg> eventBus = new SimpleEventBus<TEvent, TEventArg>();

			return eventBus;
		}

	}
}

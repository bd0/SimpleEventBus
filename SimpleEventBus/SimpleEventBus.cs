using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEventBus
{
	/// <summary>
	/// Provides a simple implementation to publish and subscribe to events.
	/// When an event is published, all event subscriptions are called 
	/// synchronously before the Publish method returns.
	/// </summary>
	/// <typeparam name="TEvent"></typeparam>
	/// <typeparam name="TEventArg"></typeparam>
	public class SimpleEventBus<TEvent, TEventArg> : IEventBus<TEvent, TEventArg> where TEventArg : IEventArgument<TEvent>
	{

		protected IList<Action<TEventArg>> subscribedHandlers;


		public SimpleEventBus()
		{
			subscribedHandlers = new List<Action<TEventArg>>();
		}


		/// <summary>
		/// Publishes an object to all subscribed handlers.
		/// </summary>
		/// <param name="eventArg"></param>
		public virtual void Publish(TEventArg eventArg)
		{
			// Invoke each of the subscribed handlers.
			foreach (var handler in subscribedHandlers)
			{
				handler(eventArg);
			}
		}


		/// <summary>
		/// Subscribes the specified handler.
		/// </summary>
		/// <param name="eventHandler"></param>
		public virtual void Subscribe(Action<TEventArg> eventHandler)
		{
			// Add the specified handler to the list.
			subscribedHandlers.Add(eventHandler);
		}


		/// <summary>
		/// Unsubscribes the specified handler.
		/// </summary>
		/// <param name="eventHandler"></param>
		public virtual void Unsubscribe(Action<TEventArg> eventHandler)
		{
			// Remove the specified subscribed handler, if found.
			subscribedHandlers.Remove(eventHandler);
		}


		/// <summary>
		/// Unsubscribes all handlers.
		/// </summary>
		public virtual void UnsubscribeAll()
		{
			// Remove all subscribed handlers.
			subscribedHandlers.Clear();
		}
	}
}

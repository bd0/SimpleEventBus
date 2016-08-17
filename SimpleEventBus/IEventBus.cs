using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEventBus
{
	/// <summary>
	/// Handles the publish/subscribe details for whatever event it represents.
	/// The event arg will be the same type for all handlers in this event bus.
	/// </summary>
	/// <typeparam name="TEventArgs"></typeparam>
	public interface IEventBus<TEvent, TEventArg> where TEventArg : IEventArgument<TEvent>
	{
		/// <summary>
		/// Publishes an object to all subscribed handlers.
		/// </summary>
		/// <param name="eventArg"></param>
		void Publish(TEventArg eventArg);

		/// <summary>
		/// Subscribes the specified handler.
		/// </summary>
		/// <param name="eventHandler"></param>
		void Subscribe(Action<TEventArg> eventHandler);

		/// <summary>
		/// Unsubscribes the specified handler.
		/// </summary>
		/// <param name="eventHandler"></param>
		void Unsubscribe(Action<TEventArg> eventHandler);

		/// <summary>
		/// Unsubscribes all handlers.
		/// </summary>
		void UnsubscribeAll();
		
	}
}

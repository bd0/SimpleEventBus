using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEventBus.Samples
{
	class Sample
	{


		private SimpleEventBusYard<MyEvents> busYard = new SimpleEventBusYard<MyEvents>();


		public static void Main()
		{
			var sample = new Sample();

			sample.GetAnEventBus();

			sample.SubscribeToEvents();

			sample.PublishEvents();

			Console.ReadKey();
		}


		private void GetAnEventBus()
		{
			// Use the GetEventBus() method to get a the event bus.
			// The type parameter specifies the Type for the event arguments
			// used for a given event type.  This ensures all 
			// publish/subscribe calls use the correct type arguments.
			// The parameter passed into the method specifies which event
			// to get the event bus for.

			// For example, this gets an event bus for the "AddSomething" event.  The argument
			// for all publish/subscribe calls will be of type "AddSomethingEventArg".
			var typedAddSomethingEventBus = busYard.GetEventBus<AddSomethingEventArg>(MyEvents.AddSomething);

			// This gets an event bus for the "StartSomething" event.  The argument
			// for all publish/subscribe calls will be of type "SampleEventArg".
			var startSomethingEventBus = busYard.GetEventBus<SampleEventArg>(MyEvents.StartSomething);

			// It is important to use the same type parameters when getting the same event bus.
			// E.g., don't try to also do 
			// var doesntWork = busYard.GetEventBus<AddSomethingEventArg>(MyEvents.StartSomething);
		}


		private void SubscribeToEvents()
		{
			busYard.GetEventBus<AddSomethingEventArg>(MyEvents.AddSomething).Subscribe((args) =>
			{
				// Do something when this event triggers.
				Console.WriteLine($@"Event type is ""{args.EventType}"" and item added was ""{args.WhatWasAdded}"".");
			});

			busYard.GetEventBus<AddSomethingEventArg>(MyEvents.AddSomething).Subscribe((args) =>
			{
				// Do something when this event triggers.
				Console.WriteLine($@"Again: Event type is ""{args.EventType}"" and item added was ""{args.WhatWasAdded}"".");
			});


		}


		private void PublishEvents()
		{
			// Publish an event to the event bus.
			busYard.GetEventBus<AddSomethingEventArg>(MyEvents.AddSomething).Publish(new AddSomethingEventArg()
			{
				EventType = MyEvents.AddSomething,
				WhatWasAdded = "sample item",
				QuantityAdded = 100,
				DateAdded = DateTime.Now
			});
		}


		#region Event and EventArg class definitions

		private enum MyEvents
		{
			AddSomething,
			RemoveSomething,
			StartSomething,
			FinishSomething
		}

		private class SampleEventArg : IEventArgument<MyEvents>
		{
			// IEventArgument interface member:
			public MyEvents EventType { get; set; }

			// Custom event data.
			public string Value { get; set; }
		}


		private class AddSomethingEventArg : IEventArgument<MyEvents>
		{
			// IEventArgument interface member:
			public MyEvents EventType { get; set; }

			// Custom event data.
			public string WhatWasAdded { get; set; }

			public int QuantityAdded { get; set; }

			public DateTime DateAdded { get; set; }
		}

		#endregion
	}
}

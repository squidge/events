using System;
using System.Collections.Generic;
using Nancy.ModelBinding;
using ServiceStack.Redis;

namespace Events
{
	public class EventsModule : Nancy.NancyModule
	{
		private RedisClient redisClient = new ServiceStack.Redis.RedisClient("localhost", 6379);

		public EventsModule ()
		{
			Get["/"] = parameters =>
			{
				Console.WriteLine("Retrieving all events from Redis storage");
				var events = redisClient.GetAll<Event>();
				return View["Index", events];
			};

			Post["/add"] = parameters =>
			{
				Event e = this.Bind();
				Console.WriteLine(String.Format("Saving {0} - {1} in Redis storage", e.What, e.When));
				redisClient.Store<Event>(e);
				return e;
			};
		}
	}
}


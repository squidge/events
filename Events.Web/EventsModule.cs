using System;
using System.Collections.Generic;
using Nancy.ModelBinding;
using ServiceStack.Redis;

namespace Events.Web
{
	public class EventsModule : Nancy.NancyModule
	{
		private RedisClient redisClient = new ServiceStack.Redis.RedisClient("localhost", 6379);

		public EventsModule ()
		{
			Get["/"] = parameters =>
			{
				var events = redisClient.GetAll<Event>();
				return View["Index", events];
			};

			Post["/add"] = parameters =>
			{
				Event e = this.Bind();
				redisClient.Store<Event>(e);
				return e;
			};
		}
	}
}


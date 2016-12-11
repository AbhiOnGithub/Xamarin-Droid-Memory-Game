﻿using System;

namespace XamGame.Events
{
	public class BackGameEvent : AbstractEvent
	{
		public override void Fire(IEventObserver eventObserver)
		{
			eventObserver.onEvent(this);
		}
	}
}
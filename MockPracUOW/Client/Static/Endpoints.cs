﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockPracUOW.Client.Static
{
	public static class Endpoints
	{
		private static readonly string Prefix = "api";

		public static readonly string TasksEndpoint = $"{Prefix}/tasks";
		public static readonly string AccountsEndpoint = $"{Prefix}/accounts";
	}
}

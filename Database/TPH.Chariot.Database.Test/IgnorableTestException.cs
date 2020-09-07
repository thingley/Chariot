using System;
using System.Collections.Generic;
using System.Text;

namespace TPH.Chariot.Database.Test
{
	internal class IgnorableTestException : SystemException
	{
		internal IgnorableTestException(string message) : base(message)
		{ }
	}
}

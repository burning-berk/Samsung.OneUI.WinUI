using System;

namespace Samsung.OneUI.WinUI.Common.Exceptions;

internal class XamlRootNullReferenceException : NullReferenceException
{
	private const string NULL_EXCEPTION_MESSAGE = "The XamlRoot value cannot be null";

	internal XamlRootNullReferenceException()
		: base("The XamlRoot value cannot be null")
	{
	}
}

using System;

namespace WPFProject_Lab4.Tools.Exceptions
{
    internal class FutureBirthdayException: Exception
    {

        internal FutureBirthdayException(string message) : base(message) { }
    }
}

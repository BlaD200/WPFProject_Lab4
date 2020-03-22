using System;

namespace WPFProject_Lab4.Tools.Exceptions
{
    internal class PastBirthdayException: Exception
    {

        internal PastBirthdayException(string message) : base(message) { }
    }
}

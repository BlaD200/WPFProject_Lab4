using System;

namespace WPFProject_Lab4.Tools.Exceptions
{
    internal class InvalidEmailException: Exception
    {

        internal InvalidEmailException(string message) : base(message) { }

    }
}

namespace ModelMatcher.Assertions
{
    using System;

    public class DidNotMatch : Exception
    {
        public DidNotMatch(string message)
            : base(message)
        {
        }
    }
}

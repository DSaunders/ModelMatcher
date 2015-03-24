namespace ModelMatcher.Exceptions
{
    using System;

    public class CollectionDoesNotMatch : Exception
    {
        public CollectionDoesNotMatch(string message) : base(message)
        {
        }
    }
}
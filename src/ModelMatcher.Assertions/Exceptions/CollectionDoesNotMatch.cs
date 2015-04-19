namespace ModelMatcher.Assertions
{
    using System;

    public class CollectionDoesNotMatch : Exception
    {
        public CollectionDoesNotMatch(string message) : base(message)
        {
        }
    }
}
namespace ModelMatcher.Exceptions
{
    using System;

    public class NoMatchingItemInCollection : Exception
    {
        public NoMatchingItemInCollection()
            : base("Could not find a matching item in the collection")
        {
        }
    }
}
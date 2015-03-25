namespace ModelMatcher.Exceptions
{
    using System;

    public class InvalidMatchExpression : Exception
    {
        public InvalidMatchExpression()
            : base("This match expression is not valid. Please pass an expression in the form '() => expectedModel.MyPropertyName'")
        {
        }
    }
}
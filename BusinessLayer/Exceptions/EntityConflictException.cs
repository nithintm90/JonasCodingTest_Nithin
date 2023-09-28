using System;

namespace BusinessLayer.Exceptions
{
    public class EntityConflictException : Exception
    {
        public EntityConflictException(string message) : base(message) {}
    }
}

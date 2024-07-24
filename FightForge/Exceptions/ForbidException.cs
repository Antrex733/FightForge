namespace FightForge.Exceptions
{
    public class ForbidException : Exception
    {
        public ForbidException(string message) : base(message) { }
        public ForbidException() : base() { }
    }
}

namespace FightForge.Authorization
{
    public enum OperationType
    {
        Read,
        Delete,
        Update,
        Create
    }
    public class ResourceOperationRequirement : IAuthorizationRequirement
    {
        public ResourceOperationRequirement(OperationType operationType)
        {
            OperationType = operationType;
        }
        public OperationType OperationType { get;}
    }
}

using GraphQL.Validation;

namespace AuditLog
{
    public class InputValidationRule : IValidationRule
    {
        public INodeVisitor Validate(ValidationContext context)
        {
            return new EnterLeaveListener(_ =>
            {
            });
        }
    }
}

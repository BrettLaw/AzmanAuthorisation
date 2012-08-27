using System.Linq.Expressions;

namespace Lockdown.Configuration.Operations
{
    public class RemoveControllersNamespaceModifier : IModifyOperationName
    {
        public string Apply(string name, MethodCallExpression expr)
        {
            return name.Replace(".Controllers.", ".");
        }
    }
}
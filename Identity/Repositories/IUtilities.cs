using Identity.ViewModels.Role;

namespace Identity.Repositories
{
    public interface IUtilities
    {
        public IList<ActionAndControllerName> ActionAndControllerNamesList();
        public IList<string> GetAllAreasNames();
        public string DataBaseRoleValidationGuid();
    }
}

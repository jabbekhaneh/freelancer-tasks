using Portal.Domain.Projects;

namespace Portal.Domain.Users;

public class User  :BaseEntity
{
    public User()
    {
        Projects=new HashSet<Project>();
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }

    #region NavigationProperty
    public ICollection<Project> Projects { get; set; }
    #endregion
}

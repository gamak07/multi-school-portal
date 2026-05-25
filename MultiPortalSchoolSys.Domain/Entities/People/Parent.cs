using MultiPortalSchoolSys.Domain.Common;

namespace MultiPortalSchoolSys.Domain.Entities.People;

public class Parent : BaseEntity
{
    // Permanent Link to Core Identity User profile
    public int UserId { get; private set; }

    public string? Occupation { get; private set; }
    public string? HomeAddress { get; private set; }

    // Using a backing field pattern to protect the collection integrity from instance replacement
    private readonly List<Student> _children = [];
    public virtual IReadOnlyCollection<Student> Children => _children.AsReadOnly();

    private Parent() { }    

    public Parent(int userId, string? occupation = null, string? homeAddress = null)
    {
        if (userId <= 0)
            throw new ArgumentException("User ID must be a positive integer.", nameof(userId));

        UserId = userId;
        UpdateProfile(occupation, homeAddress);
    }

    
    public void UpdateProfile(string? occupation, string? homeAddress)
    {
        // Apply database hygiene expression patterns
        Occupation = string.IsNullOrWhiteSpace(occupation) ? null : occupation.Trim();
        HomeAddress = string.IsNullOrWhiteSpace(homeAddress) ? null : homeAddress.Trim();
    }

   
    public void AddChild(Student child)
    {
        ArgumentNullException.ThrowIfNull(child);

        if (!_children.Any(c => c.Id == child.Id))
        {
            _children.Add(child);
        }
    }

    public void RemoveChild(Student child)
    {
        ArgumentNullException.ThrowIfNull(child);


        var existingChild = _children.FirstOrDefault(c => c.Id == child.Id);
        if (existingChild != null)
        {
            _children.Remove(existingChild);
        }
    }
}
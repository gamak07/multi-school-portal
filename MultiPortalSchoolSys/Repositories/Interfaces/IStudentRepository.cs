using MultiPortalSchoolSys.Models;

namespace MultiPortalSchoolSys.Repositories.Interfaces;

public interface IStudentRepository : IRepository<Student>
{
    /// <summary>
    /// Returns all students in a classroom, ordered by the linked User's last name.
    /// Includes the User navigation property for name access.
    /// </summary>
    Task<IEnumerable<Student>> GetByClassAsync(int classRoomId);

    /// <summary>
    /// Returns a single student with their Parent profile included.
    /// Used for parent-contact lookups.
    /// </summary>
    Task<Student?> GetWithParentAsync(int studentId);

    /// <summary>
    /// PHASE B ADDITION: Full aggregate load for a single student.
    /// Performs a single optimised query loading:
    ///   Student → User (login/name)
    ///            → Parent → Parent.User (parent name/contact)
    ///            → ClassRoom → FormTeacher → FormTeacher.User
    /// Used by the Student Dashboard and Admin profile pages.
    /// </summary>
    Task<Student?> GetWithDetailsAsync(int studentId);

    /// <summary>
    /// PHASE B FIX: Searches by navigating Student → User.FirstName / User.LastName.
    /// The name fields do not exist on Student directly.
    /// </summary>
    Task<IEnumerable<Student>> SearchByNameAsync(string name);

    /// <summary>
    /// Lookup by unique AdmissionNo. Used during login and fee invoice creation.
    /// </summary>
    Task<Student?> GetByAdmissionNoAsync(string admissionNo);
}
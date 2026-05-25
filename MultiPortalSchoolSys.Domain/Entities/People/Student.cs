using MultiPortalSchoolSys.Domain.Common;
using MultiPortalSchoolSys.Domain.Entities.Academic;

namespace MultiPortalSchoolSys.Domain.Entities.People;

public class Student : BaseEntity
{
    public int UserId { get; private set; }

    public int ParentId { get; private set; }
    public Parent? Parent { get; private set; }

    public int? ClassRoomId { get; private set; }
    public ClassRoom? ClassRoom { get; private set; }

    public string AdmissionNo { get; private set; } = string.Empty;
    public DateTime DateOfBirth { get; private set; }
    public DateTime EnrollmentDate { get; private set; } = DateTime.UtcNow;

    private Student() { }

    public Student(int userId, int parentId, string admissionNo, DateTime dateOfBirth, DateTime? enrollmentDate = null)
    {
        if (userId <= 0) throw new ArgumentException("User ID must be a positive integer.", nameof(userId));
        if (parentId <= 0) throw new ArgumentException("Parent ID must be a positive integer.", nameof(parentId));
        
        UserId = userId;
        ParentId = parentId;
        EnrollmentDate = enrollmentDate ?? DateTime.UtcNow;

        UpdateProfile(admissionNo, dateOfBirth);
    }

 
    public void UpdateProfile(string admissionNo, DateTime dateOfBirth)
    {
        if (string.IsNullOrWhiteSpace(admissionNo)) throw new ArgumentException("Admission number cannot be empty.", nameof(admissionNo));
        
        // Guard Check: Verify basic calendar timeline sequence sanity
        if (dateOfBirth.Date > DateTime.UtcNow.Date) 
            throw new ArgumentException("Date of birth cannot be set in the future.", nameof(dateOfBirth));
            
        if (EnrollmentDate.Date < dateOfBirth.Date)
            throw new ArgumentException("Enrollment date cannot be a timestamp prior to the student's birth date.");

        AdmissionNo = admissionNo.Trim();
        DateOfBirth = dateOfBirth.Date; // Strips raw time metrics for clean calendar consistency
    }

   
    public void AssignToClassRoom(int? classRoomId)
    {
        if (classRoomId.HasValue && classRoomId.Value <= 0)
            throw new ArgumentException("Invalid classroom ID specification.", nameof(classRoomId));

        ClassRoomId = classRoomId;
    }
}
using MultiPortalSchoolSys.Application.Common.Interfaces.Repositories;


namespace MultiPortalSchoolSys.Application.Common.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IClassRoomRepository ClassRooms { get; }
    IGradingSettingRepository GradingSettings { get; }
    IStudentAttendanceRepository StudentAttendances { get; }
    IStudentResultRepository StudentResults { get; }
    ISubjectRepository Subjects { get; }

    ICbtExamRepository CbtExams { get; }
    ICbtQuestionRepository CbtQuestions { get; }
    IPrintableExamRepository PrintableExams { get; }
    IStudentAnswerRepository StudentAnswers { get; }
    IStudentCbtAttemptRepository StudentCbtAttempts { get; }
    ITheoryExamRepository TheoryExams { get; }
    ITheoryQuestionRepository TheoryQuestions { get; }


    IAcademicTermRepository AcademicTerms { get; }
    ISchoolEventRepository SchoolEvents { get; }

    ILessonNoteRepository LessonNotes { get; }
    IMaterialRepository Materials { get; }

    IFeeInvoiceRepository FeeInvoices { get; }
    IPaymentReceiptRepository PaymentReceipts { get; }
    IPayrollRepository Payrolls { get; }

    ILeaveRequestRepository LeaveRequests { get; }
    ISanctionRepository Sanctions { get; }
    IStaffAttendanceRepository StaffAttendances { get; }

    IParentRepository Parents { get; }
    IStudentRepository Students { get; }
    ITeacherRepository Teachers { get; }
    Task<int> SaveChangesAsync();
}
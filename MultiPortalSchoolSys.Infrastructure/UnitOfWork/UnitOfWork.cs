using MultiPortalSchoolSys.Application.Interfaces;
using MultiPortalSchoolSys.Application.Interfaces.Repositories;
using MultiPortalSchoolSys.Infrastructure.Data;
using MultiPortalSchoolSys.Infrastructure.Repositories;

namespace MultiPortalSchoolSys.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    private IStudentRepository?    _students;
    private ITeacherRepository?    _teachers;
    private IParentRepository?     _parents;
    private IResultRepository?     _results;
    private IAttendanceRepository? _attendances;
    private IExamRepository?       _exams;
    private IFeeRepository?        _fees;
    private IPayrollRepository?    _payrolls;
    private ILeaveRepository?      _leaves;
    private ISanctionRepository?   _sanctions;
    private IMaterialRepository?   _materials;
    private ICalendarRepository?   _calendar;

    public UnitOfWork(ApplicationDbContext context)
        => _context = context;

    public IStudentRepository    Students    => _students    ??= new StudentRepository(_context);
    public ITeacherRepository    Teachers    => _teachers    ??= new TeacherRepository(_context);
    public IParentRepository     Parents     => _parents     ??= new ParentRepository(_context);
    public IResultRepository     Results     => _results     ??= new ResultRepository(_context);
    public IAttendanceRepository Attendances => _attendances ??= new AttendanceRepository(_context);
    public IExamRepository       Exams       => _exams       ??= new ExamRepository(_context);
    public IFeeRepository        Fees        => _fees        ??= new FeeRepository(_context);
    public IPayrollRepository    Payrolls    => _payrolls    ??= new PayrollRepository(_context);
    public ILeaveRepository      Leaves      => _leaves      ??= new LeaveRepository(_context);
    public ISanctionRepository   Sanctions   => _sanctions   ??= new SanctionRepository(_context);
    public IMaterialRepository   Materials   => _materials   ??= new MaterialRepository(_context);
    public ICalendarRepository   Calendar    => _calendar    ??= new CalendarRepository(_context);

    public async Task<int> SaveChangesAsync()
        => await _context.SaveChangesAsync();

    public void Dispose()
        => _context.Dispose();
}
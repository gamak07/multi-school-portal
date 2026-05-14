using MultiPortalSchoolSys.Data;
using MultiPortalSchoolSys.Repositories;
using MultiPortalSchoolSys.Repositories.Interfaces;
using MultiPortalSchoolSys.UnitOfWork.Interfaces;

namespace MultiPortalSchoolSys.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    private IStudentRepository?    _students;
    private IResultRepository?     _results;
    private IAttendanceRepository? _attendances;
    private IExamRepository?       _exams;
    private IFeeRepository?        _fees;
    private IMaterialRepository?   _materials;
    private IPayrollRepository?    _payrolls;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    
    public IStudentRepository Students
        => _students ??= new StudentRepository(_context);

    public IResultRepository Results
        => _results ??= new ResultRepository(_context);

    public IAttendanceRepository Attendances
        => _attendances ??= new AttendanceRepository(_context);

    public IExamRepository Exams
        => _exams ??= new ExamRepository(_context);

    public IFeeRepository Fees
        => _fees ??= new FeeRepository(_context);

    public IMaterialRepository Materials
        => _materials ??= new MaterialRepository(_context);

    public IPayrollRepository Payrolls
        => _payrolls ??= new PayrollRepository(_context);

    public async Task<int> SaveChangesAsync()
        => await _context.SaveChangesAsync();

    public void Dispose()
        => _context.Dispose();
}
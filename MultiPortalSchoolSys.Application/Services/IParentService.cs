using MultiPortalSchoolSys.Application.Common;
using MultiPortalSchoolSys.Application.DTOs.Parent;

namespace MultiPortalSchoolSys.Application.Interfaces.Services;

public interface IParentService
{
    Task<Result<ParentDto>> GetByIdAsync(int id);
    Task<Result<ParentDto>> GetWithChildrenAsync(int id);
    Task<Result<IEnumerable<ParentDto>>> GetAllAsync();
    Task<Result<ParentDto>> CreateAsync(CreateParentDto dto);
    Task<Result> UpdateAsync(int id, UpdateParentDto dto);
    Task<Result> DeleteAsync(int id);
}
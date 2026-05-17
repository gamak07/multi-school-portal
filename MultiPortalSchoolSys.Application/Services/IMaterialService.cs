using MultiPortalSchoolSys.Application.Common;
using MultiPortalSchoolSys.Application.DTOs.Material;

namespace MultiPortalSchoolSys.Application.Interfaces.Services;

public interface IMaterialService
{
    Task<Result<IEnumerable<MaterialDto>>> GetBySubjectAndClassAsync(int subjectId, int classRoomId);
    Task<Result<IEnumerable<MaterialDto>>> GetByTeacherAsync(int teacherId);
    Task<Result<MaterialDto>> UploadAsync(CreateMaterialDto dto);
    Task<Result> DeleteAsync(int materialId, int teacherId);
}
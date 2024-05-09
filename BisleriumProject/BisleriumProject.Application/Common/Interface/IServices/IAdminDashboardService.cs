using BisleriumProject.Application.DTOs;

namespace BisleriumProject.Application.Common.Interface.IServices
{
    public interface IAdminDashboardService
    {
        Task<AdminDashboardDataDTO> GetCumulativeCount();
        Task<AdminDashboardDataDTO> GetMonthlyCount(int month, int year);
        Task<List<BlogDTO>> GetTopPosts(int month, int year);
        Task<List<UserDTO>> GetTopBloggers(int month, int year);
    }
}

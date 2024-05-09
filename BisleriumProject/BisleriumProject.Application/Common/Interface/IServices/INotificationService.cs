using BisleriumProject.Application.DTOs;
using BisleriumProject.Domain.Entities;

namespace BisleriumProject.Application.Common_Interfaces.IServices
{
    public interface INotificationService
    {
        Task SaveNotificationAsync(NotificationDTO notification);
        Task<NotificationDTO> SaveNotificationDTOAsync(NotificationDTO notificationDto);
        Task<IEnumerable<NotificationDTO>> GetNotificationsForUserAsync(string userId);
        Task<int> CountUnreadNotifications(string userId);
        Task MarkNotificationsAsRead(string userId);
    }

}
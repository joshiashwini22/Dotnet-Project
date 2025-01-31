﻿using BisleriumProject.Infrastructures.Persistence;
using BisleriumProject.Application.Common_Interfaces.IServices;
using BisleriumProject.Application.DTOs;
using BisleriumProject.Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace BisleriumProject.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(AppDbContext context, ILogger<NotificationService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task SaveNotificationAsync(NotificationDTO notificationDto)
        {
            try
            {
                var notification = new Notification
                {
                    SenderId = notificationDto.SenderId,
                    ReceiverId = notificationDto.ReceiverId,
                    Message = notificationDto.Message,
                    Timestamp = DateTime.UtcNow,
                    IsRead = false
                };

                _context.Notifications.Add(notification);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception to understand what went wrong
                _logger.LogError("Error saving notification: {Message}", ex.Message);
                throw; // Or handle accordingly
            }
        }
        public async Task<NotificationDTO> SaveNotificationDTOAsync(NotificationDTO notificationDto)
        {
            var notification = new Notification
            {
                SenderId = notificationDto.SenderId,
                ReceiverId = notificationDto.ReceiverId,
                Message = notificationDto.Message,
                Timestamp = DateTime.UtcNow, // Set server-side to ensure consistency
                IsRead = false // Default to false when initially saving
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            notificationDto.Id = notification.Id; // Ensure the DTO has the ID set from the database
            notificationDto.Timestamp = notification.Timestamp; // Set the exact timestamp from the database
            return notificationDto; // Return the updated DTO
        }

        public async Task<IEnumerable<NotificationDTO>> GetNotificationsForUserAsync(string userId)
        {
            var notifications = await _context.Notifications
                                              .Where(n => n.ReceiverId == userId)
                                              .Select(n => new NotificationDTO
                                              {
                                                  Id = n.Id,
                                                  SenderId = n.SenderId,
                                                  ReceiverId = n.ReceiverId,
                                                  Message = n.Message,
                                                  Timestamp = n.Timestamp,
                                                  IsRead = n.IsRead
                                              })
                                              .ToListAsync();
            return notifications;
        }

        public async Task<int> CountUnreadNotifications(string userId)
        {
            return await _context.Notifications
                                 .Where(n => n.ReceiverId == userId && !n.IsRead)
                                 .CountAsync();
        }

        public async Task MarkNotificationsAsRead(string userId)
        {
            var notifications = _context.Notifications
                                        .Where(n => n.ReceiverId == userId && !n.IsRead).ToList();

            notifications.ForEach(n => n.IsRead = true);
            await _context.SaveChangesAsync();
        }
    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.API.Controllers.NotificationAPIs.ViewModels;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Interfaces;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.Common.Exceptions;
using SmartHomeManager.Domain.NotificationDomain.Entities;
using SmartHomeManager.Domain.NotificationDomain.Interfaces;
using SmartHomeManager.Domain.NotificationDomain.Services;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

public static class DTONotificationFactory
{
    public static DTO createResponseDTO(int a, IEnumerable<Notification> notifications, int statusCode = 0, string statusmessage = "")
    {

        switch (a)
        {
            case 0:
                return getNotificationDTO(notifications, statusCode, statusmessage);
            default:
                return null;
        }
    }
    public static DTO createRequestDTO(int a, IEnumerable<Notification> notifications, int statusCode = 0, string statusmessage = "")
    {

        switch (a)
        {
            case 0:
                return sendNotificationDTO(notifications);
            default:
                return null;
        }
    }
    private static DTO getNotificationDTO(IEnumerable<Notification> notifications, int statusCode, string statusMessage)
    {
        List<GetNotificationDTO> getNotifications = new List<GetNotificationDTO>();

        if (notifications != null) {
            foreach (var notification in notifications)
            {
                getNotifications.Add(new GetNotificationDTO
                {
                    NotificationId = notification.NotificationId,
                    AccountId = notification.AccountId,
                    NotificationMessage = notification.NotificationMessage,
                    SentTime = notification.SentTime,
                });
            }
        }

        return new ResponseDTO
        {
            data = getNotifications,
            response = new ResponseObjectDTO
            {
                data = getNotifications,
                ServerMessage = statusMessage,
                StatusCode = statusCode

            }
        };
    }

    private static DTO sendNotificationDTO(IEnumerable<Notification> notifications)
    {

        List<AddNotificationDTO> getNotifications = new List<AddNotificationDTO>();


        foreach (var notification in notifications)
        {
            getNotifications.Add(new AddNotificationDTO
            {
                Message = notification.NotificationMessage,
                AccountId = notification.AccountId
            });
        }

        return new RequestDTO
        {
            data = getNotifications
        };
    }
}


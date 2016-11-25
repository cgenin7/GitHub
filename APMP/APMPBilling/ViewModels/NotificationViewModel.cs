using APMPRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APMPBilling.ViewModels
{
    public class NotificationViewModel
    {
        public NotificationViewModel()
        {
            Notification = new NotificationModel();
        }

        public bool BeNotified { get; set; }
        public NotificationModel Notification { get; set; }
    }
}
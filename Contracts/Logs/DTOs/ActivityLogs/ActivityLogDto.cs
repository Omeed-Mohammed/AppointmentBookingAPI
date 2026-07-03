using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingAPI.Contracts.Logs.DTOs.ActivityLogs
{
    public class ActivityLogDto
    {
        public int LogID { get; set; }
        public string ActionType { get; set; }
        public string EntityType { get; set; }
        public int EntityID { get; set; }
        public string? Description { get; set; }
        public string PerformedBy { get; set; }
        public DateTime PerformedAt { get; set; }

        public ActivityLogDto(
            int logID,
            string actionType,
            string entityType,
            int entityID,
            string? description,
            string performedBy,
            DateTime performedAt)
        {
            LogID = logID;
            ActionType = actionType;
            EntityType = entityType;
            EntityID = entityID;
            Description = description;
            PerformedBy = performedBy;
            PerformedAt = performedAt;
        }
    }
}

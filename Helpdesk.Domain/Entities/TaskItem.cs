using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Helpdesk.Domain.Enums;
using TaskStatus = Helpdesk.Domain.Enums.TaskStatus;

namespace Helpdesk.Domain.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = "";
        public string Description { get; set; } = "";
        public int ReporterId { get; set; }
        public User Reporter { get; set; }
        public int SolverId { get; set; }
        public User Solver { get; set; }
        public DateTime ReportedAt { get; set; }
        public DateTime DueAt { get; set; }
        public Priority Priority { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime? ResolvedAt { get; set; }
        public ICollection<TaskMessage> Messages { get; set; } = new List<TaskMessage>();
        public ICollection<ChecklistItem> ChecklistItems { get; set; } = new List<ChecklistItem>();
        public ICollection<TaskAttachment> Attachments {  get; set; } = new List<TaskAttachment>();

        public void MarkDone()
        {
            Status = TaskStatus.Done;
            ResolvedAt = DateTime.UtcNow;
        }

        public void Reopen()
        {
            Status = TaskStatus.InProgress;
            ResolvedAt = null;
        }
    }
}

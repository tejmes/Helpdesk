using Helpdesk.Domain.Entities;
using Helpdesk.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using TaskStatus = Helpdesk.Domain.Enums.TaskStatus;

namespace Helpdesk.Infrastructure.Database
{
    public class DatabaseInit
    {
        public List<User> GetUsers()
        {
            return new List<User>
            {
                new User { Id = 1, FullName = "Alice Admin" },
                new User { Id = 2, FullName = "Bob Solver" },
                new User { Id = 3, FullName = "Charlie Tester" }
            };
        }

        public List<TaskItem> GetTasks()
        {
            return new List<TaskItem>
            {
                new TaskItem
                {
                    Id = 1,
                    CompanyName = "Contoso",
                    Description = "Login page does not work",
                    ReporterId = 1,
                    SolverId = 2,
                    ReportedAt = new DateTime(2026, 2, 27, 12, 0, 0, DateTimeKind.Utc),
                    DueAt      = new DateTime(2026, 3, 4, 12, 0, 0, DateTimeKind.Utc),
                    Priority = Priority.High,
                    Status = TaskStatus.InProgress,
                    ResolvedAt = null
                },

                new TaskItem
                {
                    Id = 2,
                    CompanyName = "Fabrikam",
                    Description = "Email notifications not sent",
                    ReporterId = 3,
                    SolverId = 2,
                    ReportedAt = new DateTime(2026, 2, 19, 12, 0, 0, DateTimeKind.Utc),
                    DueAt      = new DateTime(2026, 2, 27, 12, 0, 0, DateTimeKind.Utc),
                    Priority = Priority.Medium,
                    Status = TaskStatus.New,
                    ResolvedAt = null
                },

                new TaskItem
                {
                    Id = 3,
                    CompanyName = "Northwind",
                    Description = "Update address on invoice template",
                    ReporterId = 1,
                    SolverId = 3,
                    ReportedAt = new DateTime(2026, 2, 22, 12, 0, 0, DateTimeKind.Utc),
                    DueAt      = new DateTime(2026, 2, 24, 12, 0, 0, DateTimeKind.Utc),
                    Priority = Priority.Low,
                    Status = TaskStatus.Done,
                    ResolvedAt = new DateTime(2026, 2, 25, 12, 0, 0, DateTimeKind.Utc)
                }
            };
        }

        public List<ChecklistItem> GetChecklistItems()
        {
            return new List<ChecklistItem>
            {
                new ChecklistItem
                {
                    Id = 1,
                    TaskItemId = 1,
                    Title = "Check application logs",
                    IsDone = true,
                    DoneAt = new DateTime(2026, 3, 1, 7, 0, 0, DateTimeKind.Utc),
                    DueAt  = new DateTime(2026, 3, 2, 12, 0, 0, DateTimeKind.Utc)
                },
                new ChecklistItem
                {
                    Id = 2,
                    TaskItemId = 1,
                    Title = "Reproduce the issue",
                    IsDone = false,
                    DoneAt = null,
                    DueAt  = new DateTime(2026, 3, 3, 12, 0, 0, DateTimeKind.Utc)
                },

                new ChecklistItem
                {
                    Id = 3,
                    TaskItemId = 2,
                    Title = "Verify SMTP settings",
                    IsDone = false,
                    DoneAt = null,
                    DueAt  = new DateTime(2026, 2, 28, 12, 0, 0, DateTimeKind.Utc)
                }
            };
        }

        public List<TaskMessage> GetMessages()
        {
            return new List<TaskMessage>
            {
                new TaskMessage
                {
                    Id = 1,
                    TaskItemId = 1,
                    AuthorId = 1,
                    Message = "Users report that login fails with error 500",
                    CreatedAt = new DateTime(2026, 3, 1, 2, 0, 0, DateTimeKind.Utc)
                },
                new TaskMessage
                {
                    Id = 2,
                    TaskItemId = 1,
                    AuthorId = 2,
                    Message = "Investigating the issue",
                    CreatedAt = new DateTime(2026, 3, 1, 4, 0, 0, DateTimeKind.Utc)
                },
                new TaskMessage
                {
                    Id = 3,
                    TaskItemId = 2,
                    AuthorId = 3,
                    Message = "It stopped sending emails after last deployment.",
                    CreatedAt = new DateTime(2026, 2, 26, 12, 0, 0, DateTimeKind.Utc)
                }
            };
        }
    }
}

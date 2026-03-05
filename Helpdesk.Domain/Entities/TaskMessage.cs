using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Domain.Entities
{
    public class TaskMessage
    {
        public int Id { get; set; }
        public int TaskItemId { get; set; }
        public TaskItem TaskItem { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }
        public string Message { get; set; } = "";
        public DateTime CreatedAt { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Domain.Entities
{
    public class ChecklistItem
    {
        public int Id { get; set; }
        public int TaskItemId { get; set; }
        public TaskItem TaskItem {  get; set; }
        public string Title { get; set; } = "";
        public bool IsDone { get; set; }
        public DateTime? DoneAt { get; set; }
        public DateTime? DueAt {  get; set; }

        public void MarkDone()
        {
            IsDone = true;
            DoneAt = DateTime.UtcNow;
        }

        public void Reopen()
        {
            IsDone = false;
            DoneAt = null;
        }
    }
}

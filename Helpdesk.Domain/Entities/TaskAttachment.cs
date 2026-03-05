using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Domain.Entities
{
    public class TaskAttachment
    {
        public int Id { get; set; }
        public int TaskItemId {  get; set; }
        public TaskItem TaskItem {  get; set; }
        public string OriginalFileName { get; set; } = "";
        public string StoredFileName { get; set; } = "";
        public string ContentType { get; set; } = "";
        public long Size { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}

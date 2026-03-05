using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
    }
}

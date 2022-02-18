using System;
using System.Collections.Generic;

namespace Sindile.InternAPI.Data
{
    public partial class Task
    {
        public Task()
        {
            TaskLogs = new HashSet<TaskLog>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double? Duration { get; set; }
        public bool? Deleted { get; set; }
        public DateTime Created { get; set; }

        public virtual ICollection<TaskLog> TaskLogs { get; set; }
    }
}

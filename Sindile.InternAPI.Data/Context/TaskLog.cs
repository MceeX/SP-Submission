namespace Sindile.InternAPI.Data
{
    public partial class TaskLog
    {
        public int Id { get; set; }
        public int InternId { get; set; }
        public int TaskId { get; set; }
        public double Duration { get; set; }
        public DateTime DateTime { get; set; }
        public bool? Deleted { get; set; }
        public double HourlyRate { get; set; }

        public virtual Occupation Intern { get; set; }
        public virtual Task Task { get; set; }
    }
}

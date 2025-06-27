namespace TodoTrackerAPI.Requests
{
    public class TodoRequest
    {
        public string Task { get; set; }

        public bool IsCompleted { get; set; } = false;

    }
}

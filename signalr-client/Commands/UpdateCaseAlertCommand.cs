namespace signalr_client.Commands
{
    public record UpdateCaseAlertCommand
    {
        public bool? IsActive { get; set; }

        public string? Analyst { get; set; }
    }
}
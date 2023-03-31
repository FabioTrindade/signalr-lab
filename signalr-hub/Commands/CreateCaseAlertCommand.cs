namespace signalr_lab.Commands
{
    public record CreateCaseAlertCommand
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}

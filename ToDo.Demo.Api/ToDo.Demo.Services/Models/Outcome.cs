namespace ToDo.Demo.Services.Models
{
    public class Outcome
    {
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
    }

    public static class OutcomeExtensions
    {
        public static void AddError(this Outcome outcome, string message)
        {
            outcome.IsSuccess = false;
            outcome.ErrorMessage += message;
        }
    }
}

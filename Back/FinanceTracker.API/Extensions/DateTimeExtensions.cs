namespace FinanceTracker.API.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateOnly dateOnly) 
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var age = today.Year - today.Year;
            if (dateOnly > today.AddYears(-age)) age--;
            return age;
        }
    }
}

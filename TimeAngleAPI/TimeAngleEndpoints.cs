namespace TimeAngleAPI
{
    public static class TimeAngleEndpoints
    {
        public static async Task<IResult> CalculateTimeAngle(TimeAngleDTO inputTimeAngle)
        {
            var hours = inputTimeAngle.hours;
            var minutes = inputTimeAngle.minutes;
            TimeOnly parsedInput;
            if (!String.IsNullOrEmpty(inputTimeAngle.time) && TimeOnly.TryParse(inputTimeAngle.time, out parsedInput))
            {
                hours = parsedInput.Hour;
                minutes = parsedInput.Minute;
            }

            if (hours.HasValue && minutes.HasValue)
            {
                return TypedResults.Ok(CalculateTimeAngleWithHoursAndMinutes(hours.Value, minutes.Value));
            }

            return TypedResults.BadRequest("Invalid input. Please provide a parseable time *or* the hours and minutes of the time you want to calculate for");
        }

        static string CalculateTimeAngleWithHoursAndMinutes(int hours, int minutes)
        {
            int minuteAngle = minutes * 6; //60 minutes, 360 degrees, each minute is 6 degrees.
            decimal hourAngle = (hours % 12) * 30; //starting value based on hour position. Mod 12 to accommodate military time

            hourAngle += (decimal)minutes / 2; //minutes * 30 degrees / 60 minutes, simplifies to divided by 2

            return $"{hours} hours and {minutes} minutes is {hourAngle + minuteAngle} combined degrees";
        }
    }
}

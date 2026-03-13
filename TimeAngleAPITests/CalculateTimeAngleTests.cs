using Microsoft.AspNetCore.Http.HttpResults;
using TimeAngleAPI;

namespace TimeAngleAPITests
{
    public class CalculateTimeAngleTests
    {
        [Fact]
        public async Task ValidDTOReturnsCorrectDegreeCount()
        {
            TimeAngleDTO inputDTO = new TimeAngleDTO()
            {
                hours = 2, //60 degrees + 22.5 degrees
                minutes = 45 //270 degrees
            };
            var result = await TimeAngleEndpoints.CalculateTimeAngle(inputDTO);
            var value = (Ok<string>)result;

            Assert.Equal("2 hours and 45 minutes is 352.5 combined degrees", value.Value);
        }

        [Fact]
        public async Task CanParseTimeToCalculateAngle()
        {
            TimeAngleDTO inputDTO = new TimeAngleDTO()
            {
                time = "13:30" //hours is 45 degrees (1.5 hours), minutes is 180 degrees
            };
            var result = await TimeAngleEndpoints.CalculateTimeAngle(inputDTO);
            var value = (Ok<string>)result;

            Assert.Equal("13 hours and 30 minutes is 225 combined degrees", value.Value);
        }

        [Fact]
        public async Task InvalidTimeWillResultInBadRequest()
        {
            TimeAngleDTO inputDTO = new TimeAngleDTO()
            {
                time = "26:00"
            };
            var result = await TimeAngleEndpoints.CalculateTimeAngle(inputDTO);
            var value = (BadRequest<string>)result;

            Assert.Equal("Invalid input. Please provide a parseable time *or* the hours and minutes of the time you want to calculate for", value.Value);
        }
    }
}

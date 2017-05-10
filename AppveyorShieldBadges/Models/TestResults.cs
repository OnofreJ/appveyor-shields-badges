namespace AppveyorShieldBadges.Models
{
    internal class TestResults
    {
        public long PassingCount { get; }
        public long FailingCount { get; }

        public TestResults(long passing, long failing)
        {
            PassingCount = passing;
            FailingCount = failing;
        }
    }
}

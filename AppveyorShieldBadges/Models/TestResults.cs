namespace AppveyorShieldBadges.Models
{
    internal class TestResults
    {
        public bool BuildRunning { get; }
        public long PassingCount { get; }
        public long FailingCount { get; }
        
        public TestResults(bool running, long passing, long failing)
        {
            BuildRunning = running;
            PassingCount = passing;
            FailingCount = failing;
        }
    }
}

using Demo_1670.Models;

public static class Datastore
{
    public static List<JobListing> JobListings { get; set; } = new List<JobListing>
    {
        new JobListing { Title = "Backend Developer", Location = "Hanoi", PostedDate = DateTime.Now.AddDays(-2) },
        new JobListing { Title = "UI/UX Designer", Location = "Da Nang", PostedDate = DateTime.Now.AddDays(-5) }
    };
}

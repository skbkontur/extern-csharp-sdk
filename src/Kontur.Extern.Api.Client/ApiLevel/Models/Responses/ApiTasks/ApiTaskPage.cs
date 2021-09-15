namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.ApiTasks
{
    public class ApiTaskPage
    {
        public long Skip { get; set; }
        public long Take { get; set; }
        public long TotalCount { get; set; }
        public ApiTaskStatus[] ApiTaskPageItems { get; set; } = null!;
    }
}
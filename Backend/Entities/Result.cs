namespace PointHomeworkWithGeneric.Entities
{
    public class Result
    {
        public bool IsSuccess { get; set; } = false;

        public string Message { get; set; } = "";

        public object Data { get; set; }
    }
}

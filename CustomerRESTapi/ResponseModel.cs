namespace CustomerRESTapi
{
    public class ResponseModel<T>
    {
        public bool IsSuccess { get; set; }
        public string? Description { get; set; }
        public T? Result { get; set; }
    }
}

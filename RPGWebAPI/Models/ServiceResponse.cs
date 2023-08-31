namespace RPGWebAPI.Models
{
    // <T> Is the type of data that we want to return.
    public class ServiceResponse<T>
    {
        // Data of Type T is data like the RPG Characters.
        public T? Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}

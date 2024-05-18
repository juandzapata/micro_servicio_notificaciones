namespace FirebaseManager.Dtos
{
    public class CustomResponse<T> 
    {
        public T data { get; set; }
        public string message { get; set; }
        public int statusCode { get; set; }

        public CustomResponse(T data, string message)
        {
            this.data = data;
            this.message = message;
            this.statusCode = 200;
        }

        public CustomResponse(T data, string message, int statusCode) 
        { 
            this.data = data;
            this.message = message;
            this.statusCode = statusCode;
        }

    }
}

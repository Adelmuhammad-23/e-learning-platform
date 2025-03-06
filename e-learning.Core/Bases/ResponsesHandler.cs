namespace e_learning.Core.Bases
{
    public class ResponsesHandler
    {
        public Responses<T> Deleted<T>(string Message = null)
        {
            return new Responses<T>()
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Succeeded = true,
                Message = Message == null ? "Deleted" : Message
            };
        }
        public Responses<T> Success<T>(T entity, object Meta = null)
        {
            return new Responses<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,

                Message = "Success",
                Meta = Meta
            };
        }
        public Responses<T> Unauthorized<T>(string Message = null)
        {
            return new Responses<T>()
            {
                StatusCode = System.Net.HttpStatusCode.Unauthorized,
                Succeeded = true,
                Message = "UnAuthorized"
            };
        }
        public Responses<T> BadRequest<T>(string Message = null)
        {
            return new Responses<T>()
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Succeeded = false,
                Message = Message == null ? "BadRequest" : Message
            };

        }
        public Responses<T> UnprocessableEntity<T>(string Message = null)
        {
            return new Responses<T>()
            {
                StatusCode = System.Net.HttpStatusCode.UnprocessableEntity,
                Succeeded = false,
                Message = Message == null ? "UnprocessableEntity" : Message
            };
        }

        public Responses<T> NotFound<T>(string message = null)
        {
            return new Responses<T>()
            {
                StatusCode = System.Net.HttpStatusCode.NotFound,
                Succeeded = false,
                Message = message == null ? "NotFound " : message
            };
        }

        public Responses<T> Created<T>(T entity, object Meta = null)
        {
            return new Responses<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.Created,
                Succeeded = true,
                Message = "Created",
                Meta = Meta
            };
        }
    }
}

using Microsoft.AspNetCore.Http;
using CodingChallenge.Core.Common.Enum;
using CodingChallenge.Core.Models;
using CodingChallenge.Core.Models.Common;

namespace CodingChallenge.Core.Builders
{
    /// <summary>
    /// 
    /// </summary>
    public class ResponseBuilder<T> : IResponseBuilder<T>
    {
        private readonly APIResponse<T> _aPIResponse;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseBuilder{T}"/> class.
        /// </summary>
        public ResponseBuilder()
        {
            _aPIResponse = new APIResponse<T>();
            _aPIResponse.Errors = new List<Error>();
            _aPIResponse.HttpStatus = StatusCodes.Status200OK;
        }

        /// <summary>
        /// Adds the status.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        public ResponseBuilder<T> AddHttpStatus(int? status)
        {
            _aPIResponse.HttpStatus = status;
            return this;
        }

        /// <summary>
        /// Adds the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public ResponseBuilder<T> AddMessage(string message)
        {
            _aPIResponse.Message = message;
            return this;
        }

        /// <summary>
        /// Adds the success data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public ResponseBuilder<T> AddSuccessData(T data)
        {
            _aPIResponse.Data = data;
            this.AddMessage("Success");
            return this;
        }

        /// <summary>
        /// Adds the error.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        public ResponseBuilder<T> AddError(Error error)
        {
            _aPIResponse.Errors.Add(error);
            this.AddMessage("Error");
            return this;
        }

        /// <summary>
        /// Adds the error.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public ResponseBuilder<T> AddError(object error, int code = 000)
        {
            _aPIResponse.Errors.Add(new Error() { DevMessage = error.ToString(), ErrorCode = code });
            this.AddMessage("Error");
            return this;
        }

        /// <summary>
        /// Adds the errors.
        /// </summary>
        /// <param name="errors">The errors.</param>
        /// <returns></returns>
        public ResponseBuilder<T> AddErrors(List<Error> errors)
        {
            _aPIResponse.Errors.AddRange(errors);
            this.AddMessage("Error");
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        public APIResponse<T> Build()
        {
            _aPIResponse.ApiResponseStatus = (_aPIResponse.Errors.Count == 0) ? ResponseStatus.OK.ToString() : ResponseStatus.Failed.ToString();
            return _aPIResponse;
        }
    }
}

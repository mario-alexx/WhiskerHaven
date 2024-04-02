using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WhiskerHaven.Application.Models
{
    /// <summary>
    /// Represents a standard response object for API operations.
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// Gets or sets the HTTP status code of the response.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the operation was successful.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Gets or sets a list of error messages if the operation was unsuccessful.
        /// </summary>
        public List<string> ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the result data of the operation.
        /// </summary>
        public object Result { get; set; }
    }
}

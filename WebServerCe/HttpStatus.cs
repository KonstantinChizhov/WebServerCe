using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace WebServerCe
{
    public enum HttpStatus
    {
        None,
        OK = 200,
        Accepted = 202,
        BadRequest = 400,
        Unauthorized = 401,
        Forbiden = 403,
        NotFound = 404,
        InternalServerError = 500
    }
}

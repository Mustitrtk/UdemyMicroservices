using FreeCourse.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Shared.ControllerBases
{
    public class CustomeBaseController : Controller
    {
        public IActionResult CreateActionResultInstance<T>(ResponseDTO<T> response)
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode,
            };
        }
    }
}

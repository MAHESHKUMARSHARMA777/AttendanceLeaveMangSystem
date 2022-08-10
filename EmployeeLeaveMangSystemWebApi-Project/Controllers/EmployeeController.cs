using EmployeeLeaveMang.DomainLayer.Models;
using EmployeeLeaveMang.ServiceLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeLeaveMangSystemWebApi_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly InterfaceEmployeeService EmployeeService;
        private readonly ILogger<EmployeeController> _logger;


        #region "Constructor init"
        public EmployeeController(InterfaceEmployeeService EmployeeService, ILogger<EmployeeController> logger)
        {
            _logger = logger;
            _logger.LogInformation("Employee Added");
            this.EmployeeService = EmployeeService;
        }
        #endregion

        #region "View Employee Leave Status"
        [HttpGet(nameof(GetAllLeaveType))]
        public ActionResult GetAllLeaveType()
        {
            try 
            {
                var LeaveDetail = EmployeeService.GetAllLeaveType();
                if (LeaveDetail != null)
                {
                    return Ok(LeaveDetail);
                }
            }
            
             catch (Exception e)
            {
                _logger.LogError("Exception Occured", e.InnerException);
            }
            return BadRequest("Not found");
        }
        #endregion

        #region "Apply Planned Leaves"
        [HttpPost(nameof(ApplyPLeave))]
        public ActionResult ApplyPLeave(ApplyPlannedLeave applyPlannedLeave)
        {
            try
            {
                EmployeeService.ApplyPL(applyPlannedLeave);

                return Ok("Leave Applied Successully");
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occured", e.InnerException);
            }
            return BadRequest("Not Found");

        }
        #endregion
        
        #region "Cancel Planned Leaves"
        [HttpPut(nameof(CancelPlannedLeave))]
        public ActionResult CancelPlannedLeave(int EmpId)
        {
            try
            {
                EmployeeService.DeletePLeave(EmpId);

                return Ok("Leave Cancelled");

            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occured", e.InnerException);
                return BadRequest();
            }
            
        }
        #endregion
    }
}

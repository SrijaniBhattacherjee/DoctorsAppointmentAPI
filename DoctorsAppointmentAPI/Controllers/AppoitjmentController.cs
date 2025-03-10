using DoctorsAppointmentAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DoctorsAppointmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("allowCors")]
    public class AppoitjmentController : ControllerBase
    {
        private readonly AppointmentDbContext _context;
        private readonly IActionResult list;
        private readonly object? data;
        private readonly object? pat;

        public AppoitjmentController(AppointmentDbContext context)
        {
            _context = context;
        }

        [Route("Api/CreateNewAppointment")]
        [HttpPost]
        public IActionResult CreateNewAppointment(NewAppointmentModel appmodel) {

            try
            {
                var isPatientExist = _context.Patients.SingleOrDefault(m => m.Phone == appmodel.Phone);

                if (isPatientExist == null)
                {
                    Patient patient = new Patient()
                    {
                        Phone = appmodel.Phone,
                        Address = appmodel.Address,
                        City = appmodel.City,
                        Email = appmodel.Email,
                        PatientName = appmodel.PatientName

                    };

                    _context.Patients.Add(patient);
                    _context.SaveChanges();

                    Appointment appointment = new Appointment()
                    {
                        PatientId = patient.PatientId,
                        appointmentDate = appmodel.appointmentDate,
                        isDone = false

                    };
                    _context.Appointments.Add(appointment);
                    _context.SaveChanges();
                }
                else
                {
                    Appointment appointment = new Appointment()
                    {
                        PatientId = isPatientExist.PatientId,
                        appointmentDate = appmodel.appointmentDate,
                        isDone = false
                    };
                    _context.Appointments.Add(appointment);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine("There is an exception occured");
                Console.WriteLine("Exception Occured :" , ex.Message);
            }

            return Ok("Appointment Created");

        }


        [Route("Api/GetAllAppointments")]
        [HttpGet]
        public IActionResult GetAllAppointments()
        {
            
            try
            {
                var appointments = _context.Appointments.Any();

                if (appointments)
                {
                    var list = (from app in _context.Appointments
                                join
                                pat in _context.Patients on app.PatientId equals pat.PatientId
                                select new
                                {
                                    patientName = pat.PatientName,
                                    phone = pat.Phone,
                                    email = pat.Email,
                                    address = pat.Address,
                                    city = pat.City,
                                    appointmentdate = app.appointmentDate,
                                    appointmentId = app.AppointmentId
                                }).ToList().OrderBy(x => x.appointmentdate);

                    return Ok(list);
                }
                else
                {
                    return Ok("There are no records !");
                }
                
            }
            catch(Exception ex)
            {
                Console.WriteLine("There is an exception occured");
                Console.WriteLine("Exception Occured :", ex.Message);
            }
            return Ok(list);
        }

        [Route("Api/GetDoneAppointments")]
        [HttpGet]
        public IActionResult GetDoneAppointments()
        {
            var list = (from app in _context.Appointments
                        join
                        pat in _context.Patients on app.PatientId equals pat.PatientId
                        where app.isDone == true
                        select new
                        {
                            patientName = pat.PatientName,
                            phone = pat.Phone,
                            email = pat.Email,
                            appointmentdate = app.appointmentDate,
                            appointmentId = app.AppointmentId
                        }).ToList().OrderBy(x => x.appointmentdate);

            return Ok(list);
        }

        [Route("Api/GetNewAppointments")]
        [HttpGet]
        public IActionResult GetNewAppointments()
        {
            var list = (from app in _context.Appointments
                        join
                        pat in _context.Patients on app.PatientId equals pat.PatientId
                        where app.isDone == false
                        select new
                        {
                            patientName = pat.PatientName,
                            phone = pat.Phone,
                            email = pat.Email,
                            appointmentdate = app.appointmentDate,
                            appointmentId = app.AppointmentId
                        }).ToList().OrderBy(x => x.appointmentdate);

            if ((list).IsNullOrEmpty())
            {
               return Ok("No new appointments !");
            }

            return Ok(list);
        }

        [Route("Api/ChangeStatus")]
        [HttpPost]
        public IActionResult ChangeStatus(int appointmentid) 
        {
            try
            {
                if (appointmentid != 0)
                {
                    var data = _context.Appointments.SingleOrDefault(x => x.AppointmentId == appointmentid);
                    if(data != null)
                    {
                        data.isDone = true;
                    }
                    _context.SaveChanges();
                    return Ok(data);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("There is an exception occured");
                Console.WriteLine("Exception Occured :", ex.Message);
            }
            return Ok(data);

        }

        [Route("Api/GetAllPatients")]
        [HttpGet]
        public IActionResult GetAllPatients()
        {
            
            try
            {
                var pat = _context.Patients.ToList();
                return Ok(pat);

            }
            catch(Exception ex)
            {

            }
            return Ok(pat);
        }
    }
}

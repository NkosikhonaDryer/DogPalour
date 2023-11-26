using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DogPalour.Models;

namespace DogPalour.Controllers
{
    public class EmployeesController : Controller
    {
        private DogPalourContext db = new DogPalourContext();
        [HttpGet]
        public ActionResult EmployeeHome()
        {
            return View();
        }
        // GET: Employees
        public ActionResult Index()
        { 
            var Employees = db.Employees.Where(a => a.EmpStatus == "Active").ToList();
            foreach(var item in Employees)
            {
                if(item.ProfileImage == null)
                {
                    item.ProfileImage = "~/ProfileImages/DefaultImageDog.jpg";
                }
            }

            return View(Employees);
        }
        //searchin for employess
        [HttpPost]
        public ActionResult SeachEmployee(string search)
        {

            var Employess = db.Employees.ToList();
            if(search =="")
            {
                TempData["IndexInfo1"] = "Your search was empty";
                return RedirectToAction("Index");
            }
            else
            {
                //bookingslist.Where(x => x.CustomerName.StartsWith(search)
                var empEmail = db.Employees.Where(a => a.EmailAddres.Contains(search)).ToList();
                var EmpName = db.Employees.Where(a => a.FirstName.Contains(search)).ToList();
               // var empType = db.Employees.Where(a => a.ServiceOffer.Contains(search)).ToList();
              /*  if (empType.Count() > 0)
                {
                    foreach (var item in empType)
                    {
                        if (item.ProfileImage == null)
                        {
                            item.ProfileImage = "~/ProfileImages/DefaultImageDog.jpg";
                        }
                    }
                    Employess = empType;
                    TempData["IndexInfo1"] = "#Results found: " + empType.Count();
                }*/
                if (empEmail.Count() > 0)
                {
                    foreach (var item in empEmail)
                    {
                        if (item.ProfileImage == null)
                        {
                            item.ProfileImage = "~/ProfileImages/DefaultImageDog.jpg";
                        }
                    }
                    Employess = empEmail;
                    TempData["IndexInfo1"] = "#Results found: " + empEmail.Count();
                }
                if(0 < EmpName.Count())
                {
                    foreach (var item in EmpName)
                    {
                        if (item.ProfileImage == null)
                        {
                            item.ProfileImage = "~/ProfileImages/DefaultImageDog.jpg";
                        }
                    }
                    Employess = EmpName;
                    TempData["IndexInfo1"] = "#Results found: " + EmpName.Count();
                }
                if(empEmail.Count() ==0 && EmpName.Count()== 0 /*&& empType.Count() ==0*/)
                {
                    TempData["IndexInfo1"] = search + " does not match anything on our database";
                    return RedirectToAction("Index");
                }
                if(empEmail.Count() > 0 && EmpName.Count() > 0)
                {



                    Employess = db.Employees.Where(a => a.EmailAddres.Contains(search) && a.FirstName.Contains(search)).ToList();
                    foreach (var item in Employess)
                    {
                        if (item.ProfileImage == null)
                        {
                            item.ProfileImage = "~/ProfileImages/DefaultImageDog.jpg";
                        }
                    }
                    TempData["IndexInfo1"] = "#Results found: " + Employess.Count();

                }
                if(empEmail.Count() > 0 /*&& empType.Count() > 0*/)
                {
                    Employess = db.Employees.Where(a => a.EmailAddres.Contains(search) /*&& a.ServiceOffer.Contains(search)*/).ToList();
                    foreach (var item in Employess)
                    {
                        if (item.ProfileImage == null)
                        {
                            item.ProfileImage = "~/ProfileImages/DefaultImageDog.jpg";
                        }
                    }
                    TempData["IndexInfo1"] = "#Results found: " + Employess.Count();
                }
                if(/*empType.Count() > 0 &&*/ EmpName.Count() > 0)
                {
                    Employess = db.Employees.Where(a =>/* a.ServiceOffer.Contains(search) &&*/ a.FirstName.Contains(search)).ToList();
                    foreach (var item in Employess)
                    {
                        if (item.ProfileImage == null)
                        {
                            item.ProfileImage = "~/ProfileImages/DefaultImageDog.jpg";
                        }
                    }
                    TempData["IndexInfo1"] = "#Results found: " + Employess.Count();
                }


            }
            return View("Index", Employess);
        }

        // GET: Employees/Details/5
        public ActionResult Details(string email)
        {
            //Admin side
            if (email == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(email);
            if (employee == null)
            {
                return HttpNotFound();
            }
            if(employee.ProfileImage == null)
            {
                employee.ProfileImage = "~/ProfileImages/DefaultImageDog.jpg";
            }

            //Checking for the skills in or to alert the user to add them
            var skills = db.EmployeeSkills.Where(a => a.EmailAddres == email).ToList();
            if(skills.Count() == 0)
            {
                if(TempData["StaffSuccess"] == null)
                {
                    TempData["StaffSuccess"] = "Please add the add the skill(s) the staff member offers ";
                }
            }
            foreach (var item in skills) 
            {
                if(item.MarketImage == null)
                {
                    item.MarketImage = "~/DefaultImgaes/default-image.jpg";
                   
                    
                }
            }
            //getting the services offered by DogPalour for the employee to match his or her skills 
            var service = db.Services.ToList();
            foreach (var item in service)
            {
                var sk = db.EmployeeSkills.Where(a => a.JobOffer == item.ServiceType && a.EmailAddres == employee.EmailAddres).ToList();
                if(sk.Count() >0)
                {
                    item.AddState = "Added";
                }
                else if(sk.Count() == 0)
                {
                    item.AddState = null;
                }
                if(item.picPath == null)
                {
                    item.picPath = "~/DefaultImgaes/default-image.jpg";
                    
                }
            }
            ViewBag.services = service;
            ViewBag.skills = skills;
            ViewBag.Num = 1;
            if(TempData["Num"] != null)
            {
                ViewBag.Num = int.Parse(TempData["Num"].ToString());
            }
            //getting Assignment of the staff member
            var dogService = db.DogServices.Where(a => a.EmailAddres == employee.EmailAddres).ToList();
            var PendingFinish = dogService.ToList();
            var Tasks = (from DogServ in PendingFinish
                         join Dog in db.Dog1s
                         on DogServ.DogId equals Dog.id
                         join bookings in db.Bookings
                         on Dog.BookingID equals bookings.BookingId
                         select new EmployeeTaskVM
                         {
                             BookingId = bookings.BookingId,
                             DogId = Dog.id,
                             DogServiceId = DogServ.id,
                             DogName = Dog.Name,
                             SeviceNeeded = DogServ.ServiceType,
                             BookingDate = bookings.BookingDate,
                             CompleteState = DogServ.CompletionState
                         }).ToList();
            foreach(var item in Tasks)
            {
                if(item.CompleteState == null)
                {
                    item.CompleteState = "Pending";                                                                        
                }
            }
            ViewBag.Tasks = Tasks.Where(a => a.CompleteState == "Pending");
            ViewBag.TasksComplete = Tasks.Where(a => a.CompleteState == "Completed");
            return View(employee);
        }
        //Admim Editing the Staff member Details
        [HttpPost]
        public ActionResult EditEmployee(HttpPostedFileBase ProfileImage, string EmpEmail, string IdNumber, string FirstName, string Surname,string Gender,string EmailAddres,string PhoneNumber,string Address,bool ResetPassword)
        {
         
            int changeCount = 0;
            Employee employee = db.Employees.Find(EmpEmail);
            if(IdNumber != "")
            {
                employee.IdNumber = IdNumber;
                changeCount += 1;
            }
            if(FirstName != "")
            {
                employee.FirstName = FirstName;
                changeCount += 1;
            }
            if(Surname != "")
            {
                employee.Surname = Surname;
                changeCount += 1;
            }
            if(Gender != "")
            {
                employee.Gender = Gender;
                changeCount += 1;
            }
            if(EmailAddres != "")
            {
                employee.EmailAddres = EmailAddres;
                changeCount += 1;
            }
            if(PhoneNumber != "")
            {
                employee.PhoneNumber = PhoneNumber;
                changeCount += 1;
            }
            if(Address != "")
            {
                employee.Address = Address;
                changeCount += 1;
            }
            //******Saving Album Cover image on the folder "AlbumCover" and on the database variable "CoverPath"*** 
            if (ProfileImage != null)
            {
                string fileName = Path.GetFileName(ProfileImage.FileName);
                string extection = Path.GetExtension(ProfileImage.FileName);
                // fileName += extection;
                string name = fileName;
                employee.ProfileImage = "~/ProfileImages/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/ProfileImages/"), fileName);
                ProfileImage.SaveAs(fileName);
                changeCount += 1;

            }
            //******Saving Album Cover image on the folder "AlbumCover" and on the database variable "CoverPath"***
            if (ResetPassword == true)
            {
                changeCount += 1;
                string TempPassword = "Dog" + employee.IdNumber.Substring(0, 8);
                employee.Password = TempPassword;
               
            }
            //saving changes is they were made
            if(changeCount > 0)
            {
                employee.DateCreated = DateTime.Now;

                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                //sending update email
                HandleEmails handleEmails = new HandleEmails();
                handleEmails.UpdateStaffNot(employee.EmailAddres, ResetPassword);
                TempData["StaffSuccess"] = "Account updated successfully";
            }
            else
            {
                TempData["StaffSuccess"] = "There are no changes made";
            }

            TempData["Num"] = "3";
            //sending email for notice to the update to staff member
            return RedirectToAction("Details", "Employees", new { email = EmpEmail});
        }
        //Eployee side of details and work information
        public ActionResult MyDetails()
        {
            string email = "";
            try
            {
                email = Session["EmpEmail"].ToString();
            }
            catch(Exception)
            {
                TempData["LoginError"] = "Your session has ended please login again";
                return RedirectToAction("Login", "Home");
            }
            if (email == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(email);
            if (employee == null)
            {
                return HttpNotFound();
            }
            if (employee.ProfileImage == null)
            {
                employee.ProfileImage = "~/ProfileImages/DefaultImageDog.jpg";
            }

            //Checking for the skills in or to alert the user to add them
            var skills = db.EmployeeSkills.Where(a => a.EmailAddres == email).ToList();
            if (skills.Count() == 0)
            {
                if (TempData["StaffSuccess"] == null)
                {
                    TempData["StaffSuccess"] = "Please add the add the skill(s) the staff member offers ";
                }
            }
            foreach (var item in skills)
            {
                if (item.MarketImage == null)
                {
                    item.MarketImage = "~/DefaultImgaes/default-image.jpg";


                }
            }
            //getting the services offered by DogPalour for the employee to match his or her skills 
           
            ViewBag.skills = skills;
            ViewBag.Num = 1;
            if (TempData["Num"] != null)
            {
                ViewBag.Num = int.Parse(TempData["Num"].ToString());
            }
            //getting Assignment of the staff member
            var dogService = db.DogServices.Where(a => a.EmailAddres == employee.EmailAddres).ToList();
            var PendingFinish = dogService.ToList();
            var Tasks = (from DogServ in PendingFinish
                         join Dog in db.Dog1s
                         on DogServ.DogId equals Dog.id
                         join bookings in db.Bookings
                         on Dog.BookingID equals bookings.BookingId
                         select new EmployeeTaskVM
                         {
                             BookingId = bookings.BookingId,
                             DogId = Dog.id,
                             DogServiceId = DogServ.id,
                             DogName = Dog.Name,
                             SeviceNeeded = DogServ.ServiceType,
                             BookingDate = bookings.BookingDate,
                             CompleteState = DogServ.CompletionState
                         }).ToList();
            foreach (var item in Tasks)
            {
                if (item.CompleteState == null)
                {
                    
                    item.CompleteState = "Pending";
                }
            }
            ViewBag.Tasks = Tasks.Where(a => a.CompleteState == "Pending");
            ViewBag.TasksComplete = Tasks.Where(a => a.CompleteState == "Completed");
            return View(employee);
        }
        // GET: Employees/Create
        public ActionResult Create()
        {
            var services = db.Services.ToList();
            ViewBag.Services = services ;
            // don't forget to go the creation of services if there is not one available because you can not employ someone for nothing
           
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdNumber,FirstName,Surname,Gender,EmailAddres,PhoneNumber,Address,ServiceOffer,EmpStatus")] Employee employee)
        {
            var EStaff = db.Employees.Where(a => a.EmailAddres == employee.EmailAddres).ToList();
            var customers = db.Customers.Where(a => a.EmailAddress == employee.EmailAddres).ToList();
            var admins = db.Admins.Where(a => a.EmailAdddress == employee.EmailAddres).ToList();
            if (EStaff.Count() == 0 && customers.Count() == 0 && admins.Count() == 0)
            {
               
                
                        //Adding information to datbase

                        employee.DateCreated = DateTime.Now;
                        string TempPassword = "Dog" + employee.IdNumber.Substring(0, 5);
                        employee.Password = TempPassword;
                        employee.EmpStatus = "Active";
                        db.Employees.Add(employee);
                        db.SaveChanges();
                        //sending Email to the new added staff member
                        HandleEmails handleEmails = new HandleEmails();
                        handleEmails.NewStaffNot(employee.EmailAddres);
                        TempData["StaffSuccess"] = "Staff member add succefully";
                        return RedirectToAction("Details", "Employees", new { email = employee.EmailAddres });

               
              
               
            }
            else
            {
               
                ViewBag.StaffError = "An account with similar email address has already been added below is the details";

            }

            return View(employee);
        }
        //Adding the skills of the employee on Admin side
        [HttpPost]
        public ActionResult AddSkill(int ServiceId,string EmpEmail)
        {
            EmployeeSkills employeeSkills = new EmployeeSkills();
            Employee employee = db.Employees.Find(EmpEmail);
            Service service = db.Services.Find(ServiceId);
          
            var skills = db.EmployeeSkills.Where(a => a.EmailAddres == EmpEmail).ToList();
           
            if (skills.Where(a => a.JobOffer == service.ServiceType).Count() == 0)
            {
                employeeSkills.EmailAddres = employee.EmailAddres;
                employeeSkills.JobOffer = service.ServiceType;
                employeeSkills.MarketImage = service.picPath;
                db.EmployeeSkills.Add(employeeSkills);
                TempData["Num"] = "2";
                TempData["StaffSuccess"] = employeeSkills.JobOffer+" added successfully";
                db.SaveChanges();
            }
            else
            {
                TempData["StaffSuccess"] = employeeSkills.JobOffer + " service type was already been added";
            }
           

            return RedirectToAction("Details", "Employees", new { email = employee.EmailAddres });
        }
        //romoving skill from the Staff memeber offered skills
        [HttpGet]
        public ActionResult RemoveSkill(int IdSkill)
        {
            EmployeeSkills employeeSkills = db.EmployeeSkills.Find(IdSkill);
            Employee employee = db.Employees.Find(employeeSkills.EmailAddres);
            TempData["StaffSuccess"] = employeeSkills.JobOffer + " Removed successfully";
            TempData["Num"] = "2";
            db.EmployeeSkills.Remove(employeeSkills);
            db.SaveChanges();
            return RedirectToAction("Details", "Employees", new { email = employee.EmailAddres });
        }
        // GET: Employees/Edit/5
        public ActionResult Edit(string id)
        {
            //Employee side
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase ProfileImage, string EmpEmail, string IdNumber, string FirstName, string Surname, string Gender, string EmailAddres, string PhoneNumber, string Address, string Password, string NewPassword,string ConfirmPassword)
        {
            bool ChangPassword = false;
            int changeCount = 0;
            Employee employee = db.Employees.Find(EmpEmail);
            if (IdNumber != "")
            {
                employee.IdNumber = IdNumber;
                changeCount += 1;
            }
            if (FirstName != "")
            {
                employee.FirstName = FirstName;
                changeCount += 1;
            }
            if (Surname != "")
            {
                employee.Surname = Surname;
                changeCount += 1;
            }
            if (Gender != "")
            {
                employee.Gender = Gender;
                changeCount += 1;
            }
            if (EmailAddres != "")
            {
                employee.EmailAddres = EmailAddres;
                changeCount += 1;
            }
            if (PhoneNumber != "")
            {
                employee.PhoneNumber = PhoneNumber;
                changeCount += 1;
            }
            if (Address != "")
            {
                employee.Address = Address;
                changeCount += 1;
            }
           
            if(NewPassword != "")
            {
                if (Password != employee.Password)
                {
                    TempData["Num"] = "3";
                    TempData["StaffSuccess"] = "The current password must be vailid in order for the password to be updated";
                    return RedirectToAction("MyDetails", "Employees", new { email = EmpEmail });
                }
                else
                {
                    if(NewPassword != ConfirmPassword)
                    {
                        TempData["Num"] = "3";
                        TempData["StaffSuccess"] = "The new password does not match the confirmed password";
                        return RedirectToAction("MyDetails", "Employees", new { email = EmpEmail });
                    }
                    else if(NewPassword == ConfirmPassword)
                    {
                        employee.Password = NewPassword;
                        changeCount += 1;
                        ChangPassword = true;
                    }
                }
            }
            
            //******Saving Album Cover image on the folder "AlbumCover" and on the database variable "CoverPath"*** 
            if (ProfileImage != null)
            {
                string fileName = Path.GetFileName(ProfileImage.FileName);
                string extection = Path.GetExtension(ProfileImage.FileName);
                // fileName += extection;
                string name = fileName;
                employee.ProfileImage = "~/ProfileImages/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/ProfileImages/"), fileName);
                ProfileImage.SaveAs(fileName);
                changeCount += 1;

            }
            //******Saving Album Cover image on the folder "AlbumCover" and on the database variable "CoverPath"***
           
            //saving changes is they were made
            if (changeCount > 0)
            {
                employee.DateCreated = DateTime.Now;

                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                //sending email for notice to the update to staff member
                HandleEmails handleEmails = new HandleEmails();
                handleEmails.UpdateStaffNot(employee.EmailAddres, ChangPassword);
                TempData["StaffSuccess"] = "Account updated successfully";
            }
            else
            {
                TempData["StaffSuccess"] = "There are no changes made";
            }

            TempData["Num"] = "3";
            
            return RedirectToAction("MyDetails", "Employees", new { email = EmpEmail });
           
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Assigning task to the employees
        public ActionResult TaskAssign(string Servtype,int DogSerId,int DogId)
        {
            //  var DogService = db.DogServices.Where(a => a. == BookingId).ToList();
            ViewBag.DogSerId = DogSerId;
            ViewBag.DogId = DogId;
            DogService dogService = db.DogServices.Find(DogSerId);
            var Employees = db.EmployeeSkills.Where(a => a.JobOffer == dogService.ServiceType).Include(a => a.Employee).Where(a =>a.Employee.EmpStatus == "Active").ToList();
            foreach (var item in Employees)
            {
                if (item.Employee.ProfileImage == null)
                {
                    item.Employee.ProfileImage = "~/ProfileImages/DefaultImageDog.jpg";
                }
            }
            return View(Employees);
        }
        public ActionResult Assigning(string email,int DogSerId,int DogId)
        {
            DogService dogService = db.DogServices.Find(DogSerId);
            Employee employee = db.Employees.Find(email);
            dogService.EmailAddres = employee.EmailAddres;
            dogService.AssgnState = "Assigned";
            Dog1 dog1 = db.Dog1s.Where(a => a.id == DogId).SingleOrDefault();
            db.Entry(dogService).State = EntityState.Modified;
            db.SaveChanges();
            //send email to alert staff member about the their tasks
            HandleEmails handleEmails = new HandleEmails();
           
            handleEmails.TaskStaffNot(employee.EmailAddres, dog1.BookingID, dogService.id);
            TempData["AssignInfo"] = "Task successfully assined to " + employee.FirstName + " " + employee.Surname;
            return RedirectToAction("ADetails", "Dog1", new { Did = dogService.DogId });
        }
       
        //getting employees by service in order to choose the asssined ones
        public ActionResult EmpServ(int servID)
        {
            DogService dogService = db.DogServices.Find(servID);
            Service service = db.Services.Find(dogService.ServiceID);
            var employee = db.Employees.Where(a => a.EmployeeSkills.Single().JobOffer == service.ServiceType && a.EmpStatus == "Active").ToList();
            foreach (var item in employee)
            {
                if (item.ProfileImage == null)
                {
                    item.ProfileImage = "~/ProfileImages/DefaultImageDog.jpg";
                }
            }
            return View(employee);
        }
        //emplyee finishing task
        public ActionResult FinishTask(int dogServId)
        {
            DogService dogService = db.DogServices.Find(dogServId);
            dogService.CompletionState = "Completed";
            db.Entry(dogService).State = EntityState.Modified; ;
            db.SaveChanges();
            Dog1 dog1 = db.Dog1s.Find(dogService.DogId);
            var Dogs = db.Dog1s.Where(a => a.BookingID == dog1.BookingID).ToList();
            int count = 0;
            foreach(var item in Dogs)
            {
                var dgServ = db.DogServices.Where(a => a.DogId == item.id).ToList();
                if(dgServ.Where(a => a.CompletionState == "Completed").ToList().Count() == dgServ.Count())
                {
                    count += 1;
                }
            }
            if(count == Dogs.Count())
            {
                Booking booking = db.Bookings.Find(dog1.BookingID);
                booking.state = "Attended";
                db.Entry(booking).State = EntityState.Modified;
                HandleEmails handleEmails = new HandleEmails();
                handleEmails.DoneBookingNote(booking.EmailAddress);
            }
            TempData["StaffSuccess"] = "Activity completed successfully";
            TempData["Num"] = "4";
            return RedirectToAction("MyDetails", "Employees");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

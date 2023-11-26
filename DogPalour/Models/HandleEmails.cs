using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;


namespace DogPalour.Models
{
   
    public class HandleEmails
    {
        private DogPalourContext db = new DogPalourContext();
        public string AccountEmail(string IDnumber)
        {
            try
            {
                Customer customer = db.Customers.Find(IDnumber);

                string mailBody = "Dear "+customer.Firstname+ "<p> you have successfully created your account to update" +
                    "or remove account pleas login using your details</p> ";
                string senderEmail = System.Web.Configuration.WebConfigurationManager.AppSettings["SenderEmail"].ToString();
                string senderPassword = System.Web.Configuration.WebConfigurationManager.AppSettings["SenderPassword"].ToString();
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

                client.Timeout = 100000;
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                MailMessage mailM = new MailMessage(senderEmail, customer.EmailAddress, "New Booking", mailBody);
                mailM.IsBodyHtml = true;
                mailM.BodyEncoding = Encoding.UTF8;
                client.EnableSsl = true;
                client.Send(mailM);
                return "Email sent";
            }
            catch (Exception)
            {
                return "Email not sent";
            }
           
        }

        //booking email
        public string BookingEmailToCustomer(int bookId)
        {
            try
            {
                Booking booking = db.Bookings.Find(bookId);
                Customer customer = db.Customers.Find(booking.CustomerIdnumber);

                string mailBody = "Dear " + customer.Firstname + "<p> you have successfully created your booking scheduled to be on " + booking.BookingDate +
                    " for more details on the booking login to your Dog Palour account and secure with payment after admin has confirmed yopur booking";
                string senderEmail = System.Web.Configuration.WebConfigurationManager.AppSettings["SenderEmail"].ToString();
                string senderPassword = System.Web.Configuration.WebConfigurationManager.AppSettings["SenderPassword"].ToString();
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

                client.Timeout = 100000;
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                MailMessage mailM = new MailMessage(senderEmail, customer.EmailAddress, "New Booking", mailBody);
                mailM.IsBodyHtml = true;
                mailM.BodyEncoding = Encoding.UTF8;
                client.EnableSsl = true;
                client.Send(mailM);
                return "Email sent";
            }
            catch (Exception)
            {
                return "Email not sent";
            }

        }


        public string AcceptBookingToCustomer(int bookId)
        {
            try
            {
                Booking booking = db.Bookings.Find(bookId);
                Customer customer = db.Customers.Find(booking.CustomerIdnumber);

                string mailBody = "Dear " + customer.Firstname + "<p> you have a successfully cornfirmed booking by the Admin, booking scheduled to be on " + booking.BookingDate +
                    "<br/><b>Payment is required</b> for more details on the booking login to your Dog Palour account and secure with payment after admin has confirmed yopur booking<br/>" +
                    "<h3><b>Do not reply to this email</b></h3>";
                string senderEmail = System.Web.Configuration.WebConfigurationManager.AppSettings["SenderEmail"].ToString();
                string senderPassword = System.Web.Configuration.WebConfigurationManager.AppSettings["SenderPassword"].ToString();
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

                client.Timeout = 100000;
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                MailMessage mailM = new MailMessage(senderEmail, customer.EmailAddress, "Booking update", mailBody);
                mailM.IsBodyHtml = true;
                mailM.BodyEncoding = Encoding.UTF8;
                client.EnableSsl = true;
                client.Send(mailM);
                return "Email sent";
            }
            catch (Exception)
            {
                return "Email not sent";
            }

        }

        public string DeclineBookingToCustomer(int bookId,string Reason)
        {
            try
            {
                Booking booking = db.Bookings.Find(bookId);
                Customer customer = db.Customers.Find(booking.CustomerIdnumber);

                string mailBody = "Dear " + customer.Firstname + "<br/>" +
                    "We regret to inform you that you booking with Dog Palour is Unsuccessfull<br/>" +
                    "<h3>Reeson</h3><br/>" + Reason+"<br/>" +
                    "<b>Do not reply to this email<b/>"
                    ;
                string senderEmail = System.Web.Configuration.WebConfigurationManager.AppSettings["SenderEmail"].ToString();
                string senderPassword = System.Web.Configuration.WebConfigurationManager.AppSettings["SenderPassword"].ToString();
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

                client.Timeout = 100000;
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                MailMessage mailM = new MailMessage(senderEmail, customer.EmailAddress, "Booking update", mailBody);
                mailM.IsBodyHtml = true;
                mailM.BodyEncoding = Encoding.UTF8;
                client.EnableSsl = true;
                client.Send(mailM);
                return "Email sent";
            }
            catch (Exception)
            {
                return "Email not sent";
            }

        }

        public string BookingEmailToAdmin(int bookId)
        {
            try
            {
                Booking booking = db.Bookings.Find(bookId);
                Customer customer = db.Customers.Find(booking.CustomerIdnumber);

                string mailBody = "Dear Administration one new booking has been created scheduled to be on " + booking.BookingDate +
                    "to accept or decline the booking and for more details on the booking login to your Dog Palour account";
                string senderEmail = System.Web.Configuration.WebConfigurationManager.AppSettings["SenderEmail"].ToString();
                string senderPassword = System.Web.Configuration.WebConfigurationManager.AppSettings["SenderPassword"].ToString();
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

                client.Timeout = 100000;
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                MailMessage mailM = new MailMessage(senderEmail, customer.EmailAddress, "New Booking", mailBody);
                mailM.IsBodyHtml = true;
                mailM.BodyEncoding = Encoding.UTF8;
                client.EnableSsl = true;
                client.Send(mailM);
                return "Email sent";
            }
            catch (Exception)
            {
                return "Email not sent";
            }

        }

        public string CancelBookingToCustomer(int bookId)
        {
            try
            {
                Booking booking = db.Bookings.Find(bookId);
                Customer customer = db.Customers.Find(booking.CustomerIdnumber);

                string mailBody = "Dear " + customer.Firstname + "<p> you have Canceled your booking that was scheduled to be on " + booking.BookingDate +
                    "<br/> for more details on the booking login to your Dog Palour account <br/>" +
                    "<h3><b>Do not reply to this email</b></h3>";
                string senderEmail = System.Web.Configuration.WebConfigurationManager.AppSettings["SenderEmail"].ToString();
                string senderPassword = System.Web.Configuration.WebConfigurationManager.AppSettings["SenderPassword"].ToString();
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

                client.Timeout = 100000;
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                MailMessage mailM = new MailMessage(senderEmail, customer.EmailAddress, "Booking update", mailBody);
                mailM.IsBodyHtml = true;
                mailM.BodyEncoding = Encoding.UTF8;
                client.EnableSsl = true;
                client.Send(mailM);
                return "Email sent";
            }
            catch (Exception)
            {
                return "Email not sent";
            }

        }

        public string CancelBookingToAdmin(int bookId)
        {
            try
            {
                Booking booking = db.Bookings.Find(bookId);
                Customer customer = db.Customers.Find(booking.CustomerIdnumber);

                string mailBody = "Customer" + customer.Firstname + "<p> has Canceled the booking that was scheduled to be on " + booking.BookingDate +
                    "<br/> for more details on the booking login to your Dog Palour account <br/>" +
                    "<h3><b>Do not reply to this email</b></h3>";
                string senderEmail = System.Web.Configuration.WebConfigurationManager.AppSettings["SenderEmail"].ToString();
                string senderPassword = System.Web.Configuration.WebConfigurationManager.AppSettings["SenderPassword"].ToString();
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

                client.Timeout = 100000;
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                MailMessage mailM = new MailMessage(senderEmail, customer.EmailAddress, "Booking update", mailBody);
                mailM.IsBodyHtml = true;
                mailM.BodyEncoding = Encoding.UTF8;
                client.EnableSsl = true;
                client.Send(mailM);
                return "Email sent";
            }
            catch (Exception)
            {
                return "Email not sent";
            }

        }

        public string PayBookingToCustomer(int bookId,double amount)
        {
            try
            {
                Booking booking = db.Bookings.Find(bookId);
                Customer customer = db.Customers.Find(booking.CustomerIdnumber);

                string mailBody = "Dear " + customer.Firstname + "<br/>" +
                    "Payment was made succefully with vat calculated the total amount the was due and payed is: R" + amount + "" +
                    "<b>Do not reply to this email</b>";
                   
                string senderEmail = System.Web.Configuration.WebConfigurationManager.AppSettings["SenderEmail"].ToString();
                string senderPassword = System.Web.Configuration.WebConfigurationManager.AppSettings["SenderPassword"].ToString();
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

                client.Timeout = 100000;
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                MailMessage mailM = new MailMessage(senderEmail, customer.EmailAddress, "Booking payment update", mailBody);
                mailM.IsBodyHtml = true;
                mailM.BodyEncoding = Encoding.UTF8;
                client.EnableSsl = true;
                client.Send(mailM);
                return "Email sent";
            }
            catch (Exception)
            {
                return "Email not sent";
            }

        }
        public string NewStaffNot(string StaffEmail)
        {
            Employee employee = db.Employees.Find(StaffEmail);
            try
            {

                string mailBody = "<p> Dear " +employee.FirstName+"<br/>"+
                    "You have been add to the DogPalour system as a new Staff member details are as follow<br/>" +
                    "Id number: "+employee.IdNumber+"<br/>"+
                    "First name: "+employee.FirstName+"<br/>"+
                    "Surname: "+employee.Surname+"<br/>"+
                    "Email address: "+employee.EmailAddres+"<br/>" +
                    "To make any change to your details please login to your DogPalour account using you email address<br/>" +
                    "and this <h3><b>Password: <u>"+employee.Password+"</u><b><h3> which is auto genarated and can be changed<br/>" +
                    "<hr/>" +
                    "<h2><b>Do not replay to this email as it is also generated</b></h2><br/>" +
                    "<b>Regards: DogPalour<b/>"+
                    "</p>";
                string senderEmail = System.Web.Configuration.WebConfigurationManager.AppSettings["SenderEmail"].ToString();
                string senderPassword = System.Web.Configuration.WebConfigurationManager.AppSettings["SenderPassword"].ToString();
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

                client.Timeout = 100000;
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                MailMessage mailM = new MailMessage(senderEmail,employee.EmailAddres, "Attention new staff member", mailBody);
                mailM.IsBodyHtml = true;
                mailM.BodyEncoding = Encoding.UTF8;
                client.EnableSsl = true;
                client.Send(mailM);
                return "Email sent";
            }
            catch (Exception)
            {
                return "Email not sent";
            }

        }
        //Account Update alert
        public string UpdateStaffNot(string StaffEmail,bool Password)
        {
            Employee employee = db.Employees.Find(StaffEmail);
            try
            {

                string mailBody = "<p> Dear " + employee.FirstName + "<br/>" +
                    " Your account has been Updated details are as follow<br/>" +
                    "Id number: " + employee.IdNumber + "<br/>" +
                    "First name: " + employee.FirstName + "<br/>" +
                    "Surname: " + employee.Surname + "<br/>" +
                    "Email address: " + employee.EmailAddres + "<br/>" +
                    "To make any change to your details please login to your DogPalour account using you email address<br/>" +
                    "and Password<br/>" +
                    "<hr/>" +
                    "<h2><b>Do not replay to this email as it is also generated</b></h2><br/>" +
                    "<b>Regards: DogPalour<b/>" +
                    "</p><br/>";
                if(Password == true)
                {
                    mailBody += "<p><h2><b>NOTE: </b></h2>Password has changed to <h3><b><u>" + employee.Password + "</u><b><h3></p>";
                }
                string senderEmail = System.Web.Configuration.WebConfigurationManager.AppSettings["SenderEmail"].ToString();
                string senderPassword = System.Web.Configuration.WebConfigurationManager.AppSettings["SenderPassword"].ToString();
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

                client.Timeout = 100000;
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                MailMessage mailM = new MailMessage(senderEmail, employee.EmailAddres, "Attention staff Account update", mailBody);
                mailM.IsBodyHtml = true;
                mailM.BodyEncoding = Encoding.UTF8;
                client.EnableSsl = true;
                client.Send(mailM);
                return "Email sent";
            }
            catch (Exception)
            {
                return "Email not sent";
            }

        }



         public string TaskStaffNot(string StaffEmail,int bookingId,int DogServiceId)
        {
            Booking booking = db.Bookings.Find(bookingId);

            DogService dogService = db.DogServices.Find(DogServiceId);
            Dog1 dog1 = db.Dog1s.Find(dogService.DogId);
            Employee employee = db.Employees.Find(StaffEmail);
            try
            {

                string mailBody = "<p>Dear " + employee.FirstName + "<br/>" +
                    "You have been assigned to a <b>" + dogService.ServiceType + "</b> details are as follow<br/>" +
                    "<hr/>" +
                    "Dog name: " + dog1.Name + "<br/>" +
                    "Dog age: " + dog1.Age + " <br/>" +
                    "Dog type: " + dog1.DogType + "<br/>" +
                    "Booking date: " + booking.BookingDate + "<br/>" +
                    "Service needed: " + dogService.ServiceType + "<br/>" +
                    "</p>" +
                    "<p><h3><b>Do not reply to this emailas it is auto generated</b></h3><br/>" +
                    "login to your account for more information";
                string senderEmail = System.Web.Configuration.WebConfigurationManager.AppSettings["SenderEmail"].ToString();
                string senderPassword = System.Web.Configuration.WebConfigurationManager.AppSettings["SenderPassword"].ToString();
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

                client.Timeout = 100000;
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                MailMessage mailM = new MailMessage(senderEmail, employee.EmailAddres, "Attention staff Task assignment", mailBody);
                mailM.IsBodyHtml = true;
                mailM.BodyEncoding = Encoding.UTF8;
                client.EnableSsl = true;
                client.Send(mailM);
                return "Email sent";
            }
            catch (Exception)
            {
                return "Email not sent";
            }

        }


        public string DoneBookingNote(string CustomerEmail)
        {
             
            try
            {

                string mailBody = "Dear Custer your booking has been successfully completed please come get your dogs if not already have<br/>" +
                    "<h4><b>Do not reply to this email<br/>" +
                    "regards: DogPalour</b></h4>";
                string senderEmail = System.Web.Configuration.WebConfigurationManager.AppSettings["SenderEmail"].ToString();
                string senderPassword = System.Web.Configuration.WebConfigurationManager.AppSettings["SenderPassword"].ToString();
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

                client.Timeout = 100000;
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                MailMessage mailM = new MailMessage(senderEmail, CustomerEmail, "Attention Customer", mailBody);
                mailM.IsBodyHtml = true;
                mailM.BodyEncoding = Encoding.UTF8;
                client.EnableSsl = true;
                client.Send(mailM);
                return "Email sent";
            }
            catch (Exception)
            {
                return "Email not sent";
            }

        }

    }
}
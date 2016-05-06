using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DotNetAPI.Models;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;

namespace DotNetAPI.Controllers
{
    public class BOOKINGsController : ApiController
    {
        private DBConn db = new DBConn();

        // GET: api/BOOKINGs
        public IQueryable<BOOKING> GetBOOKINGs()
        {
            return db.BOOKINGs;
        }

        // GET: api/BOOKINGs/5
        [ResponseType(typeof(BOOKING))]
        public IHttpActionResult GetBOOKING(int id)
        {
            BOOKING bOOKING = db.BOOKINGs.Find(id);
            if (bOOKING == null)
            {
                return NotFound();
            }

            return Ok(bOOKING);
        }

        // PUT: api/BOOKINGs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBOOKING(int id, BOOKING bOOKING)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bOOKING.BOOKING_ID)
            {
                return BadRequest();
            }

            db.Entry(bOOKING).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BOOKINGExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = bOOKING.BOOKING_ID }, db.BOOKINGs.Find(bOOKING.BOOKING_ID)); ;
        }

        // POST: api/BOOKINGs
        [ResponseType(typeof(BOOKING))]
        public IHttpActionResult PostBOOKING(BOOKING bOOKING)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bOOKING.BOOKING_ID = db.ADD_BOOKING(bOOKING.BOOKING_ID, bOOKING.TICKET_ID, bOOKING.ORDER_ID, bOOKING.BOOKING_QUANTITY, bOOKING.BOOKING_DATE_TIME);

            ORDER order = db.ORDERS.Where(o => o.ORDER_ID == bOOKING.ORDER_ID).FirstOrDefault();
            CUSTOMER customer = db.ORDERS.Where(o => o.ORDER_ID == bOOKING.ORDER_ID).Select(o => o.CUSTOMER).FirstOrDefault();
            TICKET ticket = db.TICKETs.Where(t => t.TICKET_ID == bOOKING.TICKET_ID).FirstOrDefault();

            Thread thread = new Thread(() => sendEmail(bOOKING, customer, order, ticket));
            thread.Start();

            return CreatedAtRoute("DefaultApi", new { id = bOOKING.BOOKING_ID }, bOOKING);
        }

        private void sendEmail(BOOKING booking, CUSTOMER customer, ORDER order, TICKET ticket)
        {
            try
            {

                MailMessage m = new MailMessage();
                SmtpClient sc = new SmtpClient();

                String body;

                body = "Hi " + customer.CUSTOMER_FIRST_NAME + " " + customer.CUSTOMER_LAST_NAME + ", \n";
                body += "Thanks for booking tickets with the Function Junction. Please find your booking summary below.\n";
                body += "Booking Summary - " + booking.BOOKING_DATE_TIME + "\n";
                body += "\n";
                body += "Order ID:     " + order.ORDER_ID + "\n";
                body += "Booking ID:   " + booking.BOOKING_ID + "\n";
                body += "Ticket:       " + ticket.TICKET_TYPE + "\n";
                body += "Quantity:     " + booking.BOOKING_QUANTITY + "\n";
                body += "\n";
                body += "We hope you enjoy the event. If you have any questions, don’t hesitate to contact us at info.functionjunction@gmail.com\n";
                body += "\n";
                body += "Kind regards,\n";
                body += "The Function Junction.\n";
                body += "\n";
                body += "\n";
                body += "This email was intended for " + customer.CUSTOMER_ADDRESS + ". If this is not you, please contact us immediately at info.functionjunction@gmail.com";




                m.From = new MailAddress("info.functionjunction@gmail.com", "Function Junction");
                m.To.Add(new MailAddress(customer.CUSTOMER_EMAIL, customer.CUSTOMER_FIRST_NAME + " " + customer.CUSTOMER_LAST_NAME));
                
                m.Bcc.Add("joshua.kellaway@students.plymouth.ac.uk");

                m.Subject = "New Booking";


                m.Body = body;

                sc.Host = "smtp.gmail.com";
                sc.Port = 587;
                sc.Credentials = new NetworkCredential("info.functionjunction@gmail.com", "scottmills");
                sc.EnableSsl = true; // runtime encrypt the SMTP communications using SSL
                sc.Send(m);
            }
            catch (Exception e)
            {
            }
        }

        // DELETE: api/BOOKINGs/5
        [ResponseType(typeof(BOOKING))]
        public IHttpActionResult DeleteBOOKING(int id)
        {
            BOOKING bOOKING = db.BOOKINGs.Find(id);
            if (bOOKING == null)
            {
                return NotFound();
            }

            db.BOOKINGs.Remove(bOOKING);
            db.SaveChanges();

            return Ok(bOOKING);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BOOKINGExists(int id)
        {
            return db.BOOKINGs.Count(e => e.BOOKING_ID == id) > 0;
        }
    }
}
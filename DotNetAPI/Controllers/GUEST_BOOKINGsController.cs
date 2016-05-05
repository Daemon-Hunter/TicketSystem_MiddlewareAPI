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

namespace DotNetAPI.Controllers
{
    public class GUEST_BOOKINGsController : ApiController
    {
        private DBConn db = new DBConn();

        // GET: api/GUEST_BOOKINGs
        public IQueryable<GUEST_BOOKING> GetGUEST_BOOKING()
        {
            return db.GUEST_BOOKINGs;
        }

        // GET: api/GUEST_BOOKINGs/5
        [ResponseType(typeof(GUEST_BOOKING))]
        public IHttpActionResult GetGUEST_BOOKING(int id)
        {
            GUEST_BOOKING gUEST_BOOKING = db.GUEST_BOOKINGs.Find(id);
            if (gUEST_BOOKING == null)
            {
                return NotFound();
            }

            return Ok(gUEST_BOOKING);
        }

        // PUT: api/GUEST_BOOKINGs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGUEST_BOOKING(int id, GUEST_BOOKING gUEST_BOOKING)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gUEST_BOOKING.GUEST_BOOKING_ID)
            {
                return BadRequest();
            }

            db.Entry(gUEST_BOOKING).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GUEST_BOOKINGExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = gUEST_BOOKING.GUEST_BOOKING_ID }, db.GUEST_BOOKINGs.Find(gUEST_BOOKING.GUEST_BOOKING_ID)); ;
        }

        // POST: api/GUEST_BOOKINGs
        [ResponseType(typeof(GUEST_BOOKING))]
        public IHttpActionResult PostGUEST_BOOKING(GUEST_BOOKING gUEST_BOOKING)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!GUEST_BOOKINGExists(gUEST_BOOKING.TICKET_ID, gUEST_BOOKING.GUEST_EMAIL, gUEST_BOOKING.GUEST_ADDRESS, gUEST_BOOKING.GUEST_POSTCODE, gUEST_BOOKING.GUEST_BOOKING_QUANTITY, gUEST_BOOKING.GUEST_BOOKING_DATE_TIME))
            {

                gUEST_BOOKING.GUEST_BOOKING_ID = db.ADD_GUEST_BOOKING(gUEST_BOOKING.GUEST_BOOKING_ID, gUEST_BOOKING.TICKET_ID, gUEST_BOOKING.GUEST_EMAIL,
                    gUEST_BOOKING.GUEST_ADDRESS, gUEST_BOOKING.GUEST_POSTCODE, gUEST_BOOKING.GUEST_BOOKING_QUANTITY, gUEST_BOOKING.GUEST_BOOKING_DATE_TIME);

                return CreatedAtRoute("DefaultApi", new { id = gUEST_BOOKING.GUEST_BOOKING_ID }, gUEST_BOOKING);
            }
            return Conflict();
        }

        // DELETE: api/GUEST_BOOKINGs/5
        [ResponseType(typeof(GUEST_BOOKING))]
        public IHttpActionResult DeleteGUEST_BOOKING(int id)
        {
            GUEST_BOOKING gUEST_BOOKING = db.GUEST_BOOKINGs.Find(id);
            if (gUEST_BOOKING == null)
            {
                return NotFound();
            }

            db.GUEST_BOOKINGs.Remove(gUEST_BOOKING);
            db.SaveChanges();

            return Ok(gUEST_BOOKING);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GUEST_BOOKINGExists(int id)
        {
            return db.GUEST_BOOKINGs.Count(e => e.GUEST_BOOKING_ID == id) > 0;
        }
        private bool GUEST_BOOKINGExists(int ticketid, string email, string address, string postcode, int bookingq, DateTime dateTime)
        {
            return db.GUEST_BOOKINGs.Count(e => e.TICKET_ID == ticketid && e.GUEST_EMAIL == email && e.GUEST_ADDRESS == address && e.GUEST_POSTCODE == postcode && e.GUEST_BOOKING_QUANTITY == bookingq && e.GUEST_BOOKING_DATE_TIME == dateTime) > 0;
        }
    }
}
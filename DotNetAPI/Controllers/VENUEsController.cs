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
    public class VENUEsController : ApiController
    {
        private DBConn db = new DBConn();

        // GET: api/VENUEs
        public IQueryable<VENUE> GetVENUEs()
        {
            return db.VENUEs;
        }

        // GET: api/VENUEs/5
        [ResponseType(typeof(VENUE))]
        public IHttpActionResult GetVENUE(int id)
        {
            VENUE vENUE = db.VENUEs.Find(id);
            if (vENUE == null)
            {
                return NotFound();
            }

            return Ok(vENUE);
        }

        // PUT: api/VENUEs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVENUE(int id, VENUE vENUE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vENUE.VENUE_ID)
            {
                return BadRequest();
            }

            db.Entry(vENUE).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VENUEExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = vENUE.VENUE_ID }, db.VENUEs.Find(vENUE.VENUE_ID));
        }

        // POST: api/VENUEs
        [ResponseType(typeof(VENUE))]
        public IHttpActionResult PostVENUE(VENUE vENUE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!VENUEExists(vENUE.SOCIAL_MEDIA_ID, vENUE.VENUE_NAME))
            {
                try
                {
                    vENUE.VENUE_ID = db.ADD_VENUE(vENUE.VENUE_ID, vENUE.SOCIAL_MEDIA_ID, vENUE.VENUE_DESCRIPTION,
                        vENUE.VENUE_CAPACITY_SEATING, vENUE.VENUE_CAPACITY_STANDING, vENUE.VENUE_DISABLED_ACCESS,
                        vENUE.VENUE_FACILITES, vENUE.VENUE_PARKING, vENUE.VENUE_PHONE_NUMBER, vENUE.VENUE_EMAIL,
                        vENUE.VENUE_ADDRESS, vENUE.VENUE_POSTCODE, vENUE.VENUE_NAME, vENUE.VENUE_CITY);
                }
                catch (DbUpdateConcurrencyException e)
                {
                    return BadRequest();
                }

                return CreatedAtRoute("DefaultApi", new { id = vENUE.VENUE_ID }, vENUE);
            }

            return Conflict();
        }

        // DELETE: api/VENUEs/5
        [ResponseType(typeof(VENUE))]
        public IHttpActionResult DeleteVENUE(int id)
        {
            VENUE vENUE = db.VENUEs.Find(id);
            if (vENUE == null)
            {
                return NotFound();
            }

            db.VENUEs.Remove(vENUE);
            db.SaveChanges();

            return Ok(vENUE);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VENUEExists(int id)
        {
            return db.VENUEs.Count(e => e.VENUE_ID == id) > 0;
        }

        private bool VENUEExists(int social, string name)
        {
            return db.VENUEs.Count(e => e.SOCIAL_MEDIA_ID == social || e.VENUE_NAME == name) > 0;
        }
    }
}
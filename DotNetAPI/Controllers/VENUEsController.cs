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

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/VENUEs
        [ResponseType(typeof(VENUE))]
        public IHttpActionResult PostVENUE(VENUE vENUE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VENUEs.Add(vENUE);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (VENUEExists(vENUE.VENUE_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = vENUE.VENUE_ID }, vENUE);
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
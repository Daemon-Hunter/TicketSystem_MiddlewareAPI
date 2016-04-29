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
    public class EVENT_REVIEWsController : ApiController
    {
        private DBConn db = new DBConn();

        // GET: api/EVENT_REVIEWs
        public IQueryable<EVENT_REVIEW> GetEVENT_REVIEW()
        {
            return db.EVENT_REVIEWs;
        }

        // GET: api/EVENT_REVIEWs/5
        [ResponseType(typeof(EVENT_REVIEW))]
        public IHttpActionResult GetEVENT_REVIEW(int id)
        {
            EVENT_REVIEW eVENT_REVIEW = db.EVENT_REVIEWs.Find(id);
            if (eVENT_REVIEW == null)
            {
                return NotFound();
            }

            return Ok(eVENT_REVIEW);
        }

        // PUT: api/EVENT_REVIEWs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEVENT_REVIEW(int id, EVENT_REVIEW eVENT_REVIEW)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eVENT_REVIEW.PARENT_EVENT_ID)
            {
                return BadRequest();
            }

            db.Entry(eVENT_REVIEW).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EVENT_REVIEWExists(eVENT_REVIEW.PARENT_EVENT_ID, eVENT_REVIEW.CUSTOMER_ID))
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

        // POST: api/EVENT_REVIEWs
        [ResponseType(typeof(EVENT_REVIEW))]
        public IHttpActionResult PostEVENT_REVIEW(EVENT_REVIEW eVENT_REVIEW)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EVENT_REVIEWs.Add(eVENT_REVIEW);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EVENT_REVIEWExists(eVENT_REVIEW.PARENT_EVENT_ID, eVENT_REVIEW.CUSTOMER_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = eVENT_REVIEW.PARENT_EVENT_ID }, eVENT_REVIEW);
        }

        // DELETE: api/EVENT_REVIEWs/5
        [ResponseType(typeof(EVENT_REVIEW))]
        public IHttpActionResult DeleteEVENT_REVIEW(int id)
        {
            EVENT_REVIEW eVENT_REVIEW = db.EVENT_REVIEWs.Find(id);
            if (eVENT_REVIEW == null)
            {
                return NotFound();
            }

            db.EVENT_REVIEWs.Remove(eVENT_REVIEW);
            db.SaveChanges();

            return Ok(eVENT_REVIEW);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EVENT_REVIEWExists(int erid, int cid)
        {
            return db.EVENT_REVIEWs.Count(e => e.PARENT_EVENT_ID == erid && e.CUSTOMER_ID == cid) > 0;
        }
    }
}
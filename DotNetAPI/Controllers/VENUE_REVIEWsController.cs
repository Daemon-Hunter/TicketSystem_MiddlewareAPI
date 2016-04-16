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
    public class VENUE_REVIEWsController : ApiController
    {
        private DBConn db = new DBConn();

        // GET: api/VENUE_REVIEWs
        public IQueryable<VENUE_REVIEW> GetVENUE_REVIEW()
        {
            return db.VENUE_REVIEW;
        }

        // GET: api/VENUE_REVIEWs/5
        [ResponseType(typeof(VENUE_REVIEW))]
        public IHttpActionResult GetVENUE_REVIEW(int id)
        {
            VENUE_REVIEW vENUE_REVIEW = db.VENUE_REVIEW.Find(id);
            if (vENUE_REVIEW == null)
            {
                return NotFound();
            }

            return Ok(vENUE_REVIEW);
        }

        // PUT: api/VENUE_REVIEWs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVENUE_REVIEW(int id, VENUE_REVIEW vENUE_REVIEW)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vENUE_REVIEW.VENUE_ID)
            {
                return BadRequest();
            }

            db.Entry(vENUE_REVIEW).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VENUE_REVIEWExists(vENUE_REVIEW.VENUE_ID, vENUE_REVIEW.CUSTOMER_ID))
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

        // POST: api/VENUE_REVIEWs
        [ResponseType(typeof(VENUE_REVIEW))]
        public IHttpActionResult PostVENUE_REVIEW(VENUE_REVIEW vENUE_REVIEW)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VENUE_REVIEW.Add(vENUE_REVIEW);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (VENUE_REVIEWExists(vENUE_REVIEW.VENUE_ID, vENUE_REVIEW.CUSTOMER_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = vENUE_REVIEW.VENUE_ID }, vENUE_REVIEW);
        }

        // DELETE: api/VENUE_REVIEWs/5
        [ResponseType(typeof(VENUE_REVIEW))]
        public IHttpActionResult DeleteVENUE_REVIEW(int id)
        {
            VENUE_REVIEW vENUE_REVIEW = db.VENUE_REVIEW.Find(id);
            if (vENUE_REVIEW == null)
            {
                return NotFound();
            }

            db.VENUE_REVIEW.Remove(vENUE_REVIEW);
            db.SaveChanges();

            return Ok(vENUE_REVIEW);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VENUE_REVIEWExists(int vid, int cid)
        {
            return db.VENUE_REVIEW.Count(e => e.VENUE_ID == vid && e.CUSTOMER_ID == cid) > 0;
        }
    }
}
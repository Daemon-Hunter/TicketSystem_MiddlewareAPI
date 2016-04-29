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
    public class PARENT_EVENTsController : ApiController
    {
        private DBConn db = new DBConn();

        // GET: api/PARENT_EVENTs
        public IQueryable<PARENT_EVENT> GetPARENT_EVENT()
        {
            return db.PARENT_EVENTs;
        }

        // GET: api/PARENT_EVENTs/5
        [ResponseType(typeof(PARENT_EVENT))]
        public IHttpActionResult GetPARENT_EVENT(int id)
        {
            PARENT_EVENT pARENT_EVENT = db.PARENT_EVENTs.Find(id);
            if (pARENT_EVENT == null)
            {
                return NotFound();
            }

            return Ok(pARENT_EVENT);
        }

        // PUT: api/PARENT_EVENTs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPARENT_EVENT(int id, PARENT_EVENT pARENT_EVENT)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pARENT_EVENT.PARENT_EVENT_ID)
            {
                return BadRequest();
            }

            db.Entry(pARENT_EVENT).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PARENT_EVENTExists(id))
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

        // POST: api/PARENT_EVENTs
        [ResponseType(typeof(PARENT_EVENT))]
        public IHttpActionResult PostPARENT_EVENT(PARENT_EVENT pARENT_EVENT)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!PARENT_EVENTExists(pARENT_EVENT.SOCIAL_MEDIA_ID, pARENT_EVENT.PARENT_EVENT_NAME)) {

                pARENT_EVENT.PARENT_EVENT_ID = db.ADD_PARENT_EVENT(pARENT_EVENT.PARENT_EVENT_ID, pARENT_EVENT.SOCIAL_MEDIA_ID,
                    pARENT_EVENT.PARENT_EVENT_NAME, pARENT_EVENT.PARENT_EVENT_DESCRIPTION);

                return CreatedAtRoute("DefaultApi", new { id = pARENT_EVENT.PARENT_EVENT_ID }, pARENT_EVENT);
            }
            return Conflict();
        }

        // DELETE: api/PARENT_EVENTs/5
        [ResponseType(typeof(PARENT_EVENT))]
        public IHttpActionResult DeletePARENT_EVENT(int id)
        {
            PARENT_EVENT pARENT_EVENT = db.PARENT_EVENTs.Find(id);
            if (pARENT_EVENT == null)
            {
                return NotFound();
            }

            db.PARENT_EVENTs.Remove(pARENT_EVENT);
            db.SaveChanges();

            return Ok(pARENT_EVENT);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PARENT_EVENTExists(int id)
        {
            return db.PARENT_EVENTs.Count(e => e.PARENT_EVENT_ID == id) > 0;
        }
        private bool PARENT_EVENTExists(int mediaID, string name)
        {
            return db.PARENT_EVENTs.Count(e => e.SOCIAL_MEDIA_ID == mediaID || e.PARENT_EVENT_NAME == name) > 0;
        }
    }
}
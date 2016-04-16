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
    public class ARTIST_TYPEsController : ApiController
    {
        private DBConn db = new DBConn();

        // GET: api/ARTIST_TYPEs
        public IQueryable<ARTIST_TYPE> GetARTIST_TYPE()
        {
            return db.ARTIST_TYPE;
        }

        // GET: api/ARTIST_TYPEs/5
        [ResponseType(typeof(ARTIST_TYPE))]
        public IHttpActionResult GetARTIST_TYPE(int id)
        {
            ARTIST_TYPE aRTIST_TYPE = db.ARTIST_TYPE.Find(id);
            if (aRTIST_TYPE == null)
            {
                return NotFound();
            }

            return Ok(aRTIST_TYPE);
        }

        // PUT: api/ARTIST_TYPEs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutARTIST_TYPE(int id, ARTIST_TYPE aRTIST_TYPE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != aRTIST_TYPE.ARTIST_TYPE_ID)
            {
                return BadRequest();
            }

            db.Entry(aRTIST_TYPE).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ARTIST_TYPEExists(id))
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

        // POST: api/ARTIST_TYPEs
        [ResponseType(typeof(ARTIST_TYPE))]
        public IHttpActionResult PostARTIST_TYPE(ARTIST_TYPE aRTIST_TYPE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ARTIST_TYPE.Add(aRTIST_TYPE);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ARTIST_TYPEExists(aRTIST_TYPE.ARTIST_TYPE_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = aRTIST_TYPE.ARTIST_TYPE_ID }, aRTIST_TYPE);
        }

        // DELETE: api/ARTIST_TYPEs/5
        [ResponseType(typeof(ARTIST_TYPE))]
        public IHttpActionResult DeleteARTIST_TYPE(int id)
        {
            ARTIST_TYPE aRTIST_TYPE = db.ARTIST_TYPE.Find(id);
            if (aRTIST_TYPE == null)
            {
                return NotFound();
            }

            db.ARTIST_TYPE.Remove(aRTIST_TYPE);
            db.SaveChanges();

            return Ok(aRTIST_TYPE);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ARTIST_TYPEExists(int id)
        {
            return db.ARTIST_TYPE.Count(e => e.ARTIST_TYPE_ID == id) > 0;
        }
    }
}
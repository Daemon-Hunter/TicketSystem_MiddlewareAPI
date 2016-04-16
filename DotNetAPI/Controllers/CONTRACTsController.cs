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
    public class CONTRACTsController : ApiController
    {
        private DBConn db = new DBConn();

        // GET: api/CONTRACTs
        public IQueryable<CONTRACT> GetCONTRACTS()
        {
            return db.CONTRACTS;
        }

        // GET: api/CONTRACTs/5
        [ResponseType(typeof(CONTRACT))]
        public IHttpActionResult GetCONTRACT(int id)
        {
            CONTRACT cONTRACT = db.CONTRACTS.Find(id);
            if (cONTRACT == null)
            {
                return NotFound();
            }

            return Ok(cONTRACT);
        }

        // PUT: api/CONTRACTs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCONTRACT(int id, CONTRACT cONTRACT)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cONTRACT.ARTIST_ID)
            {
                return BadRequest();
            }

            db.Entry(cONTRACT).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CONTRACTExists(cONTRACT.ARTIST_ID, cONTRACT.CHILD_EVENT_ID))
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

        // POST: api/CONTRACTs
        [ResponseType(typeof(CONTRACT))]
        public IHttpActionResult PostCONTRACT(CONTRACT cONTRACT)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CONTRACTS.Add(cONTRACT);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CONTRACTExists(cONTRACT.ARTIST_ID, cONTRACT.CHILD_EVENT_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cONTRACT.ARTIST_ID }, cONTRACT);
        }

        // DELETE: api/CONTRACTs/5
        [ResponseType(typeof(CONTRACT))]
        public IHttpActionResult DeleteCONTRACT(int id)
        {
            CONTRACT cONTRACT = db.CONTRACTS.Find(id);
            if (cONTRACT == null)
            {
                return NotFound();
            }

            db.CONTRACTS.Remove(cONTRACT);
            db.SaveChanges();

            return Ok(cONTRACT);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CONTRACTExists(int aid, int cid)
        {
            return db.CONTRACTS.Count(e => e.ARTIST_ID == aid && e.CHILD_EVENT_ID == cid) > 0;
        }
    }
}
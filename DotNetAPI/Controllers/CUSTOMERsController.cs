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
    public class CUSTOMERsController : ApiController
    {
        private DBConn db = new DBConn();

        // GET: api/CUSTOMERs
        public IQueryable<CUSTOMER> GetCUSTOMERs()
        {
            foreach (CUSTOMER c in db.CUSTOMERs)
            {
                c.CUSTOMER_PASSWORD = "";
            }
            return db.CUSTOMERs;
        }

        // GET: api/CUSTOMERs/5
        [ResponseType(typeof(CUSTOMER))]
        public IHttpActionResult GetCUSTOMER(int id)
        {
            CUSTOMER cUSTOMER = db.CUSTOMERs.Find(id);
            if (cUSTOMER == null)
            {
                return NotFound();
            }

            cUSTOMER.CUSTOMER_PASSWORD = "";

            return Ok(cUSTOMER);
        }

        // PUT: api/CUSTOMERs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCUSTOMER(int id, CUSTOMER cUSTOMER)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cUSTOMER.CUSTOMER_ID)
            {
                return BadRequest();
            }

            db.Entry(cUSTOMER).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CUSTOMERExists(id))
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

        // POST: api/CUSTOMERs
        [ResponseType(typeof(CUSTOMER))]
        public IHttpActionResult PostCUSTOMER(CUSTOMER cUSTOMER)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!CUSTOMERExists(cUSTOMER.CUSTOMER_EMAIL))
            {

                db.CUSTOMERs.Add(cUSTOMER);

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    if (CUSTOMERExists(cUSTOMER.CUSTOMER_ID))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }

                return CreatedAtRoute("DefaultApi", new { id = cUSTOMER.CUSTOMER_ID }, cUSTOMER);
            }
            return Conflict();
        }

        // DELETE: api/CUSTOMERs/5
        [ResponseType(typeof(CUSTOMER))]
        public IHttpActionResult DeleteCUSTOMER(int id)
        {
            CUSTOMER cUSTOMER = db.CUSTOMERs.Find(id);
            if (cUSTOMER == null)
            {
                return NotFound();
            }

            db.CUSTOMERs.Remove(cUSTOMER);
            db.SaveChanges();

            return Ok(cUSTOMER);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CUSTOMERExists(int id)
        {
            return db.CUSTOMERs.Count(e => e.CUSTOMER_ID == id) > 0;
        }
        private bool CUSTOMERExists(string email)
        {
            return db.CUSTOMERs.Count(e => e.CUSTOMER_EMAIL == email) > 0;
        }
    }
}
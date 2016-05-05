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

            if (cUSTOMER.CUSTOMER_PASSWORD == "" || cUSTOMER.CUSTOMER_PASSWORD == null)
            {
                cUSTOMER.CUSTOMER_PASSWORD = db.CUSTOMERs.Find(cUSTOMER.CUSTOMER_ID).CUSTOMER_PASSWORD;
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

            return CreatedAtRoute("DefaultApi", new { id = cUSTOMER.CUSTOMER_ID }, db.CUSTOMERs.Find(cUSTOMER.CUSTOMER_ID)); ;
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

                db.ADD_CUSTOMER(cUSTOMER.CUSTOMER_ID, cUSTOMER.CUSTOMER_FIRST_NAME, cUSTOMER.CUSTOMER_LAST_NAME,
                    cUSTOMER.CUSTOMER_EMAIL, cUSTOMER.CUSTOMER_ADDRESS, cUSTOMER.CUSTOMER_POSTCODE, cUSTOMER.CUSTOMER_PASSWORD);

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
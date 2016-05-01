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
    public class ADMINsController : ApiController
    {
        private DBConn db = new DBConn();

        // GET: api/ADMINs
        public IQueryable<ADMIN> GetADMINs()
        {

            foreach(ADMIN a in db.ADMINs)
            {
                a.ADMIN_PASSWORD = "";
            }

            return db.ADMINs;
        }

        // GET: api/ADMINs/5
        [ResponseType(typeof(ADMIN))]
        public IHttpActionResult GetADMIN(int id)
        {
            ADMIN aDMIN = db.ADMINs.Find(id);
            if (aDMIN == null)
            {
                return NotFound();
            }

            aDMIN.ADMIN_PASSWORD = "";

            return Ok(aDMIN);
        }

        // PUT: api/ADMINs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutADMIN(int id, ADMIN aDMIN)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != aDMIN.ADMIN_ID)
            {
                return BadRequest();
            }

            if(aDMIN.ADMIN_PASSWORD == "" || aDMIN.ADMIN_PASSWORD == null)
            {
                aDMIN.ADMIN_PASSWORD = db.ADMINs.Find(aDMIN.ADMIN_ID).ADMIN_PASSWORD;
            }

            db.Entry(aDMIN).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ADMINExists(id))
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

        // POST: api/ADMINs
        [ResponseType(typeof(ADMIN))]
        public IHttpActionResult PostADMIN(ADMIN aDMIN)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!ADMINExists(aDMIN.ADMIN_EMAIL))
            {

                aDMIN.ADMIN_ID = db.ADD_ADMIN(aDMIN.ADMIN_ID, aDMIN.ADMIN_EMAIL, aDMIN.ADMIN_PASSWORD);

                return CreatedAtRoute("DefaultApi", new { id = aDMIN.ADMIN_ID }, aDMIN);
            }
            return Conflict();
        }

        // DELETE: api/ADMINs/5
        [ResponseType(typeof(ADMIN))]
        public IHttpActionResult DeleteADMIN(int id)
        {
            ADMIN aDMIN = db.ADMINs.Find(id);
            if (aDMIN == null)
            {
                return NotFound();
            }

            db.ADMINs.Remove(aDMIN);
            db.SaveChanges();

            return Ok(aDMIN);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ADMINExists(int id)
        {
            return db.ADMINs.Count(e => e.ADMIN_ID == id) > 0;
        }
        private bool ADMINExists(string email)
        {
            return db.ADMINs.Count(e => e.ADMIN_EMAIL == email) > 0;
        }
    }
}
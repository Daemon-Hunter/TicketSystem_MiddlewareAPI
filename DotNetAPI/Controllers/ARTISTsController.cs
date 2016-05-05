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
    public class ARTISTsController : ApiController
    {
        private DBConn db = new DBConn();

        // GET: api/ARTISTs
        public IQueryable<ARTIST> GetARTISTs()
        {
            return db.ARTISTs;
        }

        // GET: api/ARTISTs/5
        [ResponseType(typeof(ARTIST))]
        public IHttpActionResult GetARTIST(int id)
        {
            ARTIST aRTIST = db.ARTISTs.Find(id);
            if (aRTIST == null)
            {
                return NotFound();
            }

            return Ok(aRTIST);
        }

        // PUT: api/ARTISTs/5
        [ResponseType(typeof(ARTIST))]
        public IHttpActionResult PutARTIST(int id, ARTIST aRTIST)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != aRTIST.ARTIST_ID)
            {
                return BadRequest();
            }

            db.Entry(aRTIST).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ARTISTExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new {id = aRTIST.ARTIST_ID}, db.ARTISTs.Find(aRTIST.ARTIST_ID));
        }

        // POST: api/ARTISTs
        [ResponseType(typeof(ARTIST))]
        public IHttpActionResult PostARTIST(ARTIST aRTIST)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!ARTISTExists(aRTIST.ARTIST_NAME, aRTIST.SOCIAL_MEDIA_ID, aRTIST.ARTIST_TYPE_ID)) {

                db.ADD_ARTIST(aRTIST.ARTIST_ID, aRTIST.ARTIST_NAME, aRTIST.ARTIST_TAGS, aRTIST.SOCIAL_MEDIA_ID,
                    aRTIST.ARTIST_DESCRIPTION, aRTIST.ARTIST_TYPE_ID);

                return CreatedAtRoute("DefaultApi", new { id = aRTIST.ARTIST_ID }, aRTIST);
            }
            return Conflict();
        }

        // DELETE: api/ARTISTs/5
        [ResponseType(typeof(ARTIST))]
        public IHttpActionResult DeleteARTIST(int id)
        {
            ARTIST aRTIST = db.ARTISTs.Find(id);
            if (aRTIST == null)
            {
                return NotFound();
            }

            db.ARTISTs.Remove(aRTIST);
            db.SaveChanges();

            return Ok(aRTIST);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ARTISTExists(int id)
        {
            return db.ARTISTs.Count(e => e.ARTIST_ID == id) > 0;
        }
        private bool ARTISTExists(string name, int mediaid, int type)
        {
            return db.ARTISTs.Count(e => (e.ARTIST_NAME == name &&  e.ARTIST_TYPE_ID == type) || e.SOCIAL_MEDIA_ID == mediaid) > 0;
        }
    }
}
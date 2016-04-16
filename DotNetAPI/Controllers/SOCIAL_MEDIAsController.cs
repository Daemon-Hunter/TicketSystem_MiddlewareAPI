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
    public class SOCIAL_MEDIAsController : ApiController
    {
        private DBConn db = new DBConn();

        // GET: api/SOCIAL_MEDIAs
        public IQueryable<SOCIAL_MEDIA> GetSOCIAL_MEDIA()
        {
            return db.SOCIAL_MEDIA;
        }

        // GET: api/SOCIAL_MEDIAs/5
        [ResponseType(typeof(SOCIAL_MEDIA))]
        public IHttpActionResult GetSOCIAL_MEDIA(int id)
        {
            SOCIAL_MEDIA sOCIAL_MEDIA = db.SOCIAL_MEDIA.Find(id);
            if (sOCIAL_MEDIA == null)
            {
                return NotFound();
            }

            return Ok(sOCIAL_MEDIA);
        }

        // PUT: api/SOCIAL_MEDIAs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSOCIAL_MEDIA(int id, SOCIAL_MEDIA sOCIAL_MEDIA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sOCIAL_MEDIA.SOCIAL_MEDIA_ID)
            {
                return BadRequest();
            }

            db.Entry(sOCIAL_MEDIA).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SOCIAL_MEDIAExists(id))
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

        // POST: api/SOCIAL_MEDIAs
        [ResponseType(typeof(SOCIAL_MEDIA))]
        public IHttpActionResult PostSOCIAL_MEDIA(SOCIAL_MEDIA sOCIAL_MEDIA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SOCIAL_MEDIA.Add(sOCIAL_MEDIA);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SOCIAL_MEDIAExists(sOCIAL_MEDIA.SOCIAL_MEDIA_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = sOCIAL_MEDIA.SOCIAL_MEDIA_ID }, sOCIAL_MEDIA);
        }

        // DELETE: api/SOCIAL_MEDIAs/5
        [ResponseType(typeof(SOCIAL_MEDIA))]
        public IHttpActionResult DeleteSOCIAL_MEDIA(int id)
        {
            SOCIAL_MEDIA sOCIAL_MEDIA = db.SOCIAL_MEDIA.Find(id);
            if (sOCIAL_MEDIA == null)
            {
                return NotFound();
            }

            db.SOCIAL_MEDIA.Remove(sOCIAL_MEDIA);
            db.SaveChanges();

            return Ok(sOCIAL_MEDIA);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SOCIAL_MEDIAExists(int id)
        {
            return db.SOCIAL_MEDIA.Count(e => e.SOCIAL_MEDIA_ID == id) > 0;
        }
    }
}
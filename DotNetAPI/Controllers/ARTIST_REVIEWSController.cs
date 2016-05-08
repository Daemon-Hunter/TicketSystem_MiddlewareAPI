using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using DotNetAPI.Models;

namespace DotNetAPI.Controllers
{
    public class ARTIST_REVIEWSController : ApiController
    {
        private DBConn db = new DBConn();

        // GET: api/ARTIST_REVIEWS
        public IQueryable<ARTIST_REVIEW> GetARTIST_REVIEW()
        {
            return db.ARTIST_REVIEWs;
        }

        // GET: api/ARTIST_REVIEWS/5
        [ResponseType(typeof(ARTIST_REVIEW))]
        public IHttpActionResult GetARTIST_REVIEW(int id)
        {
            ARTIST_REVIEW aRTIST_REVIEW = db.ARTIST_REVIEWs.Find(id);
            if (aRTIST_REVIEW == null)
            {
                return NotFound();
            }

            return Ok(aRTIST_REVIEW);
        }

        // PUT: api/ARTIST_REVIEWS/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutARTIST_REVIEW(int id, ARTIST_REVIEW aRTIST_REVIEW)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != aRTIST_REVIEW.ARTIST_ID)
            {
                return BadRequest();
            }

            db.Entry(aRTIST_REVIEW).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ARTIST_REVIEWExists(aRTIST_REVIEW.ARTIST_ID, aRTIST_REVIEW.CUSTOMER_ID))
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

        // POST: api/ARTIST_REVIEWS
        [ResponseType(typeof(ARTIST_REVIEW))]
        public IHttpActionResult PostARTIST_REVIEW(ARTIST_REVIEW aRTIST_REVIEW)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ARTIST_REVIEWs.Add(aRTIST_REVIEW);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ARTIST_REVIEWExists(aRTIST_REVIEW.ARTIST_ID, aRTIST_REVIEW.CUSTOMER_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = aRTIST_REVIEW.ARTIST_ID }, aRTIST_REVIEW);
        }

        // DELETE: api/ARTIST_REVIEWS/5
        [ResponseType(typeof(ARTIST_REVIEW))]
        public IHttpActionResult DeleteARTIST_REVIEW(int id)
        {
            ARTIST_REVIEW aRTIST_REVIEW = db.ARTIST_REVIEWs.Find(id);
            if (aRTIST_REVIEW == null)
            {
                return NotFound();
            }

            db.ARTIST_REVIEWs.Remove(aRTIST_REVIEW);
            db.SaveChanges();

            return Ok(aRTIST_REVIEW);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ARTIST_REVIEWExists(int aid, int cid)
        {
            return db.ARTIST_REVIEWs.Count(e => e.ARTIST_ID == aid && e.CUSTOMER_ID == cid) > 0;
        }
    }
}
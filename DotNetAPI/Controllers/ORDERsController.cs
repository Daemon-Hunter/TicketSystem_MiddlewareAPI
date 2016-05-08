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
    public class ORDERsController : ApiController
    {
        private DBConn db = new DBConn();

        // GET: api/ORDERs
        public IQueryable<ORDER> GetORDERS()
        {
            return db.ORDERS;
        }

        // GET: api/ORDERs/5
        [ResponseType(typeof(ORDER))]
        public IHttpActionResult GetORDER(int id)
        {
            ORDER oRDER = db.ORDERS.Find(id);
            if (oRDER == null)
            {
                return NotFound();
            }

            return Ok(oRDER);
        }

        // PUT: api/ORDERs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutORDER(int id, ORDER oRDER)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != oRDER.ORDER_ID)
            {
                return BadRequest();
            }

            db.Entry(oRDER).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ORDERExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = oRDER.ORDER_ID }, db.ORDERS.Find(oRDER.ORDER_ID)); ;
        }

        // POST: api/ORDERs
        [ResponseType(typeof(ORDER))]
        public IHttpActionResult PostORDER(ORDER oRDER)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try {
                oRDER.ORDER_ID = db.ADD_ORDERS(oRDER.ORDER_ID, oRDER.CUSTOMER_ID);
            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest();
            }

            return CreatedAtRoute("DefaultApi", new { id = oRDER.ORDER_ID }, oRDER);
        }

        // DELETE: api/ORDERs/5
        [ResponseType(typeof(ORDER))]
        public IHttpActionResult DeleteORDER(int id)
        {
            ORDER oRDER = db.ORDERS.Find(id);
            if (oRDER == null)
            {
                return NotFound();
            }

            db.ORDERS.Remove(oRDER);
            db.SaveChanges();

            return Ok(oRDER);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ORDERExists(int id)
        {
            return db.ORDERS.Count(e => e.ORDER_ID == id) > 0;
        }
    }
}
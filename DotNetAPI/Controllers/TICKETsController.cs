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
    public class TICKETsController : ApiController
    {
        private DBConn db = new DBConn();

        // GET: api/TICKETs
        public IQueryable<TICKET> GetTICKETs()
        {
            return db.TICKETs;
        }

        // GET: api/TICKETs/5
        [ResponseType(typeof(TICKET))]
        public IHttpActionResult GetTICKET(int id)
        {
            TICKET tICKET = db.TICKETs.Find(id);
            if (tICKET == null)
            {
                return NotFound();
            }

            return Ok(tICKET);
        }

        // PUT: api/TICKETs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTICKET(int id, TICKET tICKET)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tICKET.TICKET_ID)
            {
                return BadRequest();
            }

            db.Entry(tICKET).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TICKETExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tICKET.TICKET_ID }, db.TICKETs.Find(tICKET.TICKET_ID));
        }

        // POST: api/TICKETs
        [ResponseType(typeof(TICKET))]
        public IHttpActionResult PostTICKET(TICKET tICKET)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TICKETExists(tICKET.CHILDEVENT_ID, tICKET.TICKET_TYPE))
            {
                try
                {
                    tICKET.TICKET_ID = db.ADD_TICKET(tICKET.TICKET_ID, tICKET.TICKET_PRICE, tICKET.TICKET_DESCRIPTION,
                        tICKET.TICKET_AMOUNT_REMAINING, tICKET.TICKET_TYPE, tICKET.CHILDEVENT_ID);
                }
                catch (DbUpdateConcurrencyException e)
                {
                    return BadRequest();
                }

                return CreatedAtRoute("DefaultApi", new { id = tICKET.TICKET_ID }, tICKET);
            }
            return Conflict();
        }

        // DELETE: api/TICKETs/5
        [ResponseType(typeof(TICKET))]
        public IHttpActionResult DeleteTICKET(int id)
        {
            TICKET tICKET = db.TICKETs.Find(id);
            if (tICKET == null)
            {
                return NotFound();
            }

            db.TICKETs.Remove(tICKET);
            db.SaveChanges();

            return Ok(tICKET);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TICKETExists(int id)
        {
            return db.TICKETs.Count(e => e.TICKET_ID == id) > 0;
        }
        private bool TICKETExists(int childid, string type)
        {
            return db.TICKETs.Count(e => e.CHILDEVENT_ID == childid && e.TICKET_TYPE == type) > 0;
        }
    }
}
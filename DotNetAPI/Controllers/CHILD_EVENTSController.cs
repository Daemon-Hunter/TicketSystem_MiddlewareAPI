﻿using System;
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
    public class CHILD_EVENTSController : ApiController
    {
        private DBConn db = new DBConn();

        // GET: api/CHILD_EVENTS
        public IQueryable<CHILD_EVENT> GetCHILD_EVENT()
        {
            return db.CHILD_EVENTs;
        }

        // GET: api/CHILD_EVENTS/5
        [ResponseType(typeof(CHILD_EVENT))]
        public IHttpActionResult GetCHILD_EVENT(int id)
        {
            CHILD_EVENT cHILD_EVENT = db.CHILD_EVENTs.Find(id);
            if (cHILD_EVENT == null)
            {
                return NotFound();
            }

            return Ok(cHILD_EVENT);
        }

        // PUT: api/CHILD_EVENTS/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCHILD_EVENT(int id, CHILD_EVENT cHILD_EVENT)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cHILD_EVENT.CHILD_EVENT_ID)
            {
                return BadRequest();
            }

            db.Entry(cHILD_EVENT).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CHILD_EVENTExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cHILD_EVENT.CHILD_EVENT_ID }, db.CHILD_EVENTs.Find(cHILD_EVENT.CHILD_EVENT_ID)); ;
        }

        // POST: api/CHILD_EVENTS
        [ResponseType(typeof(CHILD_EVENT))]
        public IHttpActionResult PostCHILD_EVENT(CHILD_EVENT cHILD_EVENT)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!CHILD_EVENTExists(cHILD_EVENT.PARENT_EVENT_ID, cHILD_EVENT.VENUE_ID, cHILD_EVENT.CHILD_EVENT_NAME))
            {
                try {
                cHILD_EVENT.CHILD_EVENT_ID = db.ADD_CHILD_EVENT(cHILD_EVENT.CHILD_EVENT_ID, cHILD_EVENT.VENUE_ID, cHILD_EVENT.PARENT_EVENT_ID,
                    cHILD_EVENT.CHILD_EVENT_NAME, cHILD_EVENT.CHILD_EVENT_DESCRIPTION, cHILD_EVENT.START_DATE_TIME,
                    cHILD_EVENT.END_DATE_TIME, cHILD_EVENT.CHILD_EVENT_CANCELED);
                }
                catch (DbUpdateConcurrencyException e)
                {
                    return BadRequest(e.Message);
                }

                return CreatedAtRoute("DefaultApi", new { id = cHILD_EVENT.CHILD_EVENT_ID }, cHILD_EVENT);
            }
            return Conflict();
        }

        // DELETE: api/CHILD_EVENTS/5
        [ResponseType(typeof(CHILD_EVENT))]
        public IHttpActionResult DeleteCHILD_EVENT(int id)
        {
            CHILD_EVENT cHILD_EVENT = db.CHILD_EVENTs.Find(id);
            if (cHILD_EVENT == null)
            {
                return NotFound();
            }

            db.CHILD_EVENTs.Remove(cHILD_EVENT);
            db.SaveChanges();

            return Ok(cHILD_EVENT);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CHILD_EVENTExists(int id)
        {
            return db.CHILD_EVENTs.Count(e => e.CHILD_EVENT_ID == id) > 0;
        }

        private bool CHILD_EVENTExists(int pid, int? venueid, string name)
        {
            return db.CHILD_EVENTs.Count(e => e.PARENT_EVENT_ID == pid && e.VENUE_ID == venueid && e.CHILD_EVENT_NAME == name) > 0;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DotNetAPI.Models;

namespace DotNetAPI.Controllers
{
    public class FunctionsController : ApiController
    {
        private DBConn db = new DBConn();

        [HttpGet]
        [Route("api/functions/comparepasswords/{id}/{password}")]
        public int comparePassword(int id, string password)
        {
            return db.COMPAREPASSWORDS(id, password);
        }

        [HttpGet]
        [Route("api/functions/getartistsamount/{amount}/{lowestID}")]
        public List<ARTIST> getArtistsAmount(int amount, int lowestID)
        {
            List<ARTIST> artistList = new List<ARTIST>();
            ARTIST artist;
            int count = 0;
            int lastID = db.ARTISTs.AsEnumerable().Last().ARTIST_ID;
            int diff = 0;

            if (lowestID != 0)
            {
                diff = lastID - lowestID + 1;
            }

            for (int id = lastID - diff; count != amount && id >= 0; id--)
            {
                artist = db.ARTISTs.Find(id);
                if (artist != null)
                {
                    artistList.Add(artist);
                    count++;
                }
            }

            return artistList;
        }

        [HttpGet]
        [Route("api/functions/getvenuesamount/{amount}/{lowestID}")]
        public List<VENUE> getVenuesAmount(int amount, int lowestID)
        {
            List<VENUE> venueList = new List<VENUE>();
            VENUE venue;
            int count = 0;
            int lastID = db.VENUEs.AsEnumerable().Last().VENUE_ID;
            int diff = 0;

            if (lowestID != 0)
            {
                diff = lastID - lowestID + 1;
            }

            for (int id = lastID - diff; count != amount && id >= 0; id--)
            {
                venue = db.VENUEs.Find(id);
                if (venue != null)
                {
                    venueList.Add(venue);
                    count++;
                }
            }

            return venueList;
        }

        [HttpGet]
        [Route("api/functions/getparenteventsamount/{amount}/{lowestID}")]
        public List<PARENT_EVENT> getParentEventsAmount(int amount, int lowestID)
        {
            List<PARENT_EVENT> parentEventList = new List<PARENT_EVENT>();
            PARENT_EVENT parentEvent;
            int count = 0;
            int lastID = db.PARENT_EVENT.AsEnumerable().Last().PARENT_EVENT_ID;
            int diff = 0;

            if (lowestID != 0)
            {
                diff = lastID - lowestID + 1;
            }

            for (int id = lastID - diff; count != amount && id >= 0; id--)
            {
                parentEvent = db.PARENT_EVENT.Find(id);
                if (parentEvent != null)
                {
                    parentEventList.Add(parentEvent);
                    count++;
                }
            }

            return parentEventList;
        }
    }
}

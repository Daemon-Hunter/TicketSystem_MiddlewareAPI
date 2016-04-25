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
        struct Reviews
        {
            public List<ARTIST_REVIEW> artistReviewList;
            public List<EVENT_REVIEW> parentEventReviewList;
            public List<VENUE_REVIEW> venueReviewList;
        }

        [HttpGet]
        [Route("api/functions/comparepasswords/{email}/{password}")]
        public CUSTOMER comparePassword(string email, string password)
        {
            CUSTOMER cust = db.CUSTOMERs.Where(c => c.CUSTOMER_EMAIL == email && c.CUSTOMER_PASSWORD == password).FirstOrDefault();

            return cust;
        }

        [HttpGet]
        [Route("api/functions/getartistsamount/{amount}/{lowestID}")]
        public List<ARTIST> getArtistsAmount(int amount, int lowestID)
        {
            List<ARTIST> artistList = new List<ARTIST>();
            ARTIST artist;
            int count = 0;
            int lastID = db.ARTISTs.Max(a => a.ARTIST_ID);
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
            int lastID = db.VENUEs.Max(a => a.VENUE_ID);
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
        [Route("api/functions/getparent_eventsamount/{amount}/{lowestID}")]
        public List<PARENT_EVENT> getParentEventsAmount(int amount, int lowestID)
        {
            List<PARENT_EVENT> parentEventList = new List<PARENT_EVENT>();
            PARENT_EVENT parentEvent;
            int count = 0;
            int lastID = db.PARENT_EVENTs.Max(a => a.PARENT_EVENT_ID);
            int diff = 0;

            if (lowestID != 0)
            {
                diff = lastID - lowestID + 1;
            }

            for (int id = lastID - diff; count != amount && id >= 0; id--)
            {
                parentEvent = db.PARENT_EVENTs.Find(id);
                if (parentEvent != null)
                {
                    parentEventList.Add(parentEvent);
                    count++;
                }
            }

            return parentEventList;
        }

        //[HttpGet]
        //[Route("api/functions/getcustomersreviews/{id}")]
        //public Reviews getCustomersReviews(int id)
        //{
        //    Reviews reviews;
        //    reviews.artistReviewList = db.ARTIST_REVIEW.Where(b => b.CUSTOMER_ID == id).ToList();
        //    reviews.venueReviewList = db.VENUE_REVIEW.Where(b => b.CUSTOMER_ID == id).ToList();
        //    reviews.parentEventReviewList = db.EVENT_REVIEW.Where(b => b.c == id).ToList();
        //    return reviews;
        //}

        [HttpGet]
        [Route("api/functions/getReviewsOfArtist/{id}")]
        public List<ARTIST_REVIEW> getReviewsOfArtist(int id)
        {
            return db.ARTIST_REVIEWs.Where(b => b.ARTIST_ID == id).ToList();
        }

        [HttpGet]
        [Route("api/functions/getReviewsOfVenue/{id}")]
        public List<VENUE_REVIEW> getReviewsOfVenue(int id)
        {
            return db.VENUE_REVIEWs.Where(b => b.VENUE_ID == id).ToList();
        }

        [HttpGet]
        [Route("api/functions/getReviewsOfParent_Event/{id}")]
        public List<EVENT_REVIEW> getReviewsOfParent_Event(int id)
        {
            return db.EVENT_REVIEW.Where(b => b.PARENT_EVENT_ID == id).ToList();
        }

        [HttpGet]
        [Route("api/functions/getChild_EventsOfParent_Event/{id}")]
        public List<CHILD_EVENT> getChild_EventsOfParent_Event(int id)
        {
            return db.CHILD_EVENTs.Where(c => c.PARENT_EVENT_ID == id).ToList();
        }

        [HttpGet]
        [Route("api/functions/getChild_EventsOfContract/{artistID}")]
        public List<CHILD_EVENT> getChild_EventsOfContract(int artistID)
        {
            return db.CONTRACTS.Where(c => c.ARTIST_ID == artistID).Select(d => d.CHILD_EVENT).ToList();
        }

        [HttpGet]
        [Route("api/functions/getArtistsOfContract/{childEventID}")]
        public List<ARTIST> getArtistsViaContract(int childEventID)
        {
            return db.CONTRACTS.Where(c => c.CHILD_EVENT_ID == childEventID).Select(d => d.ARTIST).ToList();
        }

        [HttpGet]
        [Route("api/functions/getEventsOfVenue/{venueID}")]
        public List<CHILD_EVENT> getEventsOfVenue(int venueID)
        {
            return db.CHILD_EVENTs.Where(c => c.VENUE_ID == venueID).ToList();
        }

        [HttpGet]
        [Route("api/functions/searchParent_Events/{searchString}")]
        public List<PARENT_EVENT> searchParentEvents(String searchString)
        {
            return db.PARENT_EVENTs.Where(p => p.PARENT_EVENT_NAME.ToLower().Contains(searchString.ToLower())).ToList();
        }

        [HttpGet]
        [Route("api/functions/searchArtists/{searchString}")]
        public List<ARTIST> searchArtists(String searchString)
        {
            return db.ARTISTs.Where(a => a.ARTIST_NAME.ToLower().Contains(searchString.ToLower())).ToList();
        }

        [HttpGet]
        [Route("api/functions/searchVenues/{searchString}")]
        public List<VENUE> searchVenues(String searchString)
        {
            return db.VENUEs.Where(a => a.VENUE_NAME.ToLower().Contains(searchString.ToLower())).ToList();
        }

        [HttpGet]
        [Route("api/functions/getBookingsOfOrder/{orderID}")]
        public List<BOOKING> getBookingsOfOrder(int orderID)
        {
            return db.BOOKINGs.Where(b => b.ORDER_ID == orderID).ToList();
        }

        [HttpGet]
        [Route("api/functions/getOrdersOfCustomer/{customerID}")]
        public List<ORDER> getOrdersOfCustomer(int customerID)
        {
            return db.ORDERS.Where(b => b.CUSTOMER_ID == customerID).ToList();
        }

        //Get Customer orders
        //Search to have max return value
    }
}

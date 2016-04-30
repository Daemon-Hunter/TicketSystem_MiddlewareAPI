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
        [Route("api/functions/compareCustomerPasswords/{email}/{password}")]
        public CUSTOMER compareCustomerPassword(string email, string password)
        {
            CUSTOMER cust = db.CUSTOMERs.Where(c => c.CUSTOMER_EMAIL == email.ToLower() && c.CUSTOMER_PASSWORD == password).FirstOrDefault();
            if (cust == null)
            {
                cust = new CUSTOMER();
                cust.CUSTOMER_ID = -1;
            }
            return cust;
        }

        [HttpGet]
        [Route("api/functions/compareAdminPasswords/{email}/{password}")]
        public ADMIN compareAdminPassword(string email, string password)
        {
            ADMIN admin = db.ADMINs.Where(c => c.ADMIN_EMAIL == email.ToLower() && c.ADMIN_PASSWORD == password).FirstOrDefault();
            if (admin == null)
            {
                admin = new ADMIN();
                admin.ADMIN_ID = -1;
            }
            return admin;
        }

        [HttpGet]
        [Route("api/functions/getartistsamount/{amount}/{lowestID}")]
        public List<ARTIST> getArtistsAmount(int amount, int lowestID)
        {
            if (lowestID.Equals(0))
            {
                return db.ARTISTs.OrderByDescending(a => a.ARTIST_ID).Take(amount).ToList();
            }
            else
            {
                return db.ARTISTs.OrderByDescending(a => a.ARTIST_ID).Where(a => a.ARTIST_ID < lowestID).Take(amount).ToList();
            }
        }

        [HttpGet]
        [Route("api/functions/getvenuesamount/{amount}/{lowestID}")]
        public List<VENUE> getVenuesAmount(int amount, int lowestID)
        {
            if (lowestID.Equals(0))
            {
                return db.VENUEs.OrderByDescending(a => a.VENUE_ID).Take(amount).ToList();
            }
            else
            {
                return db.VENUEs.OrderByDescending(a => a.VENUE_ID).Where(a => a.VENUE_ID < lowestID).Take(amount).ToList();
            }
        }

        [HttpGet]
        [Route("api/functions/getparent_eventsamount/{amount}/{lowestID}")]
        public List<PARENT_EVENT> getParentEventsAmount(int amount, int lowestID)
        {
            if (lowestID.Equals(0))
            {
                return db.PARENT_EVENTs.OrderByDescending(a => a.PARENT_EVENT_ID).Take(amount).ToList();
            }
            else
            {
                return db.PARENT_EVENTs.OrderByDescending(a => a.PARENT_EVENT_ID).Where(a => a.PARENT_EVENT_ID < lowestID).Take(amount).ToList();
            }
        }

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
            return db.EVENT_REVIEWs.Where(b => b.PARENT_EVENT_ID == id).ToList();
        }

        [HttpGet]
        [Route("api/functions/getChild_EventsOfParent_Event/{id}")]
        public List<CHILD_EVENT> getChild_EventsOfParent_Event(int id)
        {
            return db.CHILD_EVENTs.Where(c => c.PARENT_EVENT_ID == id).OrderBy(d => d.START_DATE_TIME).ToList();
        }

        [HttpGet]
        [Route("api/functions/getChild_EventsOfArtist/{artistID}")]
        public List<CHILD_EVENT> getChild_EventsOfArtist(int artistID)
        {
            return db.CHILD_EVENTs.Where(a => a.ARTISTs.Select(b => b.ARTIST_ID).Contains(artistID)).OrderBy(d => d.START_DATE_TIME).ToList();
        }

        [HttpGet]
        [Route("api/functions/getArtistsOfChild_Event/{childEventID}")]
        public List<ARTIST> getArtistsOfChild_Event(int childEventID)
        {
            return db.ARTISTs.Where(a => a.CHILD_EVENT.Select(b => b.CHILD_EVENT_ID).Contains(childEventID)).ToList();
        }

        [HttpGet]
        [Route("api/functions/getChild_EventsOfVenue/{venueID}")]
        public List<CHILD_EVENT> getChild_EventsOfVenue(int venueID)
        {
            return db.CHILD_EVENTs.Where(c => c.VENUE_ID == venueID).OrderBy(d => d.START_DATE_TIME).ToList();
        }

        [HttpGet]
        [Route("api/functions/searchParent_Events/{searchString}/{amount}")]
        public List<PARENT_EVENT> searchParentEvents(String searchString, int amount)
        {
            return db.PARENT_EVENTs.Where(p => p.PARENT_EVENT_NAME.ToLower().Contains(searchString.ToLower())).Take(amount).ToList();
        }

        [HttpGet]
        [Route("api/functions/searchArtists/{searchString}/{amount}")]
        public List<ARTIST> searchArtists(String searchString, int amount)
        {
            return db.ARTISTs.Where(a => a.ARTIST_NAME.ToLower().Contains(searchString.ToLower())).Take(amount).ToList();
        }

        [HttpGet]
        [Route("api/functions/searchVenues/{searchString}/{amount}")]
        public List<VENUE> searchVenues(String searchString, int amount)
        {
            return db.VENUEs.Where(a => a.VENUE_NAME.ToLower().Contains(searchString.ToLower())).Take(amount).ToList();
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

        [HttpGet]
        [Route("api/functions/getTicketsOfChild_Event/{childEventID}")]
        public List<TICKET> getTicketsOfChildEvent(int childEventID)
        {
            return db.TICKETs.Where(t => t.CHILDEVENT_ID == childEventID).ToList();
        }

        //Get Customer orders
        //Search to have max return value
    }
}

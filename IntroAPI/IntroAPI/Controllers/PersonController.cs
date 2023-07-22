using IntroAPI.EF;
using IntroAPI.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IntroAPI.Controllers
{
    public class PersonController : ApiController
    {
        [HttpGet]
        [Route("api/person/all")]
        public HttpResponseMessage GelAll() {
            var db = new PersonContext();
            var data = db.Persons.ToList();
            return Request.CreateResponse(HttpStatusCode.OK,data);
        }
        [HttpGet]
        [Route("api/person/{id}")]
        public HttpResponseMessage Get(int id) {
            var db = new PersonContext();
            var data = db.Persons.Find(id);
            return Request.CreateResponse(HttpStatusCode.OK,data);
        }
        [HttpGet]
        [Route("api/person/name/{name}")]
        public HttpResponseMessage Get(string name) {
            var db = new PersonContext();
            var data = (from p in db.Persons
                        where p.Name.Contains(name)
                        select p).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
        [HttpPost]
        [Route("api/person/create")]
        public HttpResponseMessage Create(Person p) {
            var db = new PersonContext();
            dynamic testp = new { };

            try
            {
                db.Persons.Add(p);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { Msg = "Created" });

            }
            catch (Exception ex) { 
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            
           
    
        }
        [HttpPost]
        [Route("api/person/edit")]
        public HttpResponseMessage Update(Person p) {
            var db = new PersonContext();
            var exp = db.Persons.Find(p.Id);
            //exp.Name = p.Name;
            //exp.Dob = p.Dob;
            try
            {
                db.Entry(exp).CurrentValues.SetValues(p); //don't use this always
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { Msg = "Updated" });
            }
            catch (Exception ex) {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            
         
        }
    }
}

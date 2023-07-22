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
    public class CatagoryController : ApiController
    {
        [HttpGet]
        [Route("api/catagory/read")]
        public HttpResponseMessage GetAll()
        {
            var db = new PersonContext();
            var data = db.Catagories.ToList();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
        [HttpGet]
        [Route("api/catagory/id/{id}")]
        public HttpResponseMessage Get(int id)
        {
            var db = new PersonContext();
            var data = db.Catagories.Find(id);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        

       
        [HttpPost]
        [Route("api/catagory/create")]
        public HttpResponseMessage Create(Catagory p)
        {
            var db = new PersonContext();
            //dynamic testp = new { };

            try
            {
                db.Catagories.Add(p);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { Msg = "Created" });

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }



        }
        [HttpPost]
        [Route("api/catagory/edit")]
        public HttpResponseMessage Update(Catagory p)
        {
            var db = new PersonContext();
            var exp = db.Catagories.Find(p.Id);
            //exp.Name = p.Name;
            //exp.Dob = p.Dob;
            try
            {
                db.Entry(exp).CurrentValues.SetValues(p); //don't use this always
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { Msg = p.Id + " Updated" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }


        }

        [HttpDelete]
        [Route("api/catagory/delete/{id}")]
        public HttpResponseMessage Delete(Catagory p)
        {
            var db = new PersonContext();
            var exp = db.Catagories.Find(p.Id);
            //exp.Name = p.Name;
            //exp.Dob = p.Dob;
            try
            {
                //db.Entry(exp).CurrentValues.SetValues(p); //don't use this always
                db.Catagories.Remove(exp);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { Msg = p.Id + " Deleted" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }


        }
    }
}

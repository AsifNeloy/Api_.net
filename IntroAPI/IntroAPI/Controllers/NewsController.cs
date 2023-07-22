using IntroAPI.EF;
using IntroAPI.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace IntroAPI.Controllers
{
    public class NewsController : ApiController
    {
        [HttpGet]
        [Route("api/news/read")]
        public HttpResponseMessage GetAll()
        {
            var db = new PersonContext();
            var data = db.News.ToList();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
        [HttpGet]
        [Route("api/news/id/{id}")]
        public HttpResponseMessage Get(int id)
        {
            var db = new PersonContext();
            var data = db.News.Find(id);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpGet]
        [Route("api/news/date/{Dob}")]
        public HttpResponseMessage Get(DateTime d)
        {
            var db = new PersonContext();
            var data = (from h in db.News
                       where h.Dob.Equals(d)
                        select h).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpGet]
        [Route("api/news/catagory/{name}")]
        public HttpResponseMessage Get([FromUri] string name)
        {
            var db = new PersonContext();
            //var id = (from h in db.Catagories
            //          where h.Name.Equals(name)
            //          select h.Id);
            //var data = (from h in db.News
            //            where h.CatId.Equals(id)
            //            select h).ToList();
            var data = (from h in db.Catagories
                        where h.Name.Equals(name)
                        select h.News).ToList();
            string json = JsonConvert.SerializeObject(data);
            return Request.CreateResponse(HttpStatusCode.OK, json);
        }
        [HttpGet]
        [Route("api/news/catagory/{name}/{dob}")]
        public HttpResponseMessage Get([FromUri] string name, DateTime d)
        {
            var db = new PersonContext();
            //var id = (from h in db.Catagories
            //          where h.Name.Equals(name)
            //          select h.Id);
            //var data = (from h in db.News
            //            where h.CatId.Equals(id)
            //            select h).ToList();
            var data = (from h in db.Catagories
                        where h.Name.Equals(name)
                        select h.News).ToList();
            string json = JsonConvert.SerializeObject(data);
            return Request.CreateResponse(HttpStatusCode.OK, json);
        }



        //[HttpGet]
        //[Route("api/news/catagory/{Name}")]
        //public HttpResponseMessage Get(String d)
        //{
        //    var db = new PersonContext();
        //    var data = db.Catagories.Find(Name);
        //    return Request.CreateResponse(HttpStatusCode.OK, data);
        //}

        //[HttpGet]
        //[Route("api/news/catagory/{name}")]
        //public HttpResponseMessage Get(string c)
        //{
        //    var db = new PersonContext();
        //    var catId = (from h in db.Catagory 
        //                 where h.CatId.Equals(c.Id)
        //                 select h).ToList();


        //   //var data = (from p in db.News
        //   //             where p.Title.Contains(title)
        //   //             select p);
        //    return Request.CreateResponse(HttpStatusCode.OK, catId);
        //}
        [HttpPost]
        [Route("api/news/create")]
        public HttpResponseMessage Create(News p)
        {
            var db = new PersonContext();
            //dynamic testp = new { };

            try
            {
                db.News.Add(p);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { Msg = "Created" });

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }



        }
        [HttpPost]
        [Route("api/news/edit")]
        public HttpResponseMessage Update(News p)
        {
            var db = new PersonContext();
            var exp = db.News.Find(p.Id);
            //exp.Name = p.Name;
            //exp.Dob = p.Dob;
            try
            {
                db.Entry(exp).CurrentValues.SetValues(p); //don't use this always
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { Msg = p.Id+" Updated" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }


        }

        [HttpDelete]
        [Route("api/news/delete/{id}")]
        public HttpResponseMessage Delete(News p)
        {
            var db = new PersonContext();
            var exp = db.News.Find(p.Id);
            //exp.Name = p.Name;
            //exp.Dob = p.Dob;
            try
            {
                //db.Entry(exp).CurrentValues.SetValues(p); //don't use this always
                db.News.Remove(exp);
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

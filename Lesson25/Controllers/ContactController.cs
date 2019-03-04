using Lesson25.Models;
using Lesson25.RestSharp;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lesson25.Controllers
{
    public class ContactController : Controller
    {
        public ActionResult Index()
        {
            //temporary code
            RestClient client = new RestClient("http://localhost:8018/");
            RestRequest request = new RestRequest("/api/contacts", Method.GET);

            IRestResponse<List<Contact>> response = client.Execute<List<Contact>>(request);
            List<Contact> contacts = response.Data;

            //Contact contact2 = RestSharpHelper.Execute<Contact>(@"/api/v2/phonebook/contacts/{id}",Method.GET,
            //    new Dictionary<string, object>
            //    {
            //        {"id", contacts[0].Id }
            //    }, true
            //    ).Data;

            return View(contacts);
        }
    }
}
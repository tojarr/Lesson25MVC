using Lesson25.Models;
using Lesson25.RestSharp;
using Newtonsoft.Json;
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
        //Postman
    //    {
    //    "Id": "a15064c4-446b-4843-99bf-88d9aafde38a",
    //    "FirstName": "Donald",
    //     "LastName": "Duck",
    //           "PhoneNumbers": [
    //               "55555555",
    //               "79797979"
    //           ]
    //}

    RestSharpHelper restSharpHeler = new RestSharpHelper();

        public ActionResult Index()
        {
            var contacts = restSharpHeler.Execute<List<Contact>>(/*"/api/v2/phonebook/contacts"*/RestServiceNames.GetContacts, Method.GET, null, true).Data;

            //temporary code
            //RestClient client = new RestClient("http://localhost:8018/");
            //RestRequest request = new RestRequest("/api/contacts", Method.GET);

            //IRestResponse<List<Contact>> response = client.Execute<List<Contact>>(request);
            //List<Contact> contacts = response.Data;

            //Contact contact2 = RestSharpHelper.Execute<Contact>(@"/api/v2/phonebook/contacts/{id}",Method.GET,
            //    new Dictionary<string, object>
            //    {
            //        {"id", contacts[0].Id }
            //    }, true
            //    ).Data;

            return View(contacts);
        }

        [HttpGet]
        public ActionResult Create()
        {
             return View();
        }
        [HttpPost]
        public ActionResult Create(Contact contact)
        {
            restSharpHeler.Execute<Contact>(/*"/api/v2/phonebook/contacts"*/RestServiceNames.CreateContact, Method.POST,
                new Dictionary<string, object>()
                {
                    { "body", contact }
                }, true
                );

            return RedirectToAction("Index", "Contact");
        }

        
        public ActionResult Delete(Guid id)
        {
            restSharpHeler.Execute<Contact>(/*"/api/v2/phonebook/contacts/{id}"*/RestServiceNames.DeleteContact, Method.POST,
                new Dictionary<string, object>()
                {
                    { "id", id }
                }, true
                );

            return RedirectToAction("Index", "Contact");
        }

        public ActionResult Details(Guid id)
        {
            var contact = restSharpHeler.Execute<Contact>(/*"/api/v2/phonebook/contacts"*/RestServiceNames.GetContactById, Method.GET,
                new Dictionary<string, object>()
                {
                    { "id", id }
                }, 
                true
                ).Data;
            

            return View(contact);
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var contact = restSharpHeler.Execute<Contact>(/*"/api/v2/phonebook/contacts"*/RestServiceNames.GetContactById, Method.GET,
                new Dictionary<string, object>()
                {
                    { "id", id }
                },
                true
                ).Data;

            return View(contact);
        }
        [HttpPost]
        public ActionResult Edit(Contact contact)
        {
            restSharpHeler.Execute<Contact>(/*"/api/v2/phonebook/contacts"*/RestServiceNames.UpdateContact, Method.POST,
                new Dictionary<string, object>()
                {
                    { "body", contact }
                }, true
                );

            return RedirectToAction("Index", "Contact");
        }
    }
}

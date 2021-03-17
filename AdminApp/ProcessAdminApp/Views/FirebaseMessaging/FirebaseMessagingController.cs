using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Hydron.Helpers;
using Hydron.Models.Definitions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace Hydron.Views.Payment
{

    public class FirebaseMessagingController : Controller
    {

        private readonly FirebaseMessaging messaging;


        public FirebaseMessagingController()
        {
            var firebaseApp = FirebaseApp.DefaultInstance;

            if(firebaseApp == null)
            {
              var app = FirebaseApp.Create(
              new AppOptions()
              {
                  //service_account.json should be kept somewhere. 
                  Credential = GoogleCredential.FromFile(@"C:\Users\Mark.Antony\Documents\service_account.json").CreateScoped("https://www.googleapis.com/auth/firebase.messaging")
              });
                messaging = FirebaseMessaging.GetMessaging(app);
            }
            else
            {
                messaging = FirebaseMessaging.GetMessaging(firebaseApp);
            }       
                   
        }



        private Message CreateNotification(string title, string notificationBody, string topic)
        {
            return new Message()
            {
                Topic = topic,
                Notification = new Notification()
                {
                    Body = notificationBody,
                    Title = title
                }
            };
        }

        //call this function to send notification.
        public async Task SendNotification(string topic, string title, string body)
        {
            var result = await messaging.SendAsync(CreateNotification(title, body, topic));
           
        }


        public async Task<ActionResult> Index()
        {
            
            await SendNotification("MERO-BOT-01-CLIENT", "Command", "1234");
            return View();

        }

      

    }
}
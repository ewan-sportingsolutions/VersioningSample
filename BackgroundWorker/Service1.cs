using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FixtureServiceRestSharp;
using Model;
using Model.Interfaces;

namespace BackgroundWorker
{
    public partial class Service1 : ServiceBase
    {
        private Timer timer;
        private IFixtureServiceAsync fixtureService;
        private int count;
        public Service1()
        {
            this.count = 0;

            Uri serviceEndPoint = new Uri(ConfigurationManager.AppSettings["serviceEndPoint"]);
            string authKey = ConfigurationManager.AppSettings["authKey"];

            fixtureService = new FixtureServiceAsync_RestSharp(
                serviceEndPoint,
                authKey);
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer = new Timer(Callback,null,0,5000);
        }

        private void Callback(
            object state)
        {
            this.count++;
            Fixture f = new Fixture();
            f.Id = this.count.ToString();
            f.Description = "created by the background worker!";
            this.fixtureService.AddFixtureAsync(
                f);

            Console.WriteLine("Created Fixture " + f.Id);
        }

        protected override void OnStop()
        {

        }


    }
}

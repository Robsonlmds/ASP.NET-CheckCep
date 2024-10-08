﻿using CheckCep.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace CheckCep.Controllers
{
    public class AddressController : Controller
    {

        Uri baseAddress = new Uri("https://viacep.com.br/ws/");
        private readonly HttpClient _client;
        private IWebHostEnvironment _hostingEnvironment;

        public AddressController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("HomeSite");
        }

        [HttpPost]
        public IActionResult Index(string cep)
        {
            var requisicaoWeb = WebRequest.CreateHttp(baseAddress + cep + "/json/");
            requisicaoWeb.Method = "GET";
            requisicaoWeb.UserAgent = "RequisicaoWebDemo";

            Address take = new Address("", "", "", "", "");

            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();

                take = JsonConvert.DeserializeObject<Address>(objResponse.ToString());

                /*Console.WriteLine(take.logradouro + " " + take.complemento + " " + take.localidade + " " + take.uf + " " + take.cep);*/
                /* streamDados.Close();
                 resposta.Close();*/

            }

            ViewBag.cep = take;

            return View("HomeSite");
        }

        [HttpPost]
        [ActionName("MethodEmail")]
        public IActionResult MethodEmail(string email)
        {
            email.ToString();
            User user = new User();
            user.Email = email;

            ViewBag.email = email;

            return View("HomeSite");
        }

    }
}
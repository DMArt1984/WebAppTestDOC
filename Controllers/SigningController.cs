using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppTestDOC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SigningController : ControllerBase
    {
        const string PathDoc = "Files";

        private readonly ILogger<SigningController> _logger; // может и пригодится...

        private IWebHostEnvironment _appEnvironment;

        // Шаблоны ответов
        private object jsonErrFile = new { Result = 101, Message = "Файл не найден" };
        private object jsonErrPhone = new { Result = 102, Message = "Телефон не задан" };
        private object jsonErrIIN = new { Result = 103, Message = "ИИН/БИН не задан" };
        private object jsonErrName = new { Result = 104, Message = "ФИО не заданы" };
        private object jsonErrDoc = new { Result = 201, Message = "Документ не найден" };

        private System.Text.Json.JsonSerializerOptions jsonOptions = new System.Text.Json.JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true, // учитываем регистр
            WriteIndented = true                // отступы для красоты
        };

        public SigningController(ILogger<SigningController> logger, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _appEnvironment = appEnvironment;
        }

        [HttpGet]
        public JsonResult Hello()
        {
            var rng = new Random();
            return new JsonResult(new { Result = 0, Message = "I'm alive", Random = rng.Next(-20, 55), }, jsonOptions);
        }

        // Нужна ли авторизация? сейчас ее нет
        [HttpPost("first")] // api/signing/first
        public JsonResult UploadFirstData( // может использовать [FromForm] 
            IFormFile uploadedDocument, // документ
            string phone, // телефон
            string IIN, // ИИН или БИК
            string fullName) // ФИО
        {
            try
            {
                // Проверка данных (без использования регулярных выражений)
                if (uploadedDocument == null)
                    return new JsonResult(jsonErrFile, jsonOptions);

                if (!Validator.Phone(phone))
                    return new JsonResult(jsonErrPhone, jsonOptions);

                if (!Validator.IIN(IIN))
                    return new JsonResult(jsonErrIIN, jsonOptions);

                if (!Validator.FullName(fullName))
                    return new JsonResult(jsonErrName, jsonOptions);

                // Сохранение файла, например так:
                //string path = $"/{PathDoc}/{uploadedDocument.FileName}";
                //path = path.Replace("//", "/"); // на всякий случай)

                //Directory.CreateDirectory(_appEnvironment.WebRootPath + PathDoc);
                //using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                //{
                //    await uploadedDocument.CopyToAsync(fileStream).ConfigureAwait(false);
                //}

                // Обработка данных и получение кода
                var code = DocManager.GetFirstData(uploadedDocument.FileName, phone, IIN, fullName);

                // Mогут пригодиться для формирования URL
                var myHost = this.HttpContext.Request.Host;
                var myPath = this.HttpContext.Request.Path;
                // например...
                var retvalURL = myHost + myPath.ToString().Replace("first", "second") + "/" + code;

                // Ответ
                return new JsonResult(new { Result = 0, URL = retvalURL }, jsonOptions);

            }
            catch (Exception ex)
            {
                return new JsonResult(new { Result = ex.HResult, Message = ex.Message, Source = ex.Source }, jsonOptions);
            }
        }


        [HttpGet("second/{code}")] // полученный URL из предыдущего запроса (retvalURL)
        public JsonResult SecondStep(string code)
        {
            // Получаем документ
            Document document = DocManager.GetSecondData(code);

            if (document == null)
                return new JsonResult( jsonErrDoc, jsonOptions);

            // Работа с документом
            // ...код...

            // Мы возвращаем путь к файлу или сам файл?

            // Ответ
            return new JsonResult(new { Result = 0, Document = document }, jsonOptions);
        }

        [HttpGet("left/{code}")] // подписание первой стороной
        public JsonResult StepLeft(string code)
        {
            if (DocManager.SetLeft(code))
                return new JsonResult(new { Result = 0, Document = DocManager.GetDocument(code) }, jsonOptions);

            // Какие еще могут быть ошибки?
            return new JsonResult(jsonErrDoc, jsonOptions);
        }

        [HttpGet("right/{code}")] // подписание второй стороной
        public JsonResult StepRight(string code)
        {
            if (DocManager.SetRight(code))
                return new JsonResult(new { Result = 0, Document = DocManager.GetDocument(code) }, jsonOptions);

            // Какие еще могут быть ошибки?
            return new JsonResult(jsonErrDoc, jsonOptions);
        }

    }
}

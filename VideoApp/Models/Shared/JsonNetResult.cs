﻿using Newtonsoft.Json;
using System;
using System.Web.Mvc;

namespace VideoApp.Models.Shared
{
    public class JsonNetResult : JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            var response = context.HttpContext.Response;

            response.ContentType = !string.IsNullOrEmpty(ContentType) ? ContentType : "application/json";

            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;

            if (Data == null)
                return;

            var serializedObject = JsonConvert.SerializeObject(Data, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                DateFormatString = "dd.MM.yyyy"
            });
            response.Write(serializedObject);
        }
    }
}
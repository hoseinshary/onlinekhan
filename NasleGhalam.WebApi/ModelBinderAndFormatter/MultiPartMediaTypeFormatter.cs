using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Permissions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using NasleGhalam.Common;
using NasleGhalam.ViewModels._MediaFormatter;

namespace NasleGhalam.WebApi.ModelBinderAndFormatter
{
    public class MultiPartMediaTypeFormatter : MediaTypeFormatter
    {
        public MultiPartMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("multipart/form-data"));
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/octet-stream"));
        }

        public override bool CanReadType(Type type)
        {
            return typeof(IMultiPartMediaTypeFormatter).IsAssignableFrom(type);
        }

        public override bool CanWriteType(Type type)
        {
            return false;
        }

        public override async Task<object> ReadFromStreamAsync(Type type, Stream stream, HttpContent content,
            IFormatterLogger formatterLogger)
        {
            MultipartMemoryStreamProvider provider = await content.ReadAsMultipartAsync();
            IEnumerable<HttpContent> formData = provider.Contents.AsEnumerable();

            var modelInstance = Activator.CreateInstance(type);
            IEnumerable<PropertyInfo> properties = type.GetProperties();

            foreach (PropertyInfo prop in properties)
            {
                var propName = prop.Name.ToLower().Trim();
                var propType = prop.PropertyType;

                // todo: check -- for string input
                var data = formData.FirstOrDefault(d => d.Headers
                    .ContentDisposition.Name.ToLower()
                    .Replace("\"", "").Replace("\'", "")
                    .Trim() == propName);


                if (data != null)
                {
                    if (data.Headers.ContentType != null)
                    {
                        using (var fileStream = await data.ReadAsStreamAsync())
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                fileStream.CopyTo(ms);
                                prop.SetValue(modelInstance, ms.ToArray());
                            }
                        }
                    }
                    else
                    {
                        var rawVal = await data.ReadAsStringAsync();

                        

                        if (rawVal.StartsWith("[") && rawVal.EndsWith("]"))
                        {
                            var jsSer = new JavaScriptSerializer();
                            var note = jsSer.Deserialize<int []>(rawVal );

                            rawVal = rawVal.Substring(1, rawVal.Length - 3);
                            var rawvals = rawVal.Split(',');
                            List<int> temp = new List<int>();
                            foreach (var val in rawvals)
                            {
                                temp.Add(Convert.ToInt32(val));
                            }
                            prop.SetValue(modelInstance, temp);
                        }
                        else
                        {
                            object val = Convert.ChangeType(rawVal, propType);

                            if (propType == typeof(DateTime))
                            {
                                prop.SetValue(modelInstance, rawVal.ToMiladiDateTime());
                            }
                            else if (propType == typeof(string))
                            {
                                prop.SetValue(modelInstance, rawVal.Trim());
                            }
                            else
                            {
                                prop.SetValue(modelInstance, val);
                            }
                        }
                    }
                }
            }

            return modelInstance;
        }

        class something
        {
            public List<string> strings;
        }
    }
}
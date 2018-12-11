using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using NasleGhalam.Common;
using NasleGhalam.ViewModels._MediaFormatter;

namespace NasleGhalam.WebApi.ModelBinderAndFormatter
{
    public class MultiPartMediaTypeFormatter : MediaTypeFormatter
    {
        public MultiPartMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("multipart/form-data"));
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
            var formData = provider.Contents.AsEnumerable();

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

                if (data == null) continue;

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

            return modelInstance;
        }
    }
}
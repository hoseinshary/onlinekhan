using System;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;

namespace NasleGhalam.WebApi.ModelBinderAndFormatter
{
    public class StringModelBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext.ValueProvider.GetValue(
                bindingContext.ModelName);
            if (String.IsNullOrWhiteSpace(valueResult?.AttemptedValue))
            {
                return false;
            }
            string val = valueResult
                .AttemptedValue.Trim()
                .Replace("ي", "ی").Replace("ك", "ک");
            bindingContext.Model = val;
            return true;
        }
    }
}
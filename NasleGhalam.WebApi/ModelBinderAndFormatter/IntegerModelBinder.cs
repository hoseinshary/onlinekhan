using System;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;

namespace NasleGhalam.WebApi.ModelBinderAndFormatter
{
    public class IntegerModelBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext.ValueProvider.GetValue(
                bindingContext.ModelName);
            if (valueResult == null || valueResult.AttemptedValue == null)
            {
                return false;
            }

            Int32.TryParse(valueResult
                .AttemptedValue.Trim()
                .Replace(",", ""), out int val);

            bindingContext.Model = val;
            return true;
        }
    }
}
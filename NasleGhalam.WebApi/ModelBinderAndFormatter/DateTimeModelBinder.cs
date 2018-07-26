using System;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;
using NasleGhalam.Common;

namespace NasleGhalam.WebApi.ModelBinderAndFormatter
{
    public class DateTimeModelBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (String.IsNullOrWhiteSpace(valueResult?.AttemptedValue))
            {
                return false;
            }

            bindingContext.Model = valueResult.AttemptedValue.ToMiladiDateTime();
            return true;
        }
    }
}
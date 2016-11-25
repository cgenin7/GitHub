using System;
using System.Globalization;
using System.Web.Mvc;

namespace APMPBilling
{
    public class FloatModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext.ValueProvider
                .GetValue(bindingContext.ModelName);

            ModelState modelState = new ModelState { Value = valueResult };

            float actualValue = 0;

            if (valueResult.AttemptedValue != string.Empty)
            {
                string value = valueResult.AttemptedValue.Replace(',', '.');
                float.TryParse(value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out actualValue);
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);

            return actualValue;
        }
    }
}
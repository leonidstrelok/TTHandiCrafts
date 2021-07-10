using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using TTHandiCrafts.UseCases.Modules.Products.Commands.CreateProduct;

namespace TTHandiCrafts.CustomBinders
{
    public class ImportFromFileBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            
            if (context.Metadata.ModelType == typeof(CreateProductCommand))
            {
                return new BinderTypeModelBinder(typeof(ImportFromFileBinder));
            }

            return null;
        }
    }
}
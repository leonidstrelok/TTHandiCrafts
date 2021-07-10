using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using TTHandiCrafts.UseCases.Modules.Admins.Commands.AdvertisingCommands.CreateAdvertising;
using TTHandiCrafts.UseCases.Modules.Admins.Commands.AdvertisingCommands.UpdateAdvertising;
using TTHandiCrafts.UseCases.Modules.Products.Commands.CreateProduct;
using TTHandiCrafts.UseCases.Modules.Products.Commands.UpdateProduct;

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
            
            if (context.Metadata.ModelType == typeof(CreateAdvertisingCommand))
            {
                return new BinderTypeModelBinder(typeof(ImportFromFileBinder));
            }
            
            if (context.Metadata.ModelType == typeof(UpdateAdvertisingCommand))
            {
                return new BinderTypeModelBinder(typeof(ImportFromFileBinder));
            }

            if (context.Metadata.ModelType == typeof(UpdateProductCommand))
            {
                return new BinderTypeModelBinder(typeof(ImportFromFileBinder));
            }

            return null;
        }
    }
}
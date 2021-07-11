using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TTHandiCrafts.Models.Models.Products.Enums;
using TTHandiCrafts.UseCases.Dtos;

namespace TTHandiCrafts.CustomBinders
{
    public class ImportFromFileBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            Type modelType = bindingContext.ModelMetadata.UnderlyingOrModelType;


            dynamic model = Activator.CreateInstance(modelType);

            model.Name = bindingContext.HttpContext.Request.Form["Name"].ToString();
            model.ProductType = (ProductType) Enum.Parse(typeof(ProductType),
                Convert.ToInt32(bindingContext.HttpContext.Request.Form["ProductType"].ToString()).ToString());
            model.CategoryType = (CategoryType) Enum.Parse(typeof(CategoryType),
                Convert.ToInt32(bindingContext.HttpContext.Request.Form["CategoryType"].ToString()).ToString());
            model.ToMake = Convert.ToBoolean(bindingContext.HttpContext.Request.Form["ToMake"].ToString());
            model.Comment = bindingContext.HttpContext.Request.Form["Comment"].ToString();


            var formFiles = bindingContext.HttpContext.Request.Form.Files;

            if (!formFiles.Any()) return Task.CompletedTask;


            foreach (var propertyName in model.GetType().GetProperties())
            {
                VerifyAndAddValueForType(formFiles, propertyName, model);
            }


            bindingContext.Result = ModelBindingResult.Success(model);

            return Task.CompletedTask;
        }

        private void VerifyAndAddValueForType(IFormFileCollection formFiles, dynamic propertyName, dynamic model)
        {
            if (formFiles.Any(p =>
                p.Name.Equals(propertyName.Name) &&
                typeof(ICollection<VersionFile>).IsAssignableFrom(propertyName.PropertyType)))
            {
                propertyName.SetValue(model,
                    formFiles.Where(p => p.Name.Equals(propertyName.Name))
                        .Select(p =>
                            new VersionFile
                            {
                                Stream = p.OpenReadStream(),
                                FileName = p.FileName
                            }).ToList());
            }
            else if (propertyName.PropertyType == typeof(VersionFile))
            {
                var stream = formFiles[propertyName.Name].OpenReadStream();
                var fileName = formFiles[propertyName.Name].FileName;
                propertyName.SetValue(model, new VersionFile()
                {
                    Stream = stream,
                    FileName = fileName
                });
            }
        }

        private string ToLowerFirst(string text)
            => !string.IsNullOrEmpty(text)
                ? $"{char.ToLower(text[0])}{text.Substring(1)}"
                : text;
    }
}
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShop.Infrastructure
{
    public class FileExtensionAttribute : ValidationAttribute
    {
        private readonly string[] extensions = { "jpg", "png" };

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as FormFile;

            if(file != null)
            {
                var extension = Path.GetExtension(file.FileName);

                
                bool result = extensions.Any(x => extension.EndsWith(x));

                if (!result)
                    return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }

        private string GetErrorMessage()
        {
            return "Allowed extansions are " + string.Join(" and ", extensions);
        }
    }
}

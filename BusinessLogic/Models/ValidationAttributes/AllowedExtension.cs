using BusinessLogic.Helper;
using BusinessLogic.Services;
using ImageMagick;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BusinessLogic.Models.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class AllowedExtension : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file == null)
                return new ValidationResult(ErrorMessage);

            var service = (IConfigurationsService)validationContext.GetService(typeof(IConfigurationsService));

            if (file.ContentType.Length > 0 && file.ContentType.Contains("image"))
            {
                var image = new MagickImage(file.OpenReadStream());
                var minHeightConfig = service.GetAll().Where(s => s.ConfigurationTypeId == (int)Constants.ConfigurationType.MinImgHeight).FirstOrDefault();

                if (minHeightConfig != null)
                {
                    int minHeight = int.Parse(minHeightConfig.Value);

                    if (image.Height < minHeight)
                    {
                        ErrorMessage = $"Image is too small, minimum allowed height is: {minHeight}px";

                        return new ValidationResult(ErrorMessage);
                    }
                }

                var minWidthConfig = service.GetAll().Where(s => s.ConfigurationTypeId == (int)Constants.ConfigurationType.MinImgWidth).FirstOrDefault();

                if (minWidthConfig != null)
                {
                    int minWidth = int.Parse(minWidthConfig.Value);

                    if (image.Width < minWidth)
                    {
                        ErrorMessage = $"Image is too small, minimum allowed width is: {minWidth}px";

                        return new ValidationResult(ErrorMessage);
                    }
                }
            }

            var maxSizeConfig = service.GetAll().Where(s => s.ConfigurationTypeId == (int)Constants.ConfigurationType.MaxDataSize).FirstOrDefault();

            if (maxSizeConfig != null)
            {
                int maxSize = int.Parse(maxSizeConfig.Value);

                if ((file.Length / (1024 * 1024)) > maxSize)
                {
                    ErrorMessage = $"File is too large, maximum allowed is: {maxSize} MB";

                    return new ValidationResult(ErrorMessage);
                }
            }

            string[] allowedFileExtensions = service.GetAll().Where(s => s.ConfigurationTypeId == (int)Constants.ConfigurationType.Extension).Select(t => t.Value).ToArray();

            if (allowedFileExtensions != null)
            {
                if (!allowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
                {
                    ErrorMessage = "Please upload file of type: " + string.Join(", ", allowedFileExtensions);

                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;//true;
        }
    }
}

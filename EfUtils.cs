using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFundSolution.Utils.EntityFramworks
{

    public class ValidateDateFormat : ValidationAttribute
    {
        public ValidateDateFormat(int minLength, params string[] propertyNames)
        {
            this.PropertyNames = propertyNames;
            this.MinLength = minLength;
        }

        public string[] PropertyNames { get; private set; }
        public int MinLength { get; private set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var properties = this.PropertyNames.Select(validationContext.ObjectType.GetProperty);
            var values = properties.Select(p => p.GetValue(validationContext.ObjectInstance, null)).OfType<string>();
            var totalLength = values.Sum(x => x.Length) + Convert.ToString(value).Length;
            if (totalLength < this.MinLength)
            {
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }
            return null;
        }
    }


    public sealed class checkCountry : ValidationAttribute
    {
        public String AllowCountry { get; set; }

        protected override ValidationResult IsValid(object country, ValidationContext validationContext)
        {
            string[] myarr = AllowCountry.ToString().Split(',');
            if (myarr.Contains(country))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Please choose a valid country eg.(India,Pakistan,Nepal");
            }
        }
    }

    public sealed class xxx : ValidationAttribute
    {

        protected override ValidationResult IsValid(object xxx, ValidationContext validationContext)
        {
           return new ValidationResult("Please choose a valid country eg.(India,Pakistan,Nepal");
        }
    }



}

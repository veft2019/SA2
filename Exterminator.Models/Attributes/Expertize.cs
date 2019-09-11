using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Exterminator.Models.Attributes
{
    public sealed class Expertize : ValidationAttribute
    {
        public string validExpertize = "Ghost catcher, Ghoul strangler, Monster encager, Zombie exploder";
        protected override ValidationResult IsValid(object expertize, ValidationContext validationContext)
        {
            string[] arr = validExpertize.ToString().Split(',');
            // If expertize is valid against the validExpertize
            if(arr.Contains(expertize))
            {
                return ValidationResult.Success;
            }
            else 
            {
                return new ValidationResult("Expertize not right, please choose the following: Ghost catcher, Ghoul stranger, Monster encager, Zombie exploder");
            }
        }
    }
}
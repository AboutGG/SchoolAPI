﻿using System.ComponentModel.DataAnnotations;


namespace SchoolAPI.Utils
{
    public class StringValidator : ValidationAttribute
    {
        
        private int lenght;
        public StringValidator(int lenght) { 
            this.lenght = lenght;
        }
        
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            return Convert.ToString(value).Length >= this.lenght ? ValidationResult.Success : new ValidationResult($"Attribute lenght can't be less then {lenght}.");
        }
    }
}

using System;

namespace Exterminator.Models.Exceptions
{
    public class ModelFormatException: Exception
    {
        public ModelFormatException() : base("The model was not correctly formatted") {}
        public ModelFormatException(string message) : base(message) {}
        public ModelFormatException(string message, Exception inner) : base(message, inner) {} 
    }
}
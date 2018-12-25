using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace api.Application.NotificationPattern
{
    public class Notification
    {

        private List<Error> errors = new List<Error>();

        public void addError(String message)
        {
            addError(message, null);
        }

        public void addError(String message, Exception e)
        {
            errors.Add(new Error(message, e));
        }

        public String ErrorMessage()
        {
            return string.Join(",", errors.Select(error => error.getMessage()));
        }

        public String errorMessage(String separator)
        {
            return string.Join(separator, errors.Select(error => error.getMessage()));
        }

        public bool HasErrors()
        {
            return errors.Count > 0;
        }

        public List<Error> getErrors()
        {
            return errors;
        }

        public void setErrors(List<Error> errors)
        {
            this.errors = errors;
        }
    }
}

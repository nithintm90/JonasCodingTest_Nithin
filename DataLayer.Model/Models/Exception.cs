using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Models
{

    public class CustomException : Exception
    {
        public virtual int ErrorCode { get; set; }
        public virtual string ErrorDescription { get; set; }
    }

    public class BusinessException : CustomException
    {
        public BusinessException(int errorCode, string errorDescription)
            : base()
        {
            base.ErrorCode = errorCode;
            base.ErrorDescription = errorDescription;
        }
    }

    public class TechnicalException : CustomException
    {
        public TechnicalException(string errorDescription)
            : base()
        {
            base.ErrorDescription = errorDescription;
        }
    }
}

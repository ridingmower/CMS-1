using CMS.Common.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Core.Services.Interfaces
{
    public class BaseService : IService
    {
        public BaseService()
        {
            this.UnitOfWork = new UnitOfWork();
        }
        public UnitOfWork UnitOfWork { get; set; }
        public ServiceResult Result { get; set; }

        void SetResultSuccess(string message)
        {
            this.Result = new ServiceResult { IsSuccess = true, Message = message };

        }
        void SetResultSuccess(string message,object data)
        {
            this.Result = new ServiceResult { IsSuccess = true, Message = message,Data=data };

        }
        void SetResultFail(string message,object data)
        {
            this.Result = new ServiceResult { IsSuccess = false, Message = message, Data = data };

        }
        void SetResultFail(string message)
        {
            this.Result = new ServiceResult { IsSuccess = false, Message = message };
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDesign.Api.Models
{
    public class BaseResponseModel<T> where T : class
    {
        public BaseResponseModel(string responseMessage)
        {
            this.ResponseMessage = responseMessage;
        }
        public BaseResponseModel()
        {
            this.Links = new List<LinkModel>();
        }
        public BaseResponseModel(T model)
        {
            this.Response = model;
            this.Links = new List<LinkModel>();
        }
        public T Response { get; set; }
        public List<LinkModel> Links { get; set; }
        public string ResponseMessage { get; set; }
    }

    public class BaseResponseModel
    {
        public BaseResponseModel(string responseMessage)
        {
            this.ResponseMessage = responseMessage;
        }
        public BaseResponseModel()
        {
            this.Links = new List<LinkModel>();
        }
        public List<LinkModel> Links { get; set; }
        public string ResponseMessage { get; set; }
    }
}

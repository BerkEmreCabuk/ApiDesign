using ApiDesign.Api.Enums;
using System;

namespace ApiDesign.Api.Models
{
    public abstract class BaseModel
    {
        public long Id { get; set; }
        public Status Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
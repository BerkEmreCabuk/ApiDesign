using ApiDesign.Api.Common;
using ApiDesign.Api.Enums;
using ApiDesign.Api.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDesign.Api.Models.ResponseModels
{
    public class CategoryResponseModel : BaseModel
    {
        public CategoryResponseModel(string name)
        {
            Id = CommonSeedModel.IdNext++;
            this.Name = name;
            this.Description = $"{name} Açıklaması";
            this.Status = Status.ACTIVE;
            CreateDate = DateTime.Now;
            UpdateDate = null;
        }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}

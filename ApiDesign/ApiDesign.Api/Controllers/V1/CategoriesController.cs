using ApiDesign.Api.Common;
using ApiDesign.Api.Enums;
using ApiDesign.Api.Models;
using ApiDesign.Api.Models.RequestModels;
using ApiDesign.Api.Models.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace ApiDesign.Api.Controllers.V1
{
    /// <summary>
    /// Endpointler farklı örnekler olması için oluşturuldu.
    /// Bazı endpointler business bir işlem gerçekleştirmeyip 
    /// sadece örnek amaçlı response dönmektedir.
    /// Versiyon
    /// Response Model
    /// Url Standartları
    /// üzerine örnekler bulunmaktadır
    /// </summary>
    [ApiVersion("0.9", Deprecated = true)]
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class CategoriesController : ControllerBase
    {
        private readonly LinkGenerator _linkGenerator;
        public CategoriesController(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }

        [HttpGet("{id:long}", Name = nameof(Get))]
        [ProducesResponseType(typeof(BaseResponseModel<CategoryResponseModel>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Get(long id)
        {
            try
            {
                var category = CommonSeedModel.CategoriesV1.FirstOrDefault(x => x.Id == id);
                if (category == null)
                    return NotFound(new BaseResponseModel("Kategori bulunamadı."));

                var response = new BaseResponseModel<CategoryResponseModel>(category);
                return Ok(this.CreateLinksForCategory(response));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet(Name = nameof(GetList))]
        [ProducesResponseType(typeof(BaseResponseModel<List<CategoryResponseModel>>), 200)]
        [ProducesResponseType(400)]
        public IActionResult GetList()
        {
            try
            {
                var response = new BaseResponseModel<List<CategoryResponseModel>>(CommonSeedModel.CategoriesV1);
                return Ok(this.CreateLinksForCategory(response));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet(Name = nameof(GetList_Version1_1))]
        [ApiVersion("1.1")]
        [ProducesResponseType(typeof(BaseResponseModel<List<CategoryResponseModel>>), 200)]
        [ProducesResponseType(400)]
        public IActionResult GetList_Version1_1()
        {
            try
            {
                var response = new BaseResponseModel<List<CategoryResponseModel>>(CommonSeedModel.CategoriesV1);
                return Ok(this.CreateLinksForCategory(response));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost(Name = nameof(Create))]
        [ProducesResponseType(typeof(BaseResponseModel<CategoryResponseModel>), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        public IActionResult Create([FromBody] CategoryCreateRequestModel model)
        {
            try
            {
                var isExist = CommonSeedModel.CategoriesV1.Exists(x => x.Name == model.Name && x.Status == Status.ACTIVE);
                if (isExist)
                    return UnprocessableEntity(new BaseResponseModel("Aynı isimde aktif bir kategori bulunmaktadır."));

                var category = new CategoryResponseModel(model.Name);
                CommonSeedModel.CategoriesV1.Add(category);
                var response = new BaseResponseModel<CategoryResponseModel>(category);
                return Created(Request.QueryString.ToString(), this.CreateLinksForCategory(response));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut(Name = nameof(Update))]
        [ProducesResponseType(typeof(BaseResponseModel<CategoryResponseModel>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult Update([FromBody] CategoryUpdateRequestModel model)
        {
            try
            {
                var category = CommonSeedModel.CategoriesV1.FirstOrDefault(x => x.Id == model.Id);
                if (category == null)
                    return NotFound(new BaseResponseModel("Kategori bulunamadı."));

                category.Name = model.Name;
                category.UpdateDate = DateTime.Now;

                var response = new BaseResponseModel<CategoryResponseModel>(category);
                return Ok(this.CreateLinksForCategory(response));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("follow/{id:long})", Name = nameof(Follow))]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(404)]
        public IActionResult Follow(long id)
        {
            try
            {
                var isExist = CommonSeedModel.CategoriesV1.Exists(x => x.Id == id);
                if (!isExist)
                    return NotFound(new BaseResponseModel("Kategori bulunamadı."));
                return Ok(new BaseResponseModel("Kategori takip edildi."));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("all-follow", Name = nameof(AllFollow))]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(404)]
        public IActionResult AllFollow()
        {
            try
            {
                var isExist = CommonSeedModel.CategoriesV1.Exists(x => x.Status == Status.ACTIVE);
                if (!isExist)
                    return NotFound(new BaseResponseModel("Kategoriler bulunamadı."));
                return Ok(new BaseResponseModel("Kategoriler takip edildi."));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("unfollow/{id:long}", Name = nameof(Unfollow))]
        [ApiVersion("1.1")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(404)]
        public IActionResult Unfollow(long id)
        {
            try
            {
                var isExist = CommonSeedModel.CategoriesV1.Exists(x => x.Id == id && x.Status == Status.ACTIVE);
                if (!isExist)
                    return NotFound(new BaseResponseModel("Kategori bulunamadı."));
                return Ok(new BaseResponseModel("Kategori takipten çıkarıldı."));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("{id:long}", Name = nameof(Delete))]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult Delete(long id)
        {
            try
            {
                var category = CommonSeedModel.CategoriesV1.FirstOrDefault(x => x.Id == id);
                if (category == null)
                    return NotFound(new BaseResponseModel($"{id} id'li kategori bulunamadı."));

                CommonSeedModel.CategoriesV1.Remove(category);
                return Ok(new BaseResponseModel("Kategori silindi."));
            }
            catch (Exception)
            {

                throw;
            }
        }

        private BaseResponseModel<CategoryResponseModel> CreateLinksForCategory(BaseResponseModel<CategoryResponseModel> model)
        {
            if (!HttpContext.Request.RouteValues.TryGetValue("version", out var version))
                version = ApiVersion.Default;

            model.Links.Add(
                new LinkModel
                {
                    Href = _linkGenerator.GetPathByAction(HttpContext, nameof(Get), "Categories", new { version = version.ToString(), id = model.Response.Id }),
                    Method = HttpMethod.Get.ToString()
                });
            model.Links.Add(
                new LinkModel
                {
                    Href = _linkGenerator.GetPathByAction(HttpContext, nameof(GetList), "Categories", new { version = version.ToString() }),
                    Method = HttpMethod.Get.ToString()
                });
            model.Links.Add(
            new LinkModel
            {
                Href = _linkGenerator.GetPathByAction(HttpContext, nameof(Create), "Categories", new { version = version.ToString() }),
                Method = HttpMethod.Post.ToString()
            });
            model.Links.Add(
            new LinkModel
            {
                Href = _linkGenerator.GetPathByAction(HttpContext, nameof(Update), "Categories", new { version = version.ToString() }),
                Method = HttpMethod.Put.ToString()
            });
            model.Links.Add(
            new LinkModel
            {
                Href = _linkGenerator.GetPathByAction(HttpContext, nameof(Follow), "Categories", new { version = version.ToString(), id = model.Response.Id }),
                Method = HttpMethod.Put.ToString()
            });
            model.Links.Add(
            new LinkModel
            {
                Href = _linkGenerator.GetPathByAction(HttpContext, nameof(AllFollow), "Categories", new { version = version.ToString() }),
                Method = HttpMethod.Put.ToString()
            });
            model.Links.Add(
            new LinkModel
            {
                Href = _linkGenerator.GetPathByAction(HttpContext, nameof(Delete), "Categories", new { version = version.ToString(), id = model.Response.Id }),
                Method = HttpMethod.Delete.ToString()
            });
            return model;
        }
        private BaseResponseModel<List<CategoryResponseModel>> CreateLinksForCategory(BaseResponseModel<List<CategoryResponseModel>> model)
        {
            model.Links = CreateLinksForCategory(new BaseResponseModel<CategoryResponseModel> { Response = model.Response.FirstOrDefault(), Links = model.Links }).Links;
            return model;
        }
    }
}

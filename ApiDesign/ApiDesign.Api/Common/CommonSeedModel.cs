using ApiDesign.Api.Models.ResponseModels;
using System.Collections.Generic;

namespace ApiDesign.Api.Common
{
    public static class CommonSeedModel
    {
        public static long IdNext { get; set; } = 1;

        public static List<CategoryResponseModel> CategoriesV1 = new List<CategoryResponseModel>()
            {
                new CategoryResponseModel("Bilgisayar V1"),
                new CategoryResponseModel("Akıllı Telefon V1"),
                new CategoryResponseModel("Beyaz Eşya V1"),
                new CategoryResponseModel("Oyun Konsolu V1")
            };

        public static List<CategoryResponseModel> CategoriesV2 = new List<CategoryResponseModel>()
            {
                new CategoryResponseModel("Bilgisayar V2"),
                new CategoryResponseModel("Akıllı Telefon V2"),
                new CategoryResponseModel("Beyaz Eşya V2"),
                new CategoryResponseModel("Oyun Konsolu V2")
            };
    }

}

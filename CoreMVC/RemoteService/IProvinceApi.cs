using CoreMVC.Domain.Entity;
using CoreMVC.Domain.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.Attributes;

namespace CoreMVC.RemoteService
{
    [TraceFilter]
    [HttpHost(host: "http://localhost:5100")]
    public interface IProvinceApi : IHttpApi
    {
        [HttpGet(path: "/api/province/findprovincebyid/{id}")]
        [JsonReturn]
        ITask<ApiResponse<Province>> FindProvinceById(long id);

        [HttpPost(path: "/api/province/addprovince")]
        ITask<ApiResponse<bool>> AddProvince([Required, JsonContent] Province province);
    }
}

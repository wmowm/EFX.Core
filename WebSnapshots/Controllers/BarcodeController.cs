using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using WebSnapshots.Models;

namespace WebSnapshots.Controllers
{

    public class BarcodeController : ApiController
    {

        [HttpPost]
        public JsonResult<BaseResponse> Snapshot([FromBody]string url)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                Bitmap m_Bitmap = WebSnapshotsHelper.GetWebSiteThumbnail(url, 414, 736, 414, 736); //宽高根据要获取快照的网页决定
                var name = $"/upload/{Guid.NewGuid()}.png";
                string path = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + name;

                m_Bitmap.Save(path, System.Drawing.Imaging.ImageFormat.Png); //图片格式可以自由控制
                response.code = 200;
                response.msg = "成功";
                response.data = name;
            }
            catch (Exception)
            {
                response.code = 500;
                response.msg = "服务端错误";
            }
            return Json(response);
        }

    }




}

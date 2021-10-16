using Application.Models;
using System.Web.Mvc;

namespace Application.Controllers
{
    public class BoardController : Controller
    {
        public ActionResult List()
        {          
            DocumentActs documentActs = new DocumentActs();
            MemberActs memberActs = new MemberActs();

            var documents = documentActs.GetDocuments();
            var member = memberActs.GetMember(1);

            //ViewBag.Member = member;
            ViewData["Member"] = member;

            return View(documents); // 뷰에 하나의 모델만 전달 가능
        }
    }
}
#pragma checksum "C:\Users\rlaeh\source\repos\geun25\ASP.NET\Core\Core.Web\Views\Membership\Forbidden.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "32dd362186a6238f2f93a7f46ae39516a32d58c4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Membership_Forbidden), @"mvc.1.0.view", @"/Views/Membership/Forbidden.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\rlaeh\source\repos\geun25\ASP.NET\Core\Core.Web\Views\_ViewImports.cshtml"
using Core.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\rlaeh\source\repos\geun25\ASP.NET\Core\Core.Web\Views\_ViewImports.cshtml"
using Core.Web.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\rlaeh\source\repos\geun25\ASP.NET\Core\Core.Web\Views\_ViewImports.cshtml"
using Core.Data.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"32dd362186a6238f2f93a7f46ae39516a32d58c4", @"/Views/Membership/Forbidden.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"afb5944693e31e3744963962d606c5b070ad52dc", @"/Views/_ViewImports.cshtml")]
    public class Views_Membership_Forbidden : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\rlaeh\source\repos\geun25\ASP.NET\Core\Core.Web\Views\Membership\Forbidden.cshtml"
  
    ViewData["Title"] = "접근권한 불가입니다.";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>ViewData[\"Title\"]</h1>\r\n\r\n<div class=\"text-danger\">");
#nullable restore
#line 9 "C:\Users\rlaeh\source\repos\geun25\ASP.NET\Core\Core.Web\Views\Membership\Forbidden.cshtml"
                    Write(Html.Raw(ViewData["Message"]));

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
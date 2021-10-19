using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LibraryApplication.Library
{
    public class CommonLibrary
    {
        public string GetUrl(string paramType)
        {
            string returnValue = string.Empty;

            switch (paramType)
            {
                case "full":
                    returnValue = HttpContext.Current.Request.Url.AbsoluteUri;
                    break;
                case "path":
                    returnValue = HttpContext.Current.Request.Url.AbsolutePath;
                    break;
            }

            return returnValue;
        }

        public List<UrlParameter> UrlParameters
        {
            get
            {
                var returnValue = new List<UrlParameter>();

                string url = this.GetUrl("full");
                // [0] https://localhost:44337/
                // [1] searchKind=Publisher&keyword=%EC%B6%9C%ED%8C%90

                string[] urlArr = url.Split('?');
                string[] paramArr = null;

                if (urlArr.Count() > 0)
                {
                    paramArr = urlArr[1].Split('&');
                    // [0] searchKind=Publisher
                    // [1] keyword=%EC%B6%9C%ED%8C%90

                    foreach (var item in paramArr)
                    {
                        var urlParam = new UrlParameter()
                        {
                            Key = item.Split('=')[0],
                            Value = item.Split('=')[1]
                        };
                    }
                }

                return returnValue;
            }
        }
        public string AddUrlParameter(string paramKey, string paramValue)
        {
            string returnValue = string.Empty;

            List<UrlParameter> urlParams = this.UrlParameters;
            //중복이 있는경우 삭제
            UrlParameter urlParameter = urlParams.Where(x => x.Key == paramKey).SingleOrDefault();

            if (urlParameter != null)
                urlParams.Remove(urlParameter);

            urlParams.Add(new UrlParameter()
            {
                Key = paramKey,
                Value = paramValue
            });

            // [0] Key = alpha, Value = alphaValue
            // [1] Key = beta, Value = betaValue
            // [2] Key = gamma, Value = gammaValue

            for(int i = 0; i < urlParams.Count(); i++)
            {
                returnValue += (i == 0) ? "?" : "&";
                returnValue += urlParams[i].Key + "=" + urlParams[i].Value;

                //?alpha=alphaValue&beta=betaValue&gamma=gammaValue 
            }
            
            return returnValue;
        }

    }
}

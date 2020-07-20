using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RList
{
    class RMain
    {
        public string getHttp(string url)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            var response = (System.Net.HttpWebResponse)request.GetResponse();
            var responseString = new System.IO.StreamReader(response.GetResponseStream()).ReadToEnd();
            response.Close();
            return responseString;
        }
    }
}

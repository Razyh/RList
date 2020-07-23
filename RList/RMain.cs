using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RList
{
    public class GetHttp
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
    public class RMain
    {
        /// <summary>
        /// GET запрос
        /// </summary>
        /// <param name="url">Ссылка</param>
        /// <returns></returns>

        public class SRanobe
        {
            //https://ranobehub.org/api/ranobe/getByName/fff
            public class SRdata
            {
                public int id { get; set; }
                public Ranobe.Rnames names { get; set; }
                public string image { get; set; }
                public string url { get; set; }
                public string description { get; set; }
            }
            public List<SRdata> data { get; set; }
            public SRanobe Search(string SText)
            {
                var url = $"https://ranobehub.org/api/ranobe/getByName/{SText}";
                var resutl = (new GetHttp()).getHttp(url);
                this.data = JsonConvert.DeserializeObject<SRanobe>(resutl).data;
                return this;
            }
        }
        public class Ranobe
        {
            public class RVolumes
            {
                public class RChapters
                {
                    public string changed_at { get; set; }
                    public int id { get; set; }
                    public string name { get; set; }
                    public int num { get; set; }
                }
                public int id { get; set; }
                public string name { get; set; }
                public int num { get; set; }
                public List<RChapters> chapters { get; set; }
            }
            public class Rnames
            {
                public string eng { get; set; }
                public string original { get; set; }
                public string rus { get; set; }
            }
            public Rnames names { get; set; }
            public int id { get; set; }
            public List<RVolumes> volumes { get; set; }
            public int chapters { get; set; }
            public int RededChapters { get; set; }
        }
        public List<Ranobe> Ranobes { get; set; }
        int getChapters(List<Ranobe.RVolumes> volumes)
        {
            var chapters = 0;
            /*
            var c = volumes.Last().chapters;
            int RededChapters = 0;
            for (int i = 0; i < c.Count; i++)
            {
                var g = c[(c.Count - 1) - i].name.ToLower();
                if (g.IndexOf("глава") != -1)
                {
                    RededChapters = Convert.ToInt32(g.Replace("глава ", "").Split(".".ToCharArray())[0]);
                    break;
                }
            }
            */
            foreach (var item in volumes)
            {
                chapters += item.chapters.Last().num;
            }
            return chapters;
        }
        public void Update()
        {
            foreach (var item in this.Ranobes)
            {
                var url = $"https://ranobehub.org/api/ranobe/{item.id}/contents";
                var r = JsonConvert.DeserializeObject<Ranobe>((new GetHttp()).getHttp(url));
                var ranobe = this.Ranobes.Find(x => x.id == item.id);
                if (ranobe != null)
                {
                    var id = this.Ranobes.IndexOf(ranobe);
                    var oldChapters = this.Ranobes[id].chapters;
                    this.Ranobes[id].volumes = r.volumes;
                    this.Ranobes[id].chapters = getChapters(r.volumes);
                    if (this.Ranobes[id].chapters > oldChapters)
                    {
                        new Notif.NotifC().createNotif("New chapres", $"Finded {this.Ranobes[id].chapters - oldChapters} chapters", "RList");
                    }
                }
            }
        }
        public RMain GetRanobe(int id, Ranobe.Rnames names)
        {
            //https://ranobehub.org/api/ranobe/494/contents
            var url = $"https://ranobehub.org/api/ranobe/{id}/contents";
            for (int i = 0; i < this.Ranobes.Count; i++)
            {
                if (this.Ranobes[i].id==id) {
                    this.Ranobes[i].chapters = getChapters(this.Ranobes[i].volumes);
                    return this;
                }
            }
            var resutl = (new GetHttp()).getHttp(url);
            var r = JsonConvert.DeserializeObject<Ranobe>(resutl);
            r.names = names;
            r.id = id;
            r.chapters = getChapters(r.volumes);
            this.Ranobes.Add(r);
            return this;
        }
        public RMain init()
        {
            var s = Properties.Settings.Default.Ranobe;
            if (s != "")
            {
                this.Ranobes = JsonConvert.DeserializeObject<List<Ranobe>>(s);
            }
            else
            {
                this.Ranobes = new List<Ranobe>();
            }
            return this;
        }
        public void Remove(Ranobe ranobe)
        {
            int id = this.Ranobes.IndexOf(ranobe);
            this.Ranobes.RemoveAt(id);
        }
        public void Save()
        {
            var r = JsonConvert.SerializeObject(this.Ranobes);
            Properties.Settings.Default.Ranobe = r;
            Properties.Settings.Default.Save();
        }
    }
}

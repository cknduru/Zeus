using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zeus.Models;

namespace Zeus
{
    public static class ApiUtils
    {
        public static ZResponse getNodeInfo(String apiAddress)
        {
            using (var wc = new System.Net.WebClient())
            {
                // get CVR entry from remote API
                wc.Headers["User-Agent"] = "CvrLookup 0.1";
                String json = wc.DownloadString(apiAddress);

                // deserialize raw data into model data structure
                ZResponse zr = Newtonsoft.Json.JsonConvert.DeserializeObject<ZResponse>(json);

                return zr;
            }
        }

        public static List<ZResponse> getNodesInfo()
        {
            List<String> ipList = new List<String> { "http://192.168.87.160:5000/node_info", };
            List<ZResponse> zList = new List<ZResponse>();

            foreach (var nodeAddr in ipList)
            {
                zList.Add(getNodeInfo(nodeAddr));
            }

            return zList;
        }
    }
}

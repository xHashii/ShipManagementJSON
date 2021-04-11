using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
public class Data
{
    public string PortName { get; set; }
    public string PortNumber { get; set; }
    public string Country { get; set; }
    public string Coordinates { get; set; }
    public int Shipsonport { get; set; }

    [JsonProperty("Un/locode")]
    public string UnLocode { get; set; }
    public string IMO {get; set;}
    public string shipName { get; set; }
    public string shipType {get; set;}
}

public class Root
    {
        public List<Data> data { get; set; }
    }



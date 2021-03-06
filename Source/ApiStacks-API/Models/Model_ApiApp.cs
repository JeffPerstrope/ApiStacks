﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiStacks
{
    public class Model_ApiApp
    {

        public string id { get; set; }
        public string name { get; set; }
        public string endpoint { get; set; }
        public string icon { get; set; }
        public string version { get; set; }
        public bool enabled { get; set; }
        public List<string> customization { get; set; }

        public List<KeyValuePair<string, object>> settings { get; set; }

        public Model_ApiApp()
        {
            customization = new List<string>();
        }
    }

    
}
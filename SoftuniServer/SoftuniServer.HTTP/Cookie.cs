﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SoftuniServer.HTTP
{
    public class Cookie
    {
        public Cookie(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }
        public Cookie(string cookieAsString)
        {
            var cookieParts = cookieAsString.Split(new string[] {"="},2 ,StringSplitOptions.None);
            this.Name = cookieParts[0];
            this.Value = cookieParts[1];
        }
        public string Name { get; set; }
        public string Value { get; set; }
        public override string ToString()
        {
            return $"{this.Name}: {this.Value}";
        }
    }
}

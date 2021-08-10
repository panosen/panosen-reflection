using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Panosen.Reflection.Model
{
    public class XmlDoc
    {
        public XmlAssembly Assembly { get; set; }

        public List<XmlMember> Members { get; set; }
    }
}
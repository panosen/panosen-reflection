using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Panosen.Reflection.Model
{
    /// <summary>
    /// XmlDoc
    /// </summary>
    public class XmlDoc
    {
        /// <summary>
        /// Assembly
        /// </summary>
        public XmlAssembly Assembly { get; set; }

        /// <summary>
        /// Members
        /// </summary>
        public List<XmlMember> Members { get; set; }
    }
}
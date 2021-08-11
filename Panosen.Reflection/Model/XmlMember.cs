using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Panosen.Reflection.Model
{
    /// <summary>
    /// XmlMember
    /// </summary>
    public class XmlMember
    {
        /// <summary>
        /// Summary
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Param
        /// </summary>
        public List<XmlMemberParam> Param { get; set; }

        /// <summary>
        /// Returns
        /// </summary>
        public string Returns { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
    }
}

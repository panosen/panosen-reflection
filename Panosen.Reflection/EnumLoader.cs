using Panosen.Reflection.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Panosen.Reflection
{
    /// <summary>
    /// EnumLoader
    /// </summary>
    public class EnumLoader
    {
        /// <summary>
        /// LoadEnum
        /// </summary>
        public static EnumNode LoadEnum(Type type, List<XmlMember> xmlMembers)
        {
            var enumNode = new EnumNode();

            enumNode.Name = type.Name;

            var classMember = xmlMembers != null ? xmlMembers.FirstOrDefault(v => string.Format("T:{0}", type.FullName).Equals(v.Name)) : null;
            if (classMember != null)
            {
                enumNode.Summary = classMember.Summary;
            }

            return enumNode;
        }
    }
}
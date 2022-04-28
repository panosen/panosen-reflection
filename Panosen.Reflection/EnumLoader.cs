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
            enumNode.Namespace = type.Namespace;

            var classMember = xmlMembers != null ? xmlMembers.FirstOrDefault(v => string.Format("T:{0}", type.FullName).Equals(v.Name)) : null;
            if (classMember != null)
            {
                enumNode.Summary = classMember.Summary;
            }

            enumNode.FieldNodeList = new List<FieldNode>();

            var declaredFields = (type as System.Reflection.TypeInfo).DeclaredFields.ToList();
            for (int i = 0; i < declaredFields.Count; i++)
            {
                var declaredField = declaredFields[i];

                var fieldNode = new FieldNode();
                fieldNode.Name = declaredField.Name;
                fieldNode.FieldType = declaredFields[0].FieldType;

                var fieldMember = xmlMembers != null ? xmlMembers.FirstOrDefault(v => string.Format("F:{0}.{1}", declaredField.DeclaringType.FullName, declaredField.Name).Equals(v.Name)) : null;
                if (fieldMember != null)
                {
                    fieldNode.Summary = fieldMember.Summary;
                }

                if (i>0)
                {
                    fieldNode.FieldValue = (int)declaredField.GetValue(declaredField);
                }

                enumNode.FieldNodeList.Add(fieldNode);
            }

            return enumNode;
        }
    }
}
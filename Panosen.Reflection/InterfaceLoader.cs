using Panosen.Reflection.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Panosen.Reflection
{
    /// <summary>
    /// LoadClass
    /// </summary>
    public static class InterfaceLoader
    {
        /// <summary>
        /// LoadClass
        /// </summary>
        public static InterfaceNode LoadInterface(Type type, List<XmlMember> xmlMembers = null)
        {
            var interfaceNode = new InterfaceNode();

            interfaceNode.InterfaceType = type;
            interfaceNode.Name = type.Name;
            interfaceNode.FullName = type.FullName;
            interfaceNode.Namespace = type.Namespace;
            interfaceNode.Attributes = type.GetCustomAttributes().ToList();

            var classMember = xmlMembers != null ? xmlMembers.FirstOrDefault(v => string.Format("T:{0}", type.FullName).Equals(v.Name)) : null;
            if (classMember != null)
            {
                interfaceNode.Summary = classMember.Summary;
            }

            //BaseType
            if (type.BaseType != null && type.BaseType != typeof(object))
            {
                interfaceNode.BaseTypeName = type.BaseType.Name;
                interfaceNode.BaseTypeFullName = type.BaseType.FullName;
            }

            //Methods
            var methods = type.GetMembers()
                .Where(v => v.MemberType == MemberTypes.Method)
                .Where(v => v.DeclaringType == type)
                .Select(v => (MethodInfo)v)
                .Where(v => v.IsPublic && !v.IsSpecialName)
                .ToArray();
            var methodNodes = LoadMethods(methods, xmlMembers);
            if (methodNodes != null && methodNodes.Count > 0)
            {
                foreach (var methodNode in methodNodes)
                {
                    interfaceNode.AddMethod(methodNode);
                }
            }

            return interfaceNode;
        }

        private static List<MethodNode> LoadMethods(MethodInfo[] methods, List<XmlMember> xmlMembers)
        {
            List<MethodNode> returnValue = new List<MethodNode>();

            if (methods == null || methods.Length == 0)
            {
                return returnValue;
            }

            foreach (var method in methods)
            {
                var methodModel = new MethodNode();

                methodModel.Name = method.Name;

                {
                    methodModel.ReturnType = method.ReturnType;
                    var parameters = method.GetParameters();
                    if (parameters.Length > 0)
                    {
                        methodModel.Parameters = new List<MethodParameter>();
                        foreach (var parameter in parameters)
                        {
                            var methodParameter = new MethodParameter();

                            methodParameter.Name = parameter.Name;
                            methodParameter.ParameterType = parameter.ParameterType;

                            methodModel.Parameters.Add(methodParameter);
                        }
                    }

                    var xmlMemberName = string.Format("M:{0}.{1}", method.DeclaringType.FullName, method.Name);
                    if (methodModel.Parameters != null && methodModel.Parameters.Count > 0)
                    {
                        xmlMemberName = string.Concat(xmlMemberName, "(", string.Join(",", methodModel.Parameters.Select(v => v.ParameterType.FullName)), ")");
                    }
                    var methodMember = xmlMembers != null ? xmlMembers.FirstOrDefault(v => xmlMemberName.Equals(v.Name)) : null;
                    if (methodMember != null)
                    {
                        methodModel.Summary = methodMember.Summary;
                    }
                }

                returnValue.Add(methodModel);
            }

            return returnValue;
        }
    }
}

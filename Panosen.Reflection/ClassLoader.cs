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
    public static class ClassLoader
    {
        /// <summary>
        /// LoadClass
        /// </summary>
        public static ClassNode LoadClass(Type type, List<XmlMember> xmlMembers = null)
        {
            var classNode = new ClassNode();

            classNode.ClassType = type;
            classNode.Name = type.Name;
            classNode.FullName = type.FullName;
            classNode.Namespace = type.Namespace;
            classNode.IsAbstract = type.IsAbstract;
            classNode.Attributes = type.GetCustomAttributes().ToList();

            var classMember = xmlMembers != null ? xmlMembers.FirstOrDefault(v => string.Format("T:{0}", type.FullName).Equals(v.Name)) : null;
            if (classMember != null)
            {
                classNode.Summary = classMember.Summary;
            }

            //BaseType
            if (type.BaseType != null && type.BaseType != typeof(object))
            {
                classNode.BaseTypeName = type.BaseType.Name;
                classNode.BaseTypeFullName = type.BaseType.FullName;
            }

            //Properties
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            var propertyNodes = ToPropertyNodeList(properties, xmlMembers);
            if (propertyNodes != null && propertyNodes.Count > 0)
            {
                foreach (var propertyNode in propertyNodes)
                {
                    classNode.AddProperty(propertyNode);
                }
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
                    classNode.AddMethod(methodNode);
                }
            }

            return classNode;
        }

        private static List<PropertyNode> ToPropertyNodeList(PropertyInfo[] properties, List<XmlMember> xmlMembers)
        {
            List<PropertyNode> returnValue = new List<PropertyNode>();

            if (properties == null || properties.Length == 0)
            {
                return returnValue;
            }

            foreach (var property in properties)
            {
                PropertyNode propertyNode = ToPropertyNode(property, xmlMembers);

                returnValue.Add(propertyNode);
            }

            return returnValue;
        }

        private static PropertyNode ToPropertyNode(PropertyInfo property, List<XmlMember> xmlMembers)
        {
            var propertyNode = new PropertyNode();

            propertyNode.Name = property.Name;
            propertyNode.PropertyType = property.PropertyType;
            propertyNode.Attributes = property.GetCustomAttributes().ToList();

            var propertyMember = xmlMembers != null ? xmlMembers.FirstOrDefault(v => string.Format("P:{0}.{1}", property.DeclaringType.FullName, property.Name).Equals(v.Name)) : null;
            if (propertyMember != null)
            {
                propertyNode.Summary = propertyMember.Summary;
            }

            return propertyNode;
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
                    methodModel.Attributes = method.Attributes;
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

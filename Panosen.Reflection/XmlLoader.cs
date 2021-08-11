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
    /// XmlLoader
    /// </summary>
    public static class XmlLoader
    {
        /// <summary>
        /// LoadXml
        /// </summary>
        public static XmlDoc LoadXml(string xmlPath)
        {
            if (string.IsNullOrEmpty(xmlPath))
            {
                return null;
            }

            var element = XElement.Load(xmlPath);

            return LoadXml(element);
        }

        private static XmlDoc LoadXml(XElement document)
        {
            if (document == null)
            {
                return null;
            }

            XmlDoc returnValue = new XmlDoc();

            var assemblyElement = document.Element("assembly");
            if (assemblyElement != null)
            {
                returnValue.Assembly = new XmlAssembly();
                var nameElement = assemblyElement.Element("name");
                if (nameElement != null)
                {
                    returnValue.Assembly.Name = nameElement.Value;
                }
            }

            var members = document.Element("members");
            if (members != null)
            {
                returnValue.Members = ToXmlMembers(members);
            }

            return returnValue;
        }

        private static List<XmlMember> ToXmlMembers(XElement members)
        {
            var xmlMembers = new List<XmlMember>();

            var items = members.Elements();
            foreach (var member in items)
            {
                XmlMember xmlMember = ToXmlMember(member);

                xmlMembers.Add(xmlMember);
            }

            return xmlMembers;
        }

        private static XmlMember ToXmlMember(XElement member)
        {
            XmlMember xmlMember = new XmlMember();

            //Name
            xmlMember.Name = ToName(member);

            //Summary
            xmlMember.Summary = ToSummary(member);

            xmlMember.Param = ToXmlMemberParams(member);

            xmlMember.Returns = ToReturns(member);

            return xmlMember;
        }

        private static string ToName(XElement member)
        {
            var nameAttribute = member.Attribute("name");
            if (nameAttribute == null)
            {
                return null;
            }

            return nameAttribute.Value;
        }

        private static string ToSummary(XElement member)
        {
            var summaryElement = member.Element("summary");
            if (summaryElement == null)
            {
                return null;
            }

            if (string.IsNullOrEmpty(summaryElement.Value))
            {
                return null;
            }

            return summaryElement.Value.Trim();
        }

        private static List<XmlMemberParam> ToXmlMemberParams(XElement member)
        {
            var paramElements = member.Elements("param");
            if (paramElements == null)
            {
                return null;
            }

            var xmlMemberParamList = new List<XmlMemberParam>();
            foreach (var paramElement in paramElements)
            {
                var paramNameAttribute = paramElement.Attribute("name");
                var paramValueElement = paramElement.Value;
                if (paramNameAttribute == null || string.IsNullOrEmpty(paramNameAttribute.Value) || string.IsNullOrEmpty(paramValueElement))
                {
                    continue;
                }

                var xmlMemberParam = new XmlMemberParam();

                xmlMemberParam.Name = paramNameAttribute.Value;
                xmlMemberParam.Value = paramValueElement;

                xmlMemberParamList.Add(xmlMemberParam);
            }

            if (xmlMemberParamList.Count == 0)
            {
                return null;
            }

            return xmlMemberParamList;
        }

        private static string ToReturns(XElement member)
        {
            var returns = member.Element("returns");
            if (returns == null)
            {
                return null;
            }

            if (string.IsNullOrEmpty(returns.Value))
            {
                return null;
            }

            return returns.Value;
        }
    }
}

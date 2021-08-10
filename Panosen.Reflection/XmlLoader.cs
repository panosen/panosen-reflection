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
    public class XmlLoader
    {
        public static XmlDoc LoadXml(string xmlPath)
        {
            XmlDoc doc = null;

            if (!string.IsNullOrEmpty(xmlPath))
            {
                var element = XElement.Load(xmlPath);

                doc = LoadXml(element);
            }

            return doc;
        }

        static XmlDoc LoadXml(XElement document)
        {
            XmlDoc returnValue = new XmlDoc();

            if (document != null)
            {
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
                    returnValue.Members = ToMembers(members);
                }
            }

            return returnValue;
        }

        private static List<XmlMember> ToMembers(XElement members)
        {
            var xmlMembers = new List<XmlMember>();

            var items = members.Elements();
            foreach (var member in items)
            {
                XmlMember xmlMember = new XmlMember();

                //Name
                var nameAttribute = member.Attribute("name");
                if (nameAttribute != null)
                {
                    xmlMember.Name = nameAttribute.Value;
                }

                //Summary
                var summaryElement = member.Element("summary");
                if (summaryElement != null)
                {
                    if (!string.IsNullOrEmpty(summaryElement.Value))
                    {
                        xmlMember.Summary = summaryElement.Value.Trim();
                    }
                }

                var paramElements = member.Elements("param");
                if (paramElements != null)
                {
                    var xmlMemberParamList = new List<XmlMemberParam>();
                    foreach (var paramElement in paramElements)
                    {
                        var paramNameAttribute = paramElement.Attribute("name");
                        var paramValueElement = paramElement.Value;
                        if (paramNameAttribute != null && !string.IsNullOrEmpty(paramNameAttribute.Value) && !string.IsNullOrEmpty(paramValueElement))
                        {
                            var xmlMemberParam = new XmlMemberParam();

                            xmlMemberParam.Name = paramNameAttribute.Value;
                            xmlMemberParam.Value = paramValueElement;

                            xmlMemberParamList.Add(xmlMemberParam);
                        }
                    }
                    if (xmlMemberParamList.Count > 0)
                    {
                        xmlMember.Param = xmlMemberParamList.ToArray();
                    }
                }

                var returns = member.Element("returns");
                if (returns != null)
                {
                    if (!string.IsNullOrEmpty(returns.Value))
                    {
                        xmlMember.Returns = returns.Value;
                    }
                }

                xmlMembers.Add(xmlMember);
            }

            return xmlMembers;
        }
    }
}

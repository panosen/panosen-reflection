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
    /// LoadAssembly
    /// </summary>
    public static class AssemblyLoader
    {
        /// <summary>
        /// LoadAssembly
        /// </summary>
        public static AssemblyModel LoadAssembly(Assembly assembly, XmlDoc xmlDoc = null)
        {
            AssemblyModel mainModel = new AssemblyModel();

            var types = assembly.GetTypes();

            mainModel.ClassNodeList = LoadClassList(types, xmlDoc?.Members);

            mainModel.InterfaceNodeList = LoadInterfaceList(types, xmlDoc?.Members);

            mainModel.EnumNodeList = LoadEnumList(types, xmlDoc?.Members);

            return mainModel;
        }

        private static List<ClassNode> LoadClassList(Type[] types, List<XmlMember> xmlMembers)
        {
            List<ClassNode> classNodeList = new List<ClassNode>();

            foreach (var type in types)
            {
                if (type.IsClass)
                {
                    classNodeList.Add(ClassLoader.LoadClass(type, xmlMembers));
                }
            }

            return classNodeList;
        }

        private static List<InterfaceNode> LoadInterfaceList(Type[] types, List<XmlMember> xmlMembers)
        {
            List<InterfaceNode> interfaceNodeList = new List<InterfaceNode>();

            foreach (var type in types)
            {
                if (type.IsInterface)
                {
                    interfaceNodeList.Add(InterfaceLoader.LoadInterface(type, xmlMembers));
                }
            }

            return interfaceNodeList;
        }

        private static List<EnumNode> LoadEnumList(Type[] types, List<XmlMember> xmlMembers)
        {
            List<EnumNode> enumNodeList = new List<EnumNode>();

            foreach (var type in types)
            {
                if (type.IsEnum)
                {
                    enumNodeList.Add(EnumLoader.LoadEnum(type, xmlMembers));
                }
            }

            return enumNodeList;
        }
    }
}

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

            return mainModel;
        }

        private static List<ClassNode> LoadClassList(Type[] types, List<XmlMember> xmlMembers)
        {
            List<ClassNode> classNodeList = new List<ClassNode>();

            foreach (var type in types)
            {
                ClassNode classNode = ClassLoader.LoadClass(type, xmlMembers);

                classNodeList.Add(classNode);
            }

            return classNodeList;
        }
    }
}

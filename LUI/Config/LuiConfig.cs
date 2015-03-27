using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using lasercom.gpib;
using lasercom.control;
using lasercom.camera;
using lasercom.ddg;
using lasercom;
using lasercom.objects;
using log4net.Appender;
using log4net;
using log4net.Repository.Hierarchy;
using log4net.Layout;
using log4net.Core;
using System.Linq.Expressions;

//  <summary>
//      Class for managing LUI XML config files.
//  </summary>

namespace LUI.config
{
    [XmlRoot( "LUIConfig" )]
    public class LuiConfig : IXmlSerializable
    {
        public Dictionary<Type, IList<LuiObjectParameters>> ParameterLists;

        #region ConfigFile
        private string _ConfigFile;
        public string ConfigFile
        {
            get
            {
                return _ConfigFile;
            }
            set
            {
                _ConfigFile = value;
            }
        }

        public string GetConfigFile()
        {
            return _ConfigFile;
        }

        public void SetConfigFile(string val)
        {
            _ConfigFile = val;
        }
        #endregion

        #region LogFile
        private string _LogFile;
        public string LogFile
        {
            get
            {
                return _LogFile;
            }
            set
            {
                _LogFile = value;
            }
        }

        public string GetLogFile()
        {
            return _LogFile;
        }

        public void SetLogFile(string val)
        {
            _LogFile = val;
        }
        #endregion

        #region LogLevel
        private string _LogLevel;
        public string LogLevel
        {
            get
            {
                return _LogLevel;
            }
            set
            {
                _LogLevel = value;
            }
        }

        public string GetLogLevel()
        {
            return _LogLevel;
        }

        public void SetLogLevel(string val)
        {
            _LogLevel = val;
        }
        #endregion

        public bool Ready
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public LuiConfig()
            : this(LUI.Constants.DefaultConfigFileLocation)
        {

        }

        public LuiConfig(string configFile)
        {
            // Set the private fields directly to avoid setter side effects.
            _ConfigFile = configFile;
            _LogFile = LUI.Constants.DefaultLogFileLocation;
            _LogLevel = LUI.Constants.DefaultLogLevel;

            ParameterLists = new Dictionary<Type, IList<LuiObjectParameters>>();

        }

        public Commander CreateCommander()
        {
            throw new NotImplementedException();
        }

        public void ConfigureLogging()
        {
            var tracer = new TraceAppender();
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
            hierarchy.Root.AddAppender(tracer);
            
            PatternLayout patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = "%date [%thread] %-5level %logger - %message%newline";
            patternLayout.ActivateOptions();
            tracer.Layout = patternLayout;

            FileAppender fileAppender = new FileAppender();
            fileAppender.AppendToFile = true;
            fileAppender.File = LogFile;
            fileAppender.Layout = patternLayout;
            fileAppender.LockingModel = new FileAppender.ExclusiveLock();
            fileAppender.ActivateOptions();
            hierarchy.Root.AddAppender(fileAppender);

            //TextBoxAppender textBoxAppender = new TextBoxAppender();
            //textBoxAppender.FormName = "";
            //textBoxAppender.TextBoxName = "";
            //hierarchy.Root.AddAppender(textBoxAppender);

            hierarchy.Root.Level = hierarchy.LevelMap[LogLevel];
            hierarchy.Configured = true;
        }

        public void AddParameters(LuiObjectParameters p)
        {
            IList<LuiObjectParameters> plist;
            bool found = ParameterLists.TryGetValue(p.GetType(), out plist);
            if (!found) ParameterLists.Add(p.GetType(), new List<LuiObjectParameters>());
            ParameterLists[p.GetType()].Add(p);
        }

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            // Root element NOT handled automatically.
            reader.MoveToContent();
            bool empty = reader.IsEmptyElement;
            //reader.ReadStartElement(); // Read root element.
            if (!empty)
            {
                // Application parameters.
                reader.ReadToFollowing("ApplicationParameters");
                using (var subtree = reader.ReadSubtree())
                {
                    subtree.MoveToContent();
                    while (subtree.Read())
                    {
                        if (subtree.IsStartElement())
                            typeof(LuiConfig).GetProperty(subtree.Name).SetValue(this, subtree.ReadElementContentAsString());
                    }
                }
                // LuiObjectParamters lists.
                reader.ReadToFollowing("LuiObjectParametersList");
                using (var subtree = reader.ReadSubtree())
                {
                    subtree.MoveToContent();
                    while (subtree.Read())
                    {
                        subtree.MoveToContent();
                        if (!subtree.Name.EndsWith("List") && subtree.IsStartElement())
                        {
                            // Previously serialized runtime type.
                            Type type = Type.GetType(reader.GetAttribute("ParametersTypeName"));
                            var serializer = new XmlSerializer(type);
                            LuiObjectParameters p = (LuiObjectParameters)serializer.Deserialize(subtree.ReadSubtree());
                            AddParameters(p);
                        }
                        
                    }
                }
                
                //reader.ReadEndElement(); // End root.
            }
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            // Root element <LuiConfig> start/end handled automatically.
            // Write the individual options.
            writer.WriteStartElement("ApplicationParameters");
            writer.WriteElementString(GetPropertyName(() => ConfigFile), ConfigFile.ToString());
            writer.WriteElementString(GetPropertyName(() => LogFile), LogFile.ToString());
            writer.WriteElementString(GetPropertyName(() => LogLevel), LogLevel.ToString());
            writer.WriteEndElement();

            // Write the LuiObjectParameters.
            writer.WriteStartElement("LuiObjectParametersList");
            foreach (KeyValuePair<Type, IList<LuiObjectParameters>> kvp in ParameterLists)
            {
                // Write list of specific LuiObjectParameters subtype.
                writer.WriteStartElement(kvp.Key.Name + "List");
                foreach (LuiObjectParameters p in kvp.Value)
                {
                    var serializer = new XmlSerializer(p.GetType()); // Uses exact runtime type!
                    serializer.Serialize(writer, p);
                }
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        private static string GetPropertyName<T>(Expression<Func<T>> property)
        {
            var me = property.Body as MemberExpression;
            if (me == null)
            {
                throw new ArgumentException();
            }
            return me.Member.Name;
        }
    }

}

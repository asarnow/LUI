﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using lasercom.gpib;
using lasercom.control;
using lasercom.camera;
using lasercom.ddg;
using lasercom.extensions;
using lasercom;
using lasercom.objects;
using log4net.Appender;
using log4net;
using log4net.Repository.Hierarchy;
using log4net.Layout;
using log4net.Core;
using System.Linq.Expressions;
using System.Linq;

//  <summary>
//      Class for managing LUI XML config files.
//  </summary>

namespace LUI.config
{
    [XmlRoot( "LuiConfig" )]
    public class LuiConfig : IXmlSerializable, IDisposable
    {
        //public Dictionary<Type, IEnumerable<LuiObjectParameters>> ParameterLists { get; set; }
        public Dictionary<Type, Dictionary<LuiObjectParameters, ILuiObject>> LuiObjectTableIndex { get; set; }
        //public Dictionary<LuiObjectParameters, ILuiObject> LuiObjectTable { get; set; }

        #region Application parameters
        /* Application parameters have:
         *   Private fields
         *   Public getters and setters
         *   Public properties
         *   
         * The properties have no side effects and are used for serialization.
         * The Get/Set methods either trigger events indicating that the
         * configuration has changed or directly change the application state.
         */
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
            ((Hierarchy)LogManager.GetRepository()).Root.Level = ((Hierarchy)LogManager.GetRepository()).LevelMap[val];
            ((Hierarchy)LogManager.GetRepository()).RaiseConfigurationChanged(EventArgs.Empty);
            _LogLevel = val;
        }
        #endregion
        #endregion

        public bool Ready
        {
            get
            {
                return false;
            }
        }

        public LuiConfig()
            : this(LUI.Constants.DefaultConfigFileLocation)
        {

        }

        public LuiConfig(string configFile)
        {
            ConfigFile = configFile;
            LogFile = LUI.Constants.DefaultLogFileLocation;
            LogLevel = LUI.Constants.DefaultLogLevel;

            //LuiObjectTable = new Dictionary<LuiObjectParameters, ILuiObject>();
            LuiObjectTableIndex = new Dictionary<Type, Dictionary<LuiObjectParameters, ILuiObject>>();

            //ParameterLists = new Dictionary<Type, IEnumerable<LuiObjectParameters>>();
            // Prepopulate parameter lists using all concrete LuiObjectParameters subclasses.
            foreach (Type type in typeof(LuiObjectParameters).GetSubclasses(true))
            {
                //ParameterLists.Add(type, new List<LuiObjectParameters>());
                LuiObjectTableIndex.Add(type, new Dictionary<LuiObjectParameters, ILuiObject>());
            }
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
            //IEnumerable<LuiObjectParameters> plist;
            //bool found = ParameterLists.TryGetValue(p.GetType(), out plist);
            //if (!found) ParameterLists.Add(p.GetType(), new List<LuiObjectParameters>());
            //((IList<LuiObjectParameters>)ParameterLists[p.GetType()]).Add(p);
            //LuiObjectTable.Add(p, null);

            Dictionary<LuiObjectParameters, ILuiObject> subtable;
            bool found = LuiObjectTableIndex.TryGetValue(p.GetType(), out subtable);
            if (!found) LuiObjectTableIndex.Add(p.GetType(), new Dictionary<LuiObjectParameters, ILuiObject>());
            LuiObjectTableIndex[p.GetType()].Add(p, null);
        }

        public void ReplaceParameters<P>(IEnumerable<P> NewParameters) where P:LuiObjectParameters<P>
        {
            //IEnumerable<LuiObjectParameters> OldParameters = ParameterLists[typeof(P)];
            IEnumerable<LuiObjectParameters> OldParameters = LuiObjectTableIndex[typeof(P)].Keys.AsEnumerable();

            // New parameters where all old parameters have different name.
            // Same as "New parameters where not any old parameters have same name."
            // I.e. Where(p => !OldParameters.Any(q => q.Name == p.Name));
            var DefinitelyNew = NewParameters.Where(p => OldParameters.All(q => q.Name != p.Name));
            // Old parameters where all new parameters have different name.
            // Adding ToList() makes a copy which can be iterated while modifying the source enumerable.
            var DefinitelyOld = OldParameters.Where(p => NewParameters.All(q => q.Name != p.Name)).ToList();

            // Dispose all definitely old entries.
            foreach (P p in DefinitelyOld)
            {
                //var luiObject = LuiObjectTable[p];
                var luiObject = LuiObjectTableIndex[p.GetType()][p];
                if (luiObject != null) luiObject.Dispose();
                //LuiObjectTable.Remove(p);
                LuiObjectTableIndex[p.GetType()].Remove(p); // Only legal because DefinitelyOld copied with ToList().
            }
            // Create all definitely new entries.
            foreach (P p in DefinitelyNew)
            {
                //LuiObjectTable.Add(p, null);
                LuiObjectTableIndex[p.GetType()].Add(p, null);
            }

            // Find old parameters with same name as new parameters using LINQ.
            var sameNames = from p in OldParameters
                            join q in NewParameters on p.Name equals q.Name
                            select new { Old = p, New = q };

            foreach (var pair in sameNames)
            {
                if (!pair.Old.Equals(pair.New)) // Existing entry needs update.
                {
                    //var luiObject = LuiObjectTable[pair.Old];
                    var luiObject = LuiObjectTableIndex[pair.Old.GetType()][pair.Old];
                    if (luiObject != null) luiObject.Dispose();
                    //LuiObjectTable.Remove(pair.Old);
                    //LuiObjectTable.Add(pair.New, null);
                    LuiObjectTableIndex[pair.Old.GetType()].Remove(pair.Old);
                    LuiObjectTableIndex[pair.New.GetType()].Add(pair.New, null);
                }
            }

            // At this point, all entries in the object table are null EXCEPT ones with no changed parameters.

            // Replace parameter list with list of new and updated parameters.
            //var Parameters = DefinitelyNew.Union(sameNames.Select(p => p.New)).ToList(); 
            //ParameterLists[typeof(P)] = Parameters;
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
            foreach (KeyValuePair<Type, Dictionary<LuiObjectParameters, ILuiObject>> kvp in LuiObjectTableIndex)
            //foreach (KeyValuePair<Type, IEnumerable<LuiObjectParameters>> kvp in ParameterLists)
            {
                // Write list of specific LuiObjectParameters subtype.
                writer.WriteStartElement(kvp.Key.Name + "List");
                foreach (LuiObjectParameters p in kvp.Value.Keys)
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

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var subtable in LuiObjectTableIndex.Values) // List of subtables
                {
                    foreach (var kvp in subtable) // parameter/object pair
                    {
                        if (kvp.Value != null) kvp.Value.Dispose();
                    }
                }
            }
        }

        public void Save()
        {
            Save(ConfigFile);
        }

        public void Save(string FileName)
        {
            var serializer = new XmlSerializer(typeof(LuiConfig));
            using (var writer = new StreamWriter(FileName))
            {
                serializer.Serialize(writer, this);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public static LuiConfig FromFile(string FileName)
        {
            var serializer = new XmlSerializer(typeof(LuiConfig));
            LuiConfig Config = null;
            using (var reader = new StreamReader(FileName))
            {
                Config = (LuiConfig)serializer.Deserialize(reader);
            }
            return Config;
        }

        public void InstantiateConfiguration()
        {
            Stack<Type> TypeStack = new Stack<Type>(LuiObjectTableIndex.Keys.AsEnumerable());

            Type type;
            while((type = TypeStack.Pop()) != null){
                
            }
        }

        public void InstantiateDependencies(Type t)
        {
            //IEnumerable<LuiObjectParameters> parameters = ParameterLists[t];
            // Type dependency = p.Type; // Should be loop over p.Dependencies
            
        }
    }

}

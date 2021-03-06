﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Extensions;
using lasercom;
using lasercom.camera;
using lasercom.control;
using lasercom.ddg;
using lasercom.objects;
using log4net;
using log4net.Appender;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using LUI.tabs;

//  <summary>
//      Class for managing LUI XML config files.
//  </summary>

namespace LUI.config
{
    [XmlRoot( "LuiConfig" )]
    public class LuiConfig : IXmlSerializable, IDisposable
    {
        protected static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Dictionary<Type, Dictionary<LuiObjectParameters, ILuiObject>> LuiObjectTableIndex { get; set; }

        public event EventHandler ParametersChanged;

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
        #region Dry run
        private bool _DryRun;
        public bool DryRun
        {
            get
            {
                return _DryRun;
            }
            set
            {
                _DryRun = value;
            }
        }
        #endregion

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

        public Dictionary<string, Dictionary<string, string>> TabSettings { get; set; }

        private bool _Saved;
        public bool Saved
        {
            get
            {
                return _Saved;
            }
            private set
            {
                _Saved = value;
            }
        }

        public bool Ready
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Enumerates all the parameters in the object table.
        /// </summary>
        public IEnumerable<LuiObjectParameters> LuiObjectParameters
        {
            get
            {
                foreach (var subtable in LuiObjectTableIndex.Values) // List of subtables
                    {
                        foreach (var kvp in subtable) // parameter/object pair
                        {
                            yield return kvp.Key;
                        }
                    }
            }
        }

        public LuiConfig()
            : this(LUI.Constants.DefaultConfigFileLocation)
        {

        }

        public LuiConfig(string configFile)
        {
            DryRun = false;
            ConfigFile = configFile;
            LogFile = LUI.Constants.DefaultLogFileLocation;
            LogLevel = LUI.Constants.DefaultLogLevel;

            Saved = false;

            TabSettings = new Dictionary<string, Dictionary<string, string>>();

            // Prepopulate tab settings using all LuiTab subclasses.
            foreach (Type type in typeof(LuiTab).GetSubclasses(true))
            {
                TabSettings.Add(type.Name, new Dictionary<string, string>());
            }
            
            LuiObjectTableIndex = new Dictionary<Type, Dictionary<LuiObjectParameters, ILuiObject>>();

            // Prepopulate parameter lists using all concrete LuiObjectParameters subclasses.
            foreach (Type type in typeof(LuiObjectParameters).GetSubclasses(true))
            {
                LuiObjectTableIndex.Add(type, new Dictionary<LuiObjectParameters, ILuiObject>());
            }
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

        //public T GetObject<P, T>(P p)
        //    where P : LuiObjectParameters<P>
        //    where T : ILuiObject<P>; // This signature could be sweet if ILuiObject<P> where P:LuiObjectParameters<P>

        public ILuiObject GetObject(LuiObjectParameters p)
        {
            ILuiObject val;
            //LuiObjectTableIndex[p.GetType()].TryGetValue(p, out val);
            val = LuiObjectTableIndex[p.GetType()][p];
            return val;
        }

        public void SetObject(LuiObjectParameters p, ILuiObject o)
        {
            LuiObjectTableIndex[p.GetType()][p] = o;
        }

        /// <summary>
        /// Get all the parameters of a particular type, as that type.
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <returns></returns>
        public IEnumerable<P> GetParameters<P>() where P:LuiObjectParameters<P>
        {
            return LuiObjectTableIndex[typeof(P)].Keys.AsEnumerable().Cast<P>();
        }

        /// <summary>
        /// Get the parameters of a particular type as nongeneric parameters.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public IEnumerable<LuiObjectParameters> GetParameters(Type t)
        {
            return LuiObjectTableIndex[t].Keys.AsEnumerable();
        }

        public LuiObjectParameters GetFirstParameters(string Name)
        {
            foreach (var p in LuiObjectParameters)
            {
                if (p.Name == Name) return p;
            }
            return null;
        }

        public LuiObjectParameters GetFirstParameters(Type t, string Name)
        {
            foreach (var p in GetParameters(t))
            {
                if (p.Name == Name) return p;
            }
            return null;
        }

        public void AddParameters(LuiObjectParameters p)
        {
            Dictionary<LuiObjectParameters, ILuiObject> subtable;
            bool found = LuiObjectTableIndex.TryGetValue(p.GetType(), out subtable);
            if (!found) LuiObjectTableIndex.Add(p.GetType(), new Dictionary<LuiObjectParameters, ILuiObject>());
            LuiObjectTableIndex[p.GetType()].Add(p, null);
        }

        public void ReplaceParameters<P>(IEnumerable<P> NewParameters) where P:LuiObjectParameters<P>
        {
            IEnumerable<P> OldParameters = LuiObjectTableIndex[typeof(P)].Keys.AsEnumerable().Cast<P>();

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
                var luiObject = LuiObjectTableIndex[p.GetType()][p];
                if (luiObject != null) luiObject.Dispose();
                LuiObjectTableIndex[p.GetType()].Remove(p); // Only legal because DefinitelyOld copied with ToList().
            }
            // Create all definitely new entries.
            foreach (P p in DefinitelyNew)
            {
                LuiObjectTableIndex[p.GetType()].Add(p, null);
            }

            // Find old parameters with same name as new parameters using LINQ.
            var sameNames = (from p in OldParameters
                            join q in NewParameters on p.Name equals q.Name
                            select new { Old = p, New = q }).ToList(); // Same ToList() copy trick.

            foreach (var pair in sameNames)
            {
                //if (!pair.Old.Equals(pair.New)) // Existing entry needs update.
                if (pair.Old.NeedsReinstantiation(pair.New))
                {
                    ILuiObject luiObject = LuiObjectTableIndex[pair.Old.GetType()][pair.Old];
                    // if NeedsReinstantion, Dispose and set null.
                    // else if NeedsUpdate, update but don't null.
                    if (luiObject != null) luiObject.Dispose();
                    LuiObjectTableIndex[pair.Old.GetType()].Remove(pair.Old);
                    LuiObjectTableIndex[pair.New.GetType()].Add(pair.New, null);
                }
                else if (pair.Old.NeedsUpdate(pair.New))
                {
                    LuiObject<P> luiObject = (LuiObject<P>) LuiObjectTableIndex[pair.Old.GetType()][pair.Old];
                    if (luiObject != null) luiObject.Update(pair.New);
                    LuiObjectTableIndex[pair.Old.GetType()].Remove(pair.Old);
                    LuiObjectTableIndex[pair.New.GetType()].Add(pair.New, luiObject);
                }
            }

            // At this point, all entries in the object table are null 
            // EXCEPT those with no changed parameters OR which have been updated without reinstantiation.
        }

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null; // This method is deprecated, framework documentation says to return null.
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
                var settings = new DataContractSerializerSettings();
                settings.PreserveObjectReferences = true;
                settings.KnownTypes = typeof(LuiObjectParameters).GetSubclasses(true);
                var serializer = new DataContractSerializer(typeof(LuiObjectParameters), settings);

                reader.ReadToFollowing("LuiObjectParametersList");
                using (var subtree = reader.ReadSubtree())
                {
                    subtree.MoveToContent();
                    while (subtree.Read())
                    {
                        subtree.MoveToContent();
                        if (!subtree.Name.EndsWith("List") && subtree.IsStartElement())
                        {
                            LuiObjectParameters p = (LuiObjectParameters)serializer.ReadObject(subtree.ReadSubtree());
                            AddParameters(p);
                        }
                        
                    }
                }

                // Tab settings.
                
                    reader.ReadToFollowing("TabSettings");
                    using (var subtree = reader.ReadSubtree())
                    {
                        subtree.MoveToContent();
                        ISet<string> TabNames = new HashSet<string>(typeof(LuiTab).GetSubclasses(true).Select(x => x.Name));
                        string Tab = null;
                        while (subtree.Read())
                        {
                            subtree.MoveToContent();
                            if (subtree.IsStartElement())
                            {
                                if (TabNames.Contains(subtree.Name))
                                {
                                    Tab = subtree.Name;
                                }
                                else
                                {
                                    string Name = subtree.Name;
                                    subtree.Read();
                                    TabSettings[Tab].Add(Name, subtree.Value);
                                }
                            
                            }
                        }
                    }

                //reader.ReadEndElement(); // End root.
            }

            Saved = true;
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            // Root element <LuiConfig> start/end handled automatically.
            // Write the individual options.
            writer.WriteStartElement("ApplicationParameters");
            writer.WriteElementString(Util.GetPropertyName(() => ConfigFile), ConfigFile.ToString());
            writer.WriteElementString(Util.GetPropertyName(() => LogFile), LogFile.ToString());
            writer.WriteElementString(Util.GetPropertyName(() => LogLevel), LogLevel.ToString());
            writer.WriteEndElement();

            // Write the LuiObjectParameters.
            var settings = new DataContractSerializerSettings();
            settings.PreserveObjectReferences = true;
            settings.KnownTypes = typeof(LuiObjectParameters).GetSubclasses(true);
            var serializer = new DataContractSerializer(typeof(LuiObjectParameters), settings);

            writer.WriteStartElement("LuiObjectParametersList");
            foreach (KeyValuePair<Type, Dictionary<LuiObjectParameters, ILuiObject>> kvp in LuiObjectTableIndex)
            {
                // Write list of specific LuiObjectParameters subtype.
                writer.WriteStartElement(kvp.Key.Name + "List");
                foreach (LuiObjectParameters p in kvp.Value.Keys)
                {
                    serializer.WriteObject(writer, p);
                }
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            // Write TabSettings.
            writer.WriteStartElement("TabSettings");
            foreach (var TabKvp in TabSettings)
            {
                // Write settings for specific LuiTab subtype.
                writer.WriteStartElement(TabKvp.Key);
                foreach (var SettingKvp in TabKvp.Value)
                {
                    writer.WriteElementString(SettingKvp.Key, SettingKvp.Value);
                }
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            Saved = true;
        }

        /// <summary>
        /// Disposes all LuiObjects in the LuiObject table.
        /// </summary>
        /// <param name="disposing"></param>
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

        /// <summary>
        /// Serializes the LuiConfig instance to ConfigFile.
        /// </summary>
        public void Save()
        {
            Save(ConfigFile);
        }

        /// <summary>
        /// Serializes the LuiConfig instance to a file.
        /// </summary>
        /// <param name="FileName"></param>
        public void Save(string FileName)
        {
            var serializer = new XmlSerializer(typeof(LuiConfig));
            try
            {
                using (var writer = new StreamWriter(FileName))
                {
                    serializer.Serialize(writer, this);
                }
                Saved = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Deserializes a LuiConfig instance from XML file.
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
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
            if (DryRun) return;
            // The topological sort ensures dependencies are resolved in a legal order.
            // Cyclic dependencies will result in exceptions.
            IEnumerable<LuiObjectParameters> dependencyOrderedParameters = LuiObjectParameters.TopologicalSort(p => p.Dependencies);
            foreach (var p in dependencyOrderedParameters)
            {
                if (GetObject(p) == null)
                {
                    IEnumerable<ILuiObject> dependencies = p.Dependencies.Select(d => GetObject(d));
                    SetObject(p, LuiObject.Create(p, dependencies));
                }
            }
        }

        public async Task InstantiateConfigurationAsync()
        {
            await Task.Run(() =>
            {
                InstantiateConfiguration();
            });
        }

        public bool TryInstantiateConfiguration()
        {
            try
            {
                InstantiateConfiguration();
            }
            catch (Exception e)
            {
                Log.Error(e);
                return false;
            }
            return true;
        }

        public void InstantiateWithDependencies(LuiObjectParameters p)
        {
            if (GetObject(p) == null)
            {
                var ordered = p.Dependencies.TopologicalSort(q => q.Dependencies);
                foreach (var q in ordered)
                {
                    InstantiateWithDependencies(q);
                }
                SetObject(p, LuiObject.Create(p, p.Dependencies.Select(d => GetObject(d))));
            }
        }

        public void OnParametersChanged(object sender, EventArgs e)
        {
            ParametersChanged.Raise(sender, e);
            if (!(sender is ParentForm)) Saved = false;
        }

        public static LuiConfig DummyConfig()
        {
            var config = new LuiConfig();
            var bf = new BeamFlagsParameters(typeof(DummyBeamFlags));
            bf.Name = "Dummy";
            var cam = new CameraParameters(typeof(DummyAndorCamera));
            cam.Name = "Dummy";
            var dg = new DelayGeneratorParameters(typeof(DummyDigitalDelayGenerator));
            dg.Name = "Dummy";
            config.AddParameters(bf);
            config.AddParameters(cam);
            config.AddParameters(dg);
            return config;
        }
    }

}

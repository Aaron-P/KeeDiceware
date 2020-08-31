using KeeDiceware.Generators;
using KeeDiceware.Wordlists;
using KeePassLib.Cryptography;
using KeePassLib.Security;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace KeeDiceware
{
    //TODO: The handling of "Generators" sucks, change it to something better.
    //      I think it will also break if we add more.
    public sealed class Settings
    {
        public static Settings Deserialize(string data)
        {
            try
            {
                var deserializer = new XmlSerializer(typeof(Settings), GeneratorBase.Types);
                using (var stringReader = new StringReader(data))
                using (var xmlReader = XmlReader.Create(stringReader))
                {
                    var settings = (Settings)deserializer.Deserialize(xmlReader);
                    //Remove non-existant generators?
                    foreach (var generator in GeneratorBase.Generators)
                    {
                        if (!settings.Generators.Any(_ => _.Key == generator.Key))
                            settings.Generators.Add(generator);
                    }

                    //Error out and create new setting instead of this?
                    if (!GeneratorBase.Generators.Any(_ => _.Key == settings.Generator))
                        settings.Generator = typeof(WordCountGenerator).Name;
                    if (!WordlistBase.Wordlists.Any(_ => _.Key == settings.Wordlist))
                        settings.Wordlist = typeof(EFFLargeWordlist).Name;

                    return settings;
                }
            }
            catch (InvalidOperationException)
            {
                return null;//default(Settings);
            }
        }

        public static Settings Default
        {
            get
            {
                var settings = new Settings();
                settings.Generators.AddRange(GeneratorBase.Generators);
                return settings;
            }
        }

        public Settings()
        {
            Wordlist = typeof(EFFLargeWordlist).Name;
            Generator = typeof(WordCountGenerator).Name;
            Generators = new List<GeneratorBase>();
        }

        public string Wordlist { get; set; }

        public string Generator { get; set; }

        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Needs to be serializable.")]
        public List<GeneratorBase> Generators { get; private set; }

        public ProtectedString Generate(CryptoRandomStream random)
        {
            var generator = Generators.SingleOrDefault(_ => _.Key == Generator);
            return generator == null ? null : generator.Generate(random, this);
        }

        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public string Serialize()
        {
            var settings = new XmlWriterSettings
            {
                Indent = false,
                OmitXmlDeclaration = false,
                NewLineHandling = NewLineHandling.None
            };

            using (var writer = new StringWriter(CultureInfo.InvariantCulture))
            using (var xmlWriter = XmlWriter.Create(writer, settings))
            {
                var serializer = new XmlSerializer(typeof(Settings), GeneratorBase.Types);
                serializer.Serialize(xmlWriter, this);
                return writer.ToString();
            }
        }
    }
}

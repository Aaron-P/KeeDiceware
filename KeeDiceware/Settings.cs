using KeeDiceware.Generators;
using KeeDiceware.Wordlists;
using KeePassLib.Cryptography;
using KeePassLib.Security;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace KeeDiceware
{
    // TODO: The handling of "Generators" sucks, change it to something better.
    // I think it will also break if we add more.

    /// <summary>
    /// A class representing the user's settings for passphrase generation.
    /// </summary>
    public sealed class Settings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Settings"/> class.
        /// </summary>
        public Settings()
        {
            Wordlist = typeof(EFFLargeWordlist).Name;
            Generator = typeof(WordCountGenerator).Name;
            Generators = new List<GeneratorBase>();
        }

        /// <summary>
        /// Gets a default set of settings.
        /// </summary>
        public static Settings Default
        {
            get
            {
                var settings = new Settings();
                settings.Generators.AddRange(GeneratorBase.Generators);
                return settings;
            }
        }

        /// <summary>
        /// Gets or sets the key for the wordlist to use.
        /// </summary>
        public string Wordlist { get; set; }

        /// <summary>
        /// Gets or sets the key for the passphrase generator to use.
        /// </summary>
        public string Generator { get; set; }

        /// <summary>
        /// Gets a list of available passphrase generators.
        /// </summary>
        public List<GeneratorBase> Generators { get; private set; }

        /// <summary>
        /// Creates a <see cref="Settings"/> instance from a serialized string.
        /// </summary>
        /// <param name="data">A serialized settings string.</param>
        /// <returns>A <see cref="Settings"/> instance representing the serialized set.</returns>
        public static Settings Deserialize(string data)
        {
            try
            {
                var deserializer = new XmlSerializer(typeof(Settings), GeneratorBase.Types);
                using (var stringReader = new StringReader(data))
                using (var xmlReader = XmlReader.Create(stringReader))
                {
                    var settings = (Settings)deserializer.Deserialize(xmlReader);
                    // Remove non-existant generators?
                    foreach (var generator in GeneratorBase.Generators)
                    {
                        if (!settings.Generators.Any(_ => _.Key == generator.Key))
                            settings.Generators.Add(generator);
                    }

                    // Error out and create new setting instead of this?
                    if (!GeneratorBase.Generators.Any(_ => _.Key == settings.Generator))
                        settings.Generator = typeof(WordCountGenerator).Name;
                    if (!WordlistBase.Wordlists.Any(_ => _.Key == settings.Wordlist))
                        settings.Wordlist = typeof(EFFLargeWordlist).Name;

                    return settings;
                }
            }
            catch (InvalidOperationException)
            {
                return null; //default(Settings);
            }
        }

        /// <summary>
        /// Generates a passphrase from the current set of settings.
        /// </summary>
        /// <param name="random">A <see cref="CryptoRandomStream"/> instance for generating random numbers.</param>
        /// <returns>A <see cref="ProtectedString"/> containing a generated passphrase.</returns>
        public ProtectedString Generate(CryptoRandomStream random)
        {
            var generator = Generators.SingleOrDefault(_ => _.Key == Generator);
            return generator == null ? null : generator.Generate(random, this);
        }

        /// <summary>
        /// Serializes the current settings to a string.
        /// </summary>
        /// <returns>A string representing serialized settings.</returns>
        public string Serialize()
        {
            var settings = new XmlWriterSettings
            {
                Indent = false,
                OmitXmlDeclaration = false,
                NewLineHandling = NewLineHandling.None,
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

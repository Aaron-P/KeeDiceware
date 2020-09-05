using KeeDiceware.Forms;
using KeePass.Plugins;
using KeePassLib;
using KeePassLib.Cryptography;
using KeePassLib.Cryptography.PasswordGenerator;
using KeePassLib.Security;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace KeeDiceware
{
    /// <summary>
    /// The class used by KeePass to generate a password.
    /// </summary>
    public sealed class KeeDicewareGen : CustomPwGenerator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeeDicewareGen"/> class.
        /// </summary>
        /// <param name="host">An <see cref="IPluginHost"/> instance.</param>
        public KeeDicewareGen(IPluginHost host)
        {
            Host = host;
        }

        /// <summary>
        /// Gets the name of the KeePass password generator plugin.
        /// </summary>
        public override string Name
        {
            get
            {
                return "Diceware Passphrase";
            }
        }

        /// <summary>
        /// Gets a value indicating whether the plugin uses custom options.
        /// </summary>
        public override bool SupportsOptions
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a unique identifier for the password generator.
        /// </summary>
        public override PwUuid Uuid
        {
            get
            {
                return new PwUuid(new Guid(((GuidAttribute)typeof(KeeDicewareGen).Assembly.GetCustomAttributes(typeof(GuidAttribute), true)[0]).Value).ToByteArray());
            }
        }

        private IPluginHost Host { get; set; }

        /// <summary>
        /// Generates a password with a given profile.
        /// </summary>
        /// <param name="prf">A password generator profile.</param>
        /// <param name="crsRandomSource">A <see cref="CryptoRandomStream"/> instance for generating random numbers.</param>
        /// <returns>A <see cref="ProtectedString"/> containing a generated password.</returns>
        public override ProtectedString Generate(PwProfile prf, CryptoRandomStream crsRandomSource)
        {
            if (prf == null)
                throw new ArgumentNullException("prf" /*nameof(prf)*/);
            if (crsRandomSource == null)
                throw new ArgumentNullException("crsRandomSource" /*nameof(crsRandomSource)*/);

            //if (prf == null)
            //    Debug.Assert(false);
            //else
            //    Debug.Assert(prf.CustomAlgorithmUuid == Convert.ToBase64String(Uuid.UuidBytes, Base64FormattingOptions.None));

            var settings = Settings.Deserialize(prf.CustomAlgorithmOptions) ?? Settings.Default;
            return settings.Generate(crsRandomSource);
        }

        /// <summary>
        /// Opens a form to get password generator settings from the user.
        /// </summary>
        /// <param name="strCurrentOptions">A serialized set of 'current' options.</param>
        /// <returns>A serialized set of options.</returns>
        public override string GetOptions(string strCurrentOptions)
        {
            var settings = Settings.Deserialize(strCurrentOptions);
            using (var form = new SettingsForm(ref settings))
            {
                form.StartPosition = FormStartPosition.CenterParent;
                if (form.ShowDialog(Host.MainWindow) == DialogResult.OK)
                    return settings.Serialize();
            }

            return strCurrentOptions;
        }

        // wordlist cache
        // parsing of diceware style lists, remove numeric strings
        // Pass config instead
        // config option include or not the separator for character/entropy
    }
}

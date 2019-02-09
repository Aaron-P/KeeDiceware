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
    public sealed class KeeDicewareGen : CustomPwGenerator
    {
        public KeeDicewareGen(IPluginHost host)
        {
            Host = host;
        }

        public override string Name => "Diceware Passphrase";

        public override bool SupportsOptions => true;

        public override PwUuid Uuid => new PwUuid(new Guid(((GuidAttribute)typeof(KeeDicewareGen).Assembly.GetCustomAttributes(typeof(GuidAttribute), true)[0]).Value).ToByteArray());

        private IPluginHost Host { get; set; }

        public override ProtectedString Generate(PwProfile prf, CryptoRandomStream crsRandomSource)
        {
            if (prf == null)
                throw new ArgumentNullException(nameof(prf));
            if (crsRandomSource == null)
                throw new ArgumentNullException(nameof(crsRandomSource));

            //if (prf == null)
            //    Debug.Assert(false);
            //else
            //    Debug.Assert(prf.CustomAlgorithmUuid == Convert.ToBase64String(Uuid.UuidBytes, Base64FormattingOptions.None));

            var settings = Settings.Deserialize(prf.CustomAlgorithmOptions) ?? Settings.Default;
            return settings.Generate(crsRandomSource);
        }

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

        //wordlist cache
        //parsing of diceware style lists, remove numeric strings
        //Pass config instead
        //config option include or not the separator for character/entropy
    }
}

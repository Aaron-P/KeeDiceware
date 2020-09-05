using KeePass.Plugins;
using System;

namespace KeeDiceware
{
    /// <summary>
    /// The main KeePass plugin class.
    /// </summary>
    public sealed class KeeDicewareExt : Plugin
    {
        private static readonly Uri UpdateUri = new Uri("https://raw.githubusercontent.com/Aaron-P/KeeDiceware/master/version.txt", UriKind.Absolute);

        private IPluginHost _host;

        private KeeDicewareGen _gen;

        /// <summary>
        /// Gets the url to check for the latest plugin version information.
        /// </summary>
        public override string UpdateUrl
        {
            get
            {
                return UpdateUri.AbsoluteUri;
            }
        }

        /// <summary>
        /// Initializes the KeePass plugin.
        /// </summary>
        /// <param name="host">An <see cref="IPluginHost"/> instance.</param>
        /// <returns>True if initialization was successful, otherwise false.</returns>
        public override bool Initialize(IPluginHost host)
        {
            if (host == null)
                return false;
            _host = host;

            _gen = new KeeDicewareGen(_host);
            _host.PwGeneratorPool.Add(_gen);

            return true;
        }

        /// <summary>
        /// Terminates the KeePass plugin.
        /// </summary>
        public override void Terminate()
        {
            if (_host != null && _gen != null)
                _host.PwGeneratorPool.Remove(_gen.Uuid);
            if (_host != null)
                _host = null;
            if (_gen != null)
                _gen = null;
        }
    }
}

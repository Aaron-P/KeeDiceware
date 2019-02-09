using System;
using KeePass.Plugins;

namespace KeeDiceware
{
    public sealed class KeeDicewareExt : Plugin
    {
        private IPluginHost m_host = null;
        private KeeDicewareGen m_gen = null;

        public override bool Initialize(IPluginHost host)
        {
            if (host == null)
                return false;
            m_host = host;

            m_gen = new KeeDicewareGen(m_host);
            m_host.PwGeneratorPool.Add(m_gen);

            return true;
        }

        public override void Terminate()
        {
            if (m_host != null && m_gen != null)
                m_host.PwGeneratorPool.Remove(m_gen.Uuid);
            if (m_host != null)
                m_host = null;
            if (m_gen != null)
                m_gen = null;
        }
    }
}

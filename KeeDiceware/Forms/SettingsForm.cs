using KeeDiceware.Generators;
using KeeDiceware.Wordlists;
using System;
using System.Linq;
using System.Windows.Forms;

namespace KeeDiceware.Forms
{
    public partial class SettingsForm : Form
    {
        private Settings Settings;

        public SettingsForm(ref Settings settings)
        {
            Settings = settings = settings ?? Settings.Default;

            InitializeComponent();
            //System.Diagnostics.Debugger.Launch();

            ComboBoxWordlist.DisplayMember = nameof(WordlistBase.DisplayName);
            ComboBoxWordlist.ValueMember = nameof(WordlistBase.Key);
            ComboBoxWordlist.DataSource = WordlistBase.Wordlists.OrderBy(_ => _.DisplayName).ToList();
            ComboBoxWordlist.DataBindings.Add(nameof(ComboBoxWordlist.SelectedValue), Settings, nameof(Settings.Wordlist));

            ComboBoxGenerator.DisplayMember = nameof(GeneratorBase.DisplayName);
            ComboBoxGenerator.ValueMember = nameof(GeneratorBase.Key);
            ComboBoxGenerator.DataSource = Settings.Generators.OrderBy(_ => _.DisplayName).ToList();//GeneratorBase.Generators;
            ComboBoxGenerator.DataBindings.Add(nameof(ComboBoxGenerator.SelectedValue), Settings, nameof(Settings.Generator));

            //NumericUpDownCount.DataBindings.Add(nameof(NumericUpDownCount.Value))

            //hmmm
            NumericUpDownCount.Value = Settings.Generators.Single(_ => _.Key == (string)ComboBoxGenerator.SelectedValue).Count;
        }

        private void ComboBoxGenerator_SelectedIndexChanged(object sender, EventArgs e)
        {
            NumericUpDownCount.Value = Settings.Generators.Single(_ => _.Key == (string)ComboBoxGenerator.SelectedValue).Count;
        }

        private void NumericUpDownCount_ValueChanged(object sender, EventArgs e)
        {
            Settings.Generators.Single(_ => _.Key == (string)ComboBoxGenerator.SelectedValue).Count = (uint)NumericUpDownCount.Value;
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}

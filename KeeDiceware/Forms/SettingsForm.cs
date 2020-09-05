using KeeDiceware.Wordlists;
using System;
using System.Linq;
using System.Windows.Forms;

namespace KeeDiceware.Forms
{
    public partial class SettingsForm : Form
    {
        private readonly Settings _settings;

        public SettingsForm(ref Settings settings)
        {
            _settings = settings = settings ?? Settings.Default;

            InitializeComponent();
            //System.Diagnostics.Debugger.Launch();

            ComboBoxWordlist.DisplayMember = "DisplayName"; //nameof(WordlistBase.DisplayName);
            ComboBoxWordlist.ValueMember = "Key"; //nameof(WordlistBase.Key);
            ComboBoxWordlist.DataSource = WordlistBase.Wordlists.OrderBy(_ => _.DisplayName).ToList();
            ComboBoxWordlist.DataBindings.Add("SelectedValue" /*nameof(ComboBoxWordlist.SelectedValue)*/, _settings, "Wordlist" /*nameof(Settings.Wordlist)*/);

            ComboBoxGenerator.DisplayMember = "DisplayName"; //nameof(GeneratorBase.DisplayName);
            ComboBoxGenerator.ValueMember = "Key"; //nameof(GeneratorBase.Key);
            ComboBoxGenerator.DataSource = settings.Generators.OrderBy(_ => _.DisplayName).ToList(); //GeneratorBase.Generators;
            ComboBoxGenerator.DataBindings.Add("SelectedValue" /*nameof(ComboBoxGenerator.SelectedValue)*/, _settings, "Generator" /*nameof(Settings.Generator)*/);

            //NumericUpDownCount.DataBindings.Add("Value" /*nameof(NumericUpDownCount.Value)*/)

            // hmmm
            NumericUpDownCount.Value = _settings.Generators.Single(_ => _.Key == (string)ComboBoxGenerator.SelectedValue).Count;
        }

        private void ComboBoxGenerator_SelectedIndexChanged(object sender, EventArgs e)
        {
            NumericUpDownCount.Value = _settings.Generators.Single(_ => _.Key == (string)ComboBoxGenerator.SelectedValue).Count;
        }

        private void NumericUpDownCount_ValueChanged(object sender, EventArgs e)
        {
            _settings.Generators.Single(_ => _.Key == (string)ComboBoxGenerator.SelectedValue).Count = (uint)NumericUpDownCount.Value;
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

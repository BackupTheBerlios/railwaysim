using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Linquistics
{
    public partial class GrammarForm : Form
    {
        GrammarManager manager;

        public GrammarForm()
        {
            InitializeComponent();
        }

        #region Obs³uga zdarzeñ
        private void wizardPictureBox_Click(object sender, EventArgs e)
        {
            //stwórz wizarda do definiowania gramatyki
        }

        private void GrammarForm_Load(object sender, EventArgs e)
        {
            manager = new GrammarManager(this);
        }

        private void commitBtn_Click(object sender, EventArgs e)
        {
            manager.createNewContextFreeGrammarManager();
            this.disableGrammarDefinitionPanel();
        }

        private void drukarkToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            this.enableGrammarDefinitionPanel();
        }

        private void nonTerminalsTextBox_TextChanged(object sender, EventArgs e)
        {
            this.refreshStartSymbolList();
        }

        private void checkNowBtn_Click(object sender, EventArgs e)
        {
            manager.checkMembershipManager();
        }

        private void simulateBtn_Click(object sender, EventArgs e)
        {
            manager.simulateCheckingMembershipManager();
        }


        #endregion

        #region Pomocnicze
        void refreshStartSymbolList()
        {
            string      allNonTerminals;
            string[]    allNonTerminalsTab;

            startSymbolComboBox.Items.Clear();
            allNonTerminals = this.nonTerminalsTextBox.Text;
            allNonTerminals.Trim();

            if (allNonTerminals.CompareTo("") != 0)
            {
                allNonTerminalsTab = allNonTerminals.Split(';');
                for (int i = 0; i < allNonTerminalsTab.Length; i++)
                    startSymbolComboBox.Items.Add(allNonTerminalsTab[i]);
            }
            else
            {
                this.startSymbolComboBox.Items.Clear();
            }
        }

        void enableGrammarDefinitionPanel()
        {
            this.terminalsTextBox.Enabled = true;
            this.nonTerminalsTextBox.Enabled = true;
            this.terminalsBtn.Enabled = true;
            this.nonTerminalsBtn.Enabled = true;
            this.startSymbolComboBox.Enabled = true;
            this.productionsNumTextBox.Enabled = true;
        }

        void disableGrammarDefinitionPanel()
        {
            this.terminalsTextBox.Enabled = false;
            this.nonTerminalsTextBox.Enabled = false;
            this.terminalsBtn.Enabled = false;
            this.nonTerminalsBtn.Enabled = false;
            this.startSymbolComboBox.Enabled = false;
            this.productionsNumTextBox.Enabled = false;
        }

        #endregion

        
       
    }
}
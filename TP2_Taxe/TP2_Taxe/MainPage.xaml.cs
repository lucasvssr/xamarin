using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TP2_Taxe
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Calculer()
        {
            if (entPrix.Text == string.Empty)
            {
                lblTaxe.Text = "---";
                lblTotal.Text = "---";
            }
            else
            {
                lblTauxTaxe.Text = (slTauxTaxe.Value / 100).ToString();
                if (swTaxeIncluse.IsToggled)
                {
                    lblTaxe.Text = (int.Parse(entPrix.Text) * float.Parse(lblTauxTaxe.Text)).ToString();
                    lblTotal.Text = (float.Parse(entPrix.Text) + float.Parse(lblTaxe.Text)).ToString();
                }
                else
                {
                    lblTaxe.Text = (int.Parse(entPrix.Text) * float.Parse(lblTauxTaxe.Text)/(1+ float.Parse(lblTauxTaxe.Text))).ToString();
                    lblTotal.Text = (int.Parse(entPrix.Text)).ToString();
                }
            }
        }

        private void entPrix_TextChanged(object sender, TextChangedEventArgs e)
        {
            Calculer();
        }

        private void slTauxTaxe_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Calculer();
        }

        private void swTaxeIncluse_Toggled(object sender, ToggledEventArgs e)
        {
            Calculer();
        }

        private void but15Pourcent_Clicked(object sender, EventArgs e)
        {
            slTauxTaxe.Value = 15;
        }

        private void but20Pourcent_Clicked(object sender, EventArgs e)
        {
            slTauxTaxe.Value = 20;
        }
    }
}

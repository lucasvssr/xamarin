using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TP2_Calculatrice
{
    public partial class MainPage : ContentPage
    {
        enum Touche
        {
            AUCUNE,
            CHIFFRE,
            OPERATEUR
        }

        private Touche DernièreTouche { get; set; }

        public MainPage()
        {
            DernièreTouche= Touche.AUCUNE;
            InitializeComponent();
        }


        /// <summary>
        /// Efface le texte de lblRésultat et met à AUCUNE la propriété DernièreTouche.
        /// </summary>
        private void ButCleanClicked(object sender, EventArgs e)
        {
            DernièreTouche = Touche.AUCUNE;
            lblOpérations.Text = "0";
            lblRésultat.Text = string.Empty;
        }


        /// <summary>
        /// Récupére le texte du bouton pour déterminer le chiffre qui a été appuyé.
        /// Puis fait appel à la fonction Calculer().
        /// </summary>
        private void ButChiffreClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (lblOpérations.Text == "0") 
            {
                if(button.Text == "0")
                {
                    DernièreTouche = Touche.AUCUNE;
                }
                else
                {
                    lblOpérations.Text = button.Text;
                    DernièreTouche = Touche.CHIFFRE;
                    Calculer();
                }
            }
            else
            {
                lblOpérations.Text += button.Text;
                DernièreTouche = Touche.CHIFFRE;
                Calculer();
            }                    
        }

        private void Calculer()
        {
            string expression = lblOpérations.Text;
            expression = expression.Replace("x", "*");
            DataTable table = new DataTable();
            try
            {
                lblRésultat.Text = table.Compute(expression, "").ToString();
            }
            catch (Exception)
            {
                lblRésultat.Text = "Expression non valide";
            }
        }

        /// <summary>
        /// Change ou ajoute un opérateur 
        /// </summary>
        private void ButOperateurClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (DernièreTouche == Touche.CHIFFRE && lblOpérations.Text != "0")
            {
                lblOpérations.Text += button.Text;
                DernièreTouche = Touche.OPERATEUR;
            }
            else
            {
               if(lblOpérations.Text.Length > 1 && DernièreTouche == Touche.OPERATEUR)
                {
                    int length = lblOpérations.Text.Length;
                    lblOpérations.Text = lblOpérations.Text.Remove(length - 1) + button.Text;
                }
                else if(button.Text == "-" && lblOpérations.Text == "0")
                {
                    lblOpérations.Text = button.Text;
                }

                DernièreTouche = Touche.OPERATEUR;

                if (button.Text == "+" && lblOpérations.Text == "-")
                {
                    lblOpérations.Text = "0";
                    DernièreTouche = Touche.AUCUNE;
                }
            }
        }

        /// <summary>
        /// Calcul du résultat.
        /// </summary>
        /// <remarks>
        /// DernièreTouche est associée à CHIFFRE pour pouvoir continuer le calcul. 
        /// </remarks>
        /// <returns>Résultat arrondie à la <seealso href="https://learn.microsoft.com/fr-fr/dotnet/api/system.math.round?view=net-7.0">convention</seealso> paire la plus proche.</returns>
        private void ButEqualClicked(object sender, EventArgs e)
        {
            if(lblRésultat.Text == string.Empty)
            {
                lblOpérations.Text = "0";
            }
            else
            {
                lblOpérations.Text = Math.Round(float.Parse(lblRésultat.Text),0).ToString();
                if (lblOpérations.Text == "0")
                {
                    Calculer();
                }
                lblRésultat.Text = string.Empty;
            }
            
            DernièreTouche = Touche.CHIFFRE;
        }

        /// <summary>
        /// Supprime la dernière entrée et fait appel à la fonction Calculer() si la dernière entrée est un chiffre.
        /// </summary>
        private void ButSupClicked(object sender, EventArgs e)
        {
            int length = lblOpérations.Text.Length;
            if (length > 0 && lblOpérations.Text != "0")
            {
                int i;
                lblOpérations.Text = lblOpérations.Text.Remove(length - 1);
                length = lblOpérations.Text.Length;

                if (length > 0 && int.TryParse(lblOpérations.Text[length - 1].ToString(), out i))
                {
                    Calculer();
                    DernièreTouche = Touche.CHIFFRE;
                }
                else if (length == 0)
                {
                    lblOpérations.Text = "0";
                    lblRésultat.Text = string.Empty;
                    DernièreTouche = Touche.AUCUNE;
                }
            }
        }
    }
}

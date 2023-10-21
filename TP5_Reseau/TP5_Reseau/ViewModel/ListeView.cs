using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using TP5_Reseau.SharpTrooper.Entities;
using System.Diagnostics;

namespace TP5_Reseau.ViewModel
{
    public class ListeView<T, ViewT>
        where T : SharpEntity
        where ViewT : ViewEntity<T>, new()
    {
        private INetworkStatus networkStatus;
        private string url;

        public ObservableCollection<ViewT> Liste { get; set; }

        public ListeView(string url, INetworkStatus networkStatus)
        {
            this.url = url;
            Liste = new ObservableCollection<ViewT>();
            this.networkStatus = networkStatus;
        }

        public static ViewT CreateInstance(T entity)
        {
            ViewT res = new ViewT();
            res.Entity = entity;
            return res;
        }

        public async void Initialiser()
        {
            if (Liste.Count > 0) return;

            // On vérifie l'état du réseau
            if (this.networkStatus == null || !await this.networkStatus.CanAccess()) return;

            // Création de la connexion
            this.networkStatus.IsAccessing = true;
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage reponse = await client.GetAsync(url);
                if (reponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContent content = reponse.Content;
                    string json = await content.ReadAsStringAsync();
                    SharpEntityResults<T> entities = JsonConvert.DeserializeObject<SharpEntityResults<T>>(json);
                    foreach (var entity in entities.results)
                    {
                        this.Liste.Add(ListeView<T, ViewT>.CreateInstance(entity));
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Initialiser : Erreur de requête - {reponse.StatusCode} ");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"** Erreur lors de l’accès réseau : {e.Message}");
            }
            finally
            {
                this.networkStatus.IsAccessing = false;
            }
        }

        public async Task<ViewT> GetEntity(string url)
        {
            foreach (var entity in this.Liste)
            {
                if (entity.Entity.url == url)
                    return entity;
            }

            // On n'a pas trouvé l'élément dans la liste
            // Certainement parce que la liste n'a pas été chargée.
            // Création de la connexion
            if (!await this.networkStatus.CanAccess()) return null;

            try
            {
                this.networkStatus.IsAccessing = true;
                var client = new HttpClient();
                HttpResponseMessage reponse = await client.GetAsync(url);
                if (reponse.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    System.Diagnostics.Debug.WriteLine("Status de la requete : " + reponse.StatusCode);
                    return null;
                }
                HttpContent contenu = reponse.Content;
                string json = await contenu.ReadAsStringAsync();
                T res;
                try
                {
                    res = JsonConvert.DeserializeObject<T>(json);
                    return ListeView<T, ViewT>.CreateInstance(res);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Erreur sur la désérialisation de JSON");
                    System.Diagnostics.Debug.WriteLine("" + e + ", message : " + e.Message);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Erreur sur la connexion : " + e.Message);
            }
            finally
            {
                this.networkStatus.IsAccessing = false;
            }
            return null;
        }
    }
}

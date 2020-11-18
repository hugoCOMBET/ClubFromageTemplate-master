using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ModelLayer.Business;
using ModelLayer.Data;
using WpfClubFromage.viewModel;
using System.Windows;

namespace WpfClubFromage.viewModel
{
    class viewModelFromage : viewModelBase
    {
        //déclaration des attributs ...à compléter
        private DaoPays vmDaoPays;
        private DaoFromage vmDaoFromage;
        private ICommand updateCommand;
        private ObservableCollection<Pays> listPays;
        private ObservableCollection<Fromage> listFromage;
        private Fromage monFromage = new Fromage(1,"Rebloch");
        private Fromage selectedFromage = new Fromage();
        private Fromage activeFromage = new Fromage();

        //déclaration des listes...à compléter avec les fromages
        public ObservableCollection<Pays> ListPays { get => listPays; set => listPays = value; }
        public ObservableCollection<Fromage> ListFromages { get => listFromage; set => listFromage = value; }
        //déclaration des propriétés avec OnPropertyChanged("nom_propriété_bindée")
        //par exemple...
        public string Name
        {
            get => activeFromage.Name;
            set
            {
                if (activeFromage.Name != value)
                {
                    activeFromage.Name = value;
                    //création d'un évènement si la propriété Name (bindée dans le XAML) change
                    OnPropertyChanged("Name");
                }
            }
        }
        public Pays Origin
        {
            get 
            {
                if (activeFromage.Origin == null)
                {
                    activeFromage.Origin = null;
                    return null;
                }
                else
                {
                    return activeFromage.Origin;
                }
            }
            set
            {
                if (activeFromage.Origin != value)
                {
                    activeFromage.Origin = value;
                    OnPropertyChanged("Origin");
                }
            }
        }

        public DateTime Creation
        {
            get => activeFromage.Creation;
            set
            {
                if (activeFromage.Creation != value)
                {
                    activeFromage.Creation = value;
                    //création d'un évènement si la propriété Creation (bindée dans le XAML) change
                    OnPropertyChanged("Creation");
                }
            }
        }
        
        public Fromage SelectedFromage
        {
            get => selectedFromage;
            set
            {
                if (selectedFromage != value)
                {
                    selectedFromage = value;
                    //création d'un évènement si la propriété Name (bindée dans le XAML) change
                    OnPropertyChanged("SelectedFromage");
                    ActiveFromage = selectedFromage;
                }
            }
        }
        public Fromage ActiveFromage
        {
            get => activeFromage;
            set
            {
                if (activeFromage != value)
                {
                    activeFromage = value;
                    //création d'un évènement si la propriété Name (bindée dans le XAML) change
                    OnPropertyChanged("Name");
                    OnPropertyChanged("Origin");
                    OnPropertyChanged("Creation");
                }
            }
        }



        //déclaration du contructeur de viewModelFromage
        public viewModelFromage(DaoPays thedaopays, DaoFromage thedaofromage)
        {
            vmDaoPays = thedaopays;

            listPays = new ObservableCollection<Pays>(thedaopays.SelectAll());

            vmDaoFromage = thedaofromage;

            listFromage = new ObservableCollection<Fromage>(thedaofromage.SelectAll());

            //foreach (Fromage F in ListFromages)
            //{
            //    foreach (Pays P in ListPays)
            //    {
            //        if (F.Origin.Name == P.Name)
            //        {
            //            F.Origin = P;
            //        }
            //    }
            //}
            foreach (Fromage F in ListFromages)
            {
                int i = 0;
                while (F.Origin.Id != listPays[i].Id)
                {
                    i++;
                }
                F.Origin = listPays[i];
            }
        }

        //Méthode appelée au click du bouton UpdateCommand
        public ICommand UpdateCommand
        {
            get
            {
                if (this.updateCommand == null)
                {
                    this.updateCommand = new RelayCommand(() => UpdateFromage(), () => true);
                }
                return this.updateCommand;

            }

        }

        private void UpdateFromage()
        {
            //code du bouton - à coder
            this.vmDaoFromage.Update(this.activeFromage);
            MessageBox.Show("Mise à jour réussi");
        }

        //private void AjouterFromage()
        //{
        //    listFromage.Add();
        //}
        private void SupprimerFromage()
        {
            this.vmDaoFromage.Delete(this.activeFromage);
            listFromage.Remove(SelectedFromage);
            MessageBox.Show("suppression réussie");
        }
    }
}

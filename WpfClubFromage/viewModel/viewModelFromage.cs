﻿using System;
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
        private ICommand supprimerCommand;
        private ICommand ajouterCommand;
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
                    if (selectedFromage != null)
                    {
                        ActiveFromage = selectedFromage;
                    }
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

        public ICommand AjouterCommand
        {
            get
            {
                if (this.ajouterCommand == null)
                {
                    this.ajouterCommand = new RelayCommand(() => AjouterFromage(), () => true);
                }
                return this.ajouterCommand;
            }
        }

        //Méthode appelée au click du bouton SupprimerFromage

        public ICommand SupprimerCommand
        {
            get
            {
                if (this.supprimerCommand == null)
                {
                    this.supprimerCommand = new RelayCommand(() => SupprimerFromage(), () => true);
                }
                return this.supprimerCommand;
            }
        }

        private void UpdateFromage()
        {
            Fromage backup = new Fromage();
            backup = ActiveFromage;
            this.vmDaoFromage.Update(this.ActiveFromage);
            int a = listFromage.IndexOf(SelectedFromage);
            listFromage.Insert(a, ActiveFromage);
            listFromage.RemoveAt(a + 1);
            SelectedFromage = backup;
            MessageBox.Show("Mise à jour réussie");
        }

        private void AjouterFromage()
        {
            foreach(Fromage f in listFromage)
            {
                int id = f.Id + 1;
                ActiveFromage.Id = id;
            }
            this.vmDaoFromage.Insert(this.activeFromage);
            listFromage.Add(this.activeFromage);
            MessageBox.Show("ajout réussie");

        }
        private void SupprimerFromage()
        {
            Fromage backup = new Fromage();
            backup = ActiveFromage;
            this.vmDaoFromage.Delete(this.ActiveFromage);
            int a = listFromage.IndexOf(SelectedFromage);
            listFromage.RemoveAt(a);
            MessageBox.Show("Fromage supprimé");
        }
    }
}
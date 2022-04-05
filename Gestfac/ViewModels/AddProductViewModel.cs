using EvernoteTagControlLibrary;
using Gestfac.Commands;
using Gestfac.Models;
using Gestfac.Services;
using Gestfac.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Gestfac.ViewModels
{
    public class AddProductViewModel : ViewModelBase
    {
        private string _externalId;
        private string _description;
        private string _tags;
        private double _currentPrice;

        public string ExternalId
        {
            get { 
                return _externalId; 
            }
            set { 
                _externalId = value;
                OnPropertyChanged(nameof(ExternalId));
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public string Tags
        {
            get
            {
                return _tags;
            }
            set
            {
                _tags = value;
                OnPropertyChanged(nameof(Tags));
            }
        }

        private List<EvernoteTagItem> _selectedTags = new List<EvernoteTagItem>();
        public List<EvernoteTagItem> SelectedTags
        {
            get { return _selectedTags; }
            set
            {
                _selectedTags = value;
                if (_selectedTags != value)
                    OnPropertyChanged("SelectedTags");
            }
        }

        public double CurrentPrice
        {
            get
            {
                return _currentPrice;
            }
            set
            {
                _currentPrice = value;
                OnPropertyChanged(nameof(CurrentPrice));
            }
        }

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public AddProductViewModel(CatalogStore catalogStore, NavigationService<ProductListingViewModel> productListingViewNavigationService)
        {
            SubmitCommand = new AddProductCommand(this, catalogStore, productListingViewNavigationService);
            CancelCommand = new NavigateCommand<ProductListingViewModel>(productListingViewNavigationService);

            this.SelectedTags = new List<EvernoteTagItem>() { new EvernoteTagItem("news"), new EvernoteTagItem("priority") };
        }
    }
}

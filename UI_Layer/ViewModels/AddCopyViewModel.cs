using UI_Layer.Commands;
using DataAccess.DataAccess;
using UI_Layer.ErrorNotification;
using DomainLayer.Models;
using DomainLayer.Abstractions;
using UI_Layer.Services;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace UI_Layer.ViewModels
{
    public class AddCopyViewModel : BaseViewModel, INotifyDataErrorInfo
    {
        protected IRepositoryService<Copy> repositoryService = new DBCopyService();
        private readonly ErrorNotifier errorNotifier;
        public bool HasErrors => errorNotifier.HasErrors;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public AddCopyViewModel(ErrorNotifier _errorNotifier)
        {
            errorNotifier = _errorNotifier;
            errorNotifier.ErrorsChanged += ErrorNotifier_ErrorsChanged;
        }

        private void ErrorNotifier_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
            OnPropertyChanged(nameof(CanAdd));
        }
        public IEnumerable GetErrors(string propertyName)
        {
            return errorNotifier.GetErrors(propertyName);
        }
        public bool CanAdd => !HasErrors;
        public ICommand AddCopyCommand { get => new AddRelayCommand(InsertCopyIntoDB, () => CanAdd && Book is not null); }

        private BookViewModel _book; 
        public BookViewModel Book
        {
            get { return _book; }
            set { _book = value; OnPropertyChanged(nameof(State)); }
        }

        private string state;
        public string State
        {
            get { return state; }
            set
            {
                state = value;
                Message = "";
                OnPropertyChanged(nameof(State));                
            }
        }
        public Array States => Enum.GetValues(typeof(State));
        public int ComboBoxIndex { get => 0; }
        public bool ShowMessage => !string.IsNullOrEmpty(Message);
        private string message;
        public string Message { get { return message; } set { message = value; OnPropertyChanged(nameof(Message)); } }
        public void InsertCopyIntoDB(string parameter)
        {
            var uniqueID = Guid.NewGuid().ToString();
            repositoryService = new DBCopyService();
            repositoryService.InsertItem(new Copy(0, uniqueID, Book.BookID, false, State));
            Book.AllCopies.Add(new CopyViewModel(repositoryService.GetByUniqueID(uniqueID)));
            Message = $"Copy Added successfully!!";
            OnPropertyChanged(nameof(ShowMessage));
        }
    }
}

using UI_Layer.Commands;
using DataAccess.DataAccess;
using UI_Layer.ErrorNotification;
using DomainLayer.Models;
using DomainLayer.Abstractions;
using UI_Layer.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UI_Layer.ViewModels
{
    public class UpdateCopyViewModel : BaseViewModel , INotifyDataErrorInfo
    {
        private readonly Messanger messanger;
        private readonly IRepositoryService<Copy> repositoryService;
        private readonly UpdateBookViewModel updateBook;
        //public event Action SelectedCopyChanged;
        private readonly ErrorNotifier errorNotifier;
        private readonly Dictionary<string, string> properties = new Dictionary<string, string>();

        public UpdateCopyViewModel(ErrorNotifier errorNotifier, IRepositoryService<Copy> repositoryService, UpdateBookViewModel updateBook, Messanger messanger)
        {
            this.repositoryService = repositoryService;
            this.errorNotifier = errorNotifier;
            this.updateBook = updateBook;
            this.messanger = messanger;            
            messanger.CopyDeleted += Messanger_CopyDeleted;
            messanger.BookUpdated += UpdateBook_BookUpdated;
            messanger.BookDeleted += DeleteBook_BookDeleted;
            messanger.SelectedBookChanged += Messanger_SelectedBookChanged;

            foreach (State item in Enum.GetValues(typeof(State)))
            {
                States.Add(item.ToString());
            }
            properties.Add("State", "State");
            properties.Add("IsRented", "Borrowed");
            SelectedBook = updateBook.Books.Count > 0 ? updateBook.SelectedBook : null;
            SelectedCopy = SelectedBook is not null? SelectedBook.AllCopies.Count > 0 ? SelectedBook.AllCopies.First() : null : null;
        }

        private void Messanger_SelectedBookChanged()
        {
            if (updateBook.Books.Count == 0)
            {
                SelectedBook = null;
            }
        }

        private void Messanger_CopyDeleted()
        {
            OnPropertyChanged(nameof(SelectedCopy));
        }

        private void DeleteBook_BookDeleted()
        {
            if (updateBook.Books.Count > 0)
            {
                SelectedBook = updateBook.SelectedBook;
                OnPropertyChanged(nameof(SelectedBook)); 
            }
        }

        private void UpdateBook_BookUpdated()
        {
            //if (index == CurrentBookIndex)
            //{
            //    updateBook.SelectedBook = Books[index];
            SelectedBook = updateBook.Books[CurrentBookIndex];
            OnPropertyChanged(nameof(SelectedBook));
            SelectedCopy = SelectedBook.AllCopies.First();
            OnPropertyChanged(nameof(SelectedCopy));
            //}
            //SelectedBook = Books[CurrentBookIndex];
        }

        public List<string> States { get; set; } = new List<string>();
        public int ComboBoxIndex { get => 0; }
        #region Error notification
        public bool ShowMessage => !string.IsNullOrEmpty(Message);
        private string message;
        public string Message { get { return message; } set { message = value; OnPropertyChanged(nameof(Message)); } }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        private void ErrorNotifier_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
            OnPropertyChanged(nameof(CanUpdate));
        }
        public bool HasErrors => errorNotifier.HasErrors;
        public bool CanUpdate => !HasErrors;

        public IEnumerable GetErrors(string propertyName)
        {
            return errorNotifier.GetErrors(propertyName);
        }
        #endregion
        public ICommand UpdateStateCommand { get => new UpdateRelayCommand(UpdateProperty, () => CanUpdate); }
        public ICommand UpdateBorrowedCommand { get => new UpdateRelayCommand(UpdateProperty, () => CanUpdate); }

        private void UpdateProperty(string parameter)
        {
            PropertyInfo propertyValue = GetType().GetProperty(properties[parameter]);
            try
            {
                string uniqueID = SelectedCopy.UniqueID;
                int intBoolValue;
                if (parameter == "IsRented")
                {
                    bool BorrValue = (bool)propertyValue.GetValue(this);
                    intBoolValue = BorrValue ? 1 : 0 ;
                    repositoryService.UpdateItem(SelectedCopy.ID, parameter, intBoolValue);
                }
                else
                {
                    repositoryService.UpdateItem(SelectedCopy.ID, parameter, propertyValue.GetValue(this));
                }
                updateBook.SelectedBook.AllCopies.Remove(SelectedCopy);
                SelectedCopy = new CopyViewModel(repositoryService.GetByUniqueID(uniqueID));
                updateBook.SelectedBook.AllCopies.Add(SelectedCopy);
                OnPropertyChanged(nameof(SelectedCopy));
                //OnPropertyChanged(nameof(SelectedBook));
                Message = $"{propertyValue.Name} Updated Successfully!";
                OnPropertyChanged(nameof(ShowMessage));

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }


        #region Properties
        public ObservableCollection<BookViewModel> Books { get => updateBook.Books; }

        public int CurrentBookIndex { get; set; }
        //public BookViewModel SelectedBook { get => updateBook.SelectedBook; }
        private BookViewModel selectedBook;
        public BookViewModel SelectedBook
        {
            get { return selectedBook; }
            set
            {
                if (value is not null)
                {
                    CurrentBookIndex = Books.IndexOf(value);
                    selectedBook = value;
                    SelectedCopy = value.AllCopies.Count > 0 ? value.AllCopies.First() : null;
                    OnPropertyChanged(nameof(SelectedCopy));
                    Message = "";
                    OnPropertyChanged(nameof(ShowMessage));
                }
                else
                {
                    SelectedCopy = null;
                }
            }
        }

        private CopyViewModel selectedCopy;
        public CopyViewModel SelectedCopy
        {
            get { return selectedCopy; }
            set
            {
                selectedCopy = value;
                //SelectedCopyChanged?.Invoke();
                messanger.WhenSelectedCopyChanged();
                State = value != null ? value.State : string.Empty;
                if (value != null)
                {
                    Borrowed = value.IsRented;
                }
                Message = "";
                OnPropertyChanged(nameof(ShowMessage));
            }
        }

        private string state;
        public string State
        {
            get { return state; }
            set
            {
                state = value;
                OnPropertyChanged(nameof(State));
                errorNotifier.ClearError(nameof(State));
                Message = "";
                OnPropertyChanged(nameof(ShowMessage));
                if (SelectedCopy is null)
                {
                    errorNotifier.AddError(nameof(State), "Please select a copy first!");
                    OnPropertyChanged(nameof(CanUpdate));
                }
            }
        }

        private bool borrowed;
        public bool Borrowed
        {
            get {return borrowed;}  
            set 
            {
                borrowed = value;
                OnPropertyChanged(nameof(Borrowed));
                errorNotifier.ClearError(nameof(Borrowed));
                Message = "";
                OnPropertyChanged(nameof(ShowMessage));
                if (SelectedCopy is null)
                {
                    errorNotifier.AddError(nameof(Borrowed), "Please select a copy first!");
                    OnPropertyChanged(nameof(CanUpdate));
                }
            } 
        }        
        #endregion

    }
}

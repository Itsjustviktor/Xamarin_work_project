using SQLite;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AndroidApp.Models
{
    [Table("BaseModel")]
    public abstract class BaseModel
        : INotifyPropertyChanged
    {
        [PrimaryKey]
        public virtual Int32 Id { get; set; }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
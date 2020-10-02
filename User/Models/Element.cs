using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Models
{
    public class Element : INotifyPropertyChanged
    {
        private string path;
        private string icon;

        public event PropertyChangedEventHandler PropertyChanged;

        private Exception ElementNotFoundException = new Exception("Element not found");

        public string Path
        {
            get { return path; }
            set
            {
                path = value;
                OnPropertyChanged("Path");
            }
        }

        public string Icon
        {
            get { return icon; }
            set
            {
                icon = value;
                OnPropertyChanged("Icon");
            }
        }

        public Element(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            if (fileInfo.Exists == true)
            {
                this.path = path;
                icon = "/Images/icons8-document-36.png";
            }
            else if (directoryInfo.Exists == true)
            {
                this.path = path;
                icon = "/Images/icons8-folder-36.png";
            }
            else throw ElementNotFoundException;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

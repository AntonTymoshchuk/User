using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Models
{
    public class Content : INotifyPropertyChanged
    {
        private ObservableCollection<Element> elements;
        private int directoriesCount;
        private int filesCount;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Element> Elements
        {
            get { return elements; }
            set
            {
                elements = value;
                OnPropertyChanged("Elements");
            }
        }

        public int DirectoriesCount
        {
            get { return directoriesCount; }
        }

        public int FilesCount
        {
            get { return filesCount; }
        }

        public int Total
        {
            get { return directoriesCount + filesCount; }
        }

        public Content()
        {
            elements = new ObservableCollection<Element>();

            DirectoryInfo directoryInfo = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            GetDirectoryContent(directoryInfo);

            directoryInfo = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic));
            GetDirectoryContent(directoryInfo);

            directoryInfo = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
            GetDirectoryContent(directoryInfo);

            directoryInfo = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos));
            GetDirectoryContent(directoryInfo);

            directoryInfo = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
            GetDirectoryContent(directoryInfo);
        }

        private void GetDirectoryContent(DirectoryInfo directoryInfo)
        {
            try
            {
                DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories();
                for (int i = 0; i < directoryInfos.Length; i++)
                {
                    elements.Add(new Element(directoryInfos[i].FullName));
                    GetDirectoryContent(directoryInfos[i]);
                    directoriesCount++;
                }

                FileInfo[] fileInfos = directoryInfo.GetFiles();
                for (int i = 0; i < fileInfos.Length; i++)
                {
                    elements.Add(new Element(fileInfos[i].FullName));
                    filesCount++;
                }
            }
            catch { }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

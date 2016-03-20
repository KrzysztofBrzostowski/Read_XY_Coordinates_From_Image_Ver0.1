using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadXYCoordinatesFromImage.Model
{
    public class ImageProcessingModel : INotifyPropertyChanged
    {
        private Boolean m_IsSaveButtonEnabled;

        public Boolean IsSaveButtonEnabled
        {
            get { return m_IsSaveButtonEnabled; }
            set { m_IsSaveButtonEnabled = value; WyslijPowiadomienie("IsSaveButtonEnabled"); }
        }

        private String m_NumberOfTxtRow;

        public String NumberOfTxtRow
        {
            get { return m_NumberOfTxtRow; }
            set { m_NumberOfTxtRow = value; WyslijPowiadomienie("NumberOfTxtRow"); }
        }

        private String m_SelectedFileName;

        public String SelectedFileName
        {
            get { return m_SelectedFileName; }
            set { m_SelectedFileName = value; WyslijPowiadomienie("SelectedFileName"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void WyslijPowiadomienie(string NazwaWlasciwosci)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(NazwaWlasciwosci));
            }
        }
    }

}

    
    

using System;

namespace VipaksTestTask.ViewModels
{
    /// <summary>
    /// Модель для хранения данных о количестве прибывших и улетевших пассажиров за час
    /// </summary>
    public class HourColumn : ViewModel
    {
        private int _arrivedCount;
        private int _departuredCount;
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }

        public int ArrivedCount
        {
            get { return _arrivedCount; }
            set
            {
                _arrivedCount = value;
                RaisePropertyChanged(nameof(ArrivedCount));
            }
        }

        public int DeparturedCount
        {
            get { return _departuredCount; }
            set
            {
                _departuredCount = value;
                RaisePropertyChanged(nameof(DeparturedCount));
            }
        }
    }
}
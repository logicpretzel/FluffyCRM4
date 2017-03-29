using FluffyCRM.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluffyCRM.utils
{

    public  class flSettings
    {
        private Settings setting ;
        private string _publisher;

        public flSettings() {
            setting = new Settings();
        }
        public string Publisher
        {
            get {
                _publisher = setting.Publisher.ToString();
                return _publisher;
            }
           
        }

        private string _appTitle;

        public string AppTitle
        {
            get {
                _appTitle = setting.AppTitle.ToString();
                return _appTitle;
            }
           
        }


    }
}

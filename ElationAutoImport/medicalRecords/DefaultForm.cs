using System;

namespace ElationAutoImport
{
    class DefaultForm : GeneralForm
    {
        public DefaultForm(string fileTitle)
        {
            fileName = fileTitle;
            printInfo();
        }
    }
}

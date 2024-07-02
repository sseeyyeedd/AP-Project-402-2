using SofreDaar.Infrastructure;
using SofreDaar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SofreDaar.ViewModels
{
    public class ReportManagement:BaseViewModel
    {
        public ReportManagement(DatabaseContext DbContext, MainViewModel main) : base(DbContext, main)
        {
            
        }
        public void UpdateReport(Report report)
        {

        }
    }
}

using SofreDaar.Infrastructure;

namespace SofreDaar.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {
        public SignUpViewModel(DatabaseContext databaseContext, MainViewModel main) : base(databaseContext, main)
        {
        }
    }
}

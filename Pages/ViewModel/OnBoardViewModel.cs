using Spring.ViewModel.Base;
using System;


namespace Spring.Pages.ViewModel
{
    public class OnBoardViewModel : BaseViewModel
    {
        //indecates every movements of adding new card [subboard] this makes this vm singelton!
        public bool NewSubBoard { get; set; }
    }
}

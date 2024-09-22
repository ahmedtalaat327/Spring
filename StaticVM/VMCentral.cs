////You can replace this class with injet [memory locker mechanism]

using Spring.Pages.ViewModel;
using Spring.ViewModel;

namespace Spring.StaticVM
{
    public static class VMCentral
    {
        public static DockingManagerViewModel DockingManagerViewModel = new DockingManagerViewModel();

        public static OnBoardViewModel OnBoardViewModel = new OnBoardViewModel();
    }
}

using System.Reflection;
using System.Windows.Forms;

namespace AndroidIntelliTool
{
    public class DoubleBufferedListView : ListView
    {
        public DoubleBufferedListView()
        {
            // Activate double buffering
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            // And call the base class method to set the DoubleBuffered property
            typeof(ListView).InvokeMember(
                "DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
                null,
                this,
                new object[] { true });
        }
    }
}

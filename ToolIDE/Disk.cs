using System.Windows.Shapes;

namespace ToolIDE
{
    public class Disk
    {
        public int PillarIndex { get; set; }
        public Rectangle Rectangle { get; }
        
        public int DiskSize { get; }

        public Disk(int pillarIndex, Rectangle rectangle, int diskSize)
        {
            PillarIndex = pillarIndex;
            Rectangle = rectangle;
            DiskSize = diskSize;
        }
    }
}
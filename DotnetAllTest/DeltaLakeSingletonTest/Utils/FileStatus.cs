using System.Runtime.ConstrainedExecution;

namespace DeltaLakeSingletonTest.Utils
{
    public class FileStatus
    {
        private readonly string path;

        private readonly long size;

        private readonly long modificationTime;

        protected FileStatus(string path, long size, long modificationTime)
        {
            this.path = path;
            this.size = size;
            this.modificationTime = modificationTime;
        }

        public string Path { get { return path; } }

        public long Size { get { return size; } }

        public long ModificationTime {  get { return modificationTime; } }

        public static FileStatus Of(string path, long size, long modificationTime)
        {
            return new FileStatus(path, size, modificationTime);
        }
    }
}

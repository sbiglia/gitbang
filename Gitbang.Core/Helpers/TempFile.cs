using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Serilog;

namespace Gitbang.Core.Helpers
{
    public class TempFile : IDisposable
    {
        public string FilePath { get; private set; }

        public TempFile()
            : this(Path.GetTempPath())
        {
        }

        public TempFile(string directory) => Create(Path.Combine(directory, Path.GetRandomFileName()));

        ~TempFile() => Delete();

        public void Dispose()
        {
            Delete();
            GC.SuppressFinalize(this);
        }

        private void Create(string path)
        {
            FilePath = path;
            using (File.Create(FilePath))
                ;
        }

        private void Delete()
        {
            if (FilePath == null)
                return;

            try
            {
                File.Delete(FilePath);
            }
            catch (Exception ex)
            {
                Log.Logger.Debug(ex, "Temp file can't be deleted.");
            }

            FilePath = null;
        }
    }
}
